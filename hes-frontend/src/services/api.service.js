import axios from "axios";
import c from '../../config';
import router from "../router";
import store from '../store';

axios.interceptors.request.use((c) => {

        let token = store.state.auth.token;

        if (token) 
            c.headers['Authorization'] = `Bearer ${ token }`;

        c.withCredentials = true

        return c;

    }, 
    (error) => {
        return Promise.reject(error);
    }
);

let isAlreadyFetchingAccessToken = false
let subscribers = []

function onAccessTokenFetched(access_token) {
  subscribers = subscribers.filter(callback => callback(access_token))
}

function addSubscriber(callback) {
  subscribers.push(callback)
}

axios.interceptors.response.use(function (response) {
  
    return response

}, function (error) {

    const { config, response } = error
    const originalRequest = config

    if(!response)
        return Promise.reject(error);

    if (response.status === 401) {

        if (!isAlreadyFetchingAccessToken) {
        
            isAlreadyFetchingAccessToken = true
      
            axios.post(c.API_URL_ROOT_AUTH + '/auth/session/refresh')
                .then(res => {

                    isAlreadyFetchingAccessToken = false
                    onAccessTokenFetched(res.data.token)
                    
                    store.commit('auth/auth_success', res.data);
                })
                .catch(() => {

                    router.push({ name: 'home' });
                    store.dispatch('auth/logout');

                });
        }

        const retryOriginalRequest = new Promise((resolve) => {
            addSubscriber(access_token => {
                
                originalRequest.headers.Authorization = 'Bearer ' + access_token

                resolve(axios(originalRequest))
            })
        })

        return retryOriginalRequest
    }
    
    return Promise.reject(error.response)

})

export default axios;
