import config from "../../config";
import axios  from '../services/api.service';

export const auth = {
    namespaced: true,
    state: {
        token:  '',
        profile: ''
    },
    mutations: {
        auth_success(state, data) {
            state.profile = data.user;
            state.token = data._token;
            
        },
        auth_logout(state) {
            state.profile = '';
            state.token = '';      
        }
    },
    actions: {

        async login({ commit }, params){
   
            try {

                let res = await axios.post(config.API_URL_ROOT_AUTH + "/auth", params);
                
                commit('auth_success', res.data);

                return Promise.resolve(true)

            } catch(err) {
      
                return Promise.reject(err)
            }
               
  
        },
        async me({commit}) {
            

            try {

                let res = await axios.get(config.API_URL_ROOT_AUTH + '/auth/me')
                commit('auth_success', res.data);
                
                return Promise.resolve(true)

            } catch(err) {
                return Promise.reject(err)
            }
       
        },
        async refreshSession({commit}) {
            

            try {

                let res =  await axios.post(config.API_URL_ROOT_AUTH + '/auth/session/refresh');
                
                commit('auth_success', res.data);

                return Promise.resolve(true)

            } catch(err) {
            
                return Promise.reject(err)
            }

   
        },
        async logout({ commit }) {

            try {
       

                await axios.post(config.API_URL_ROOT_AUTH + '/auth/session/end')
                
                commit('auth_logout');
                return Promise.resolve(true)

            } catch(err) {

                return Promise.reject(err)
            }
             
     
        }
        
    },
    getters: {
        isAuthenticated: state => !!state.token,
        profile: state => state.profile,
    }
}