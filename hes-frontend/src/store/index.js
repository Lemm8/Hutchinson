import Vue from "vue";
import Vuex from "vuex";


Vue.use(Vuex);

import { auth } from '../modules/auth.module.js'

export default new Vuex.Store({
  state: {
    firstLoad: true,
  },
  mutations: {
    setFirstLoad(state, data) {
        state.firstLoad = data
    },

  },
  actions: {},
  modules: {
    auth
  },
  getters: {
    isFirstLoad: state => state.firstLoad
  }
});
