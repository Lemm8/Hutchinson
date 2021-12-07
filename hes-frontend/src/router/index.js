import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import IdeasMejora from "../views/IdeasMejora.vue";
import qrqc_system from "../views/qrqc_system.vue"
import auditorias from "../views/Auditorias.vue"
import auditores from "../views/Auditores.vue"
import policy_dev from "../views/policy_dev.vue"
import sistema_6s from "../views/sistema_6s.vue"
import proyectos_mejora from "../views/proyectos_mejora.vue"
import hes_training from "../views/hes_training.vue"

import axios from 'axios'
import VueAxios from 'vue-axios'

import store from '../store/index.js';

Vue.use(VueRouter);
Vue.use(VueAxios, axios)


const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/ideas-mejora",
    name: "ideas-mejora",
    component: IdeasMejora,
  },

  {
    path: "/qrqc_system",
    name: "qrqc_system",
    component: qrqc_system,
    meta: { auth: true }
  },
  {
    path: "/policy_dev",
    name: "policy_dev",
    component: policy_dev,
    meta: { auth: true }
  },
  {
    path: "/sistema_6s",
    name: "sistema_6s",
    component: sistema_6s,
    meta: { auth: true }
  },
  {
    path: "/auditorias",
    name: "auditorias",
    component: auditorias,
    meta: { auth: true }
  },  
  {
    path: "/auditores",
    name: "auditores",
    component: auditores,
    meta: { auth: true }
  },  
  {
    path: "/proyectos_mejora",
    name: "proyectos_mejora",
    component: proyectos_mejora,
    meta: { auth: true }
  },
  {
    path: "/hes_training",
    name: "hes_training",
    component: hes_training,
    meta: { auth: true }
  }

];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach(async (to, from, next) => {

  let authenticated = store.getters['auth/isAuthenticated']
  let firstload = store.getters['isFirstLoad']

  if (!authenticated && firstload) {
    try { await store.dispatch('auth/me') } catch (err) { store.commit('auth/auth_logout') } finally { store.commit('setFirstLoad', false) }
  }

  if (to.meta.auth) {

    if (!store.state.auth.token) {

      store.commit('snackbar_show', { text: 'Acceso no authorizado. Favor de acceder con sus credenciales' });
      next({ name: 'Home' });

    } else {

      let found = true

      if (to.meta.permissions)
        for (let index = 0; index < to.meta.permissions.length; index++) {
          const element = to.meta.permissions[index];

          if (store.state.auth.profile.permissions.filter((item) => item.description === element).length > 0) {
            found = true

            break
          }

          found = false
        }

      if (!found) {

        store.commit('snackbar_show', { text: 'Unathorized access' })
        next({ name: 'Login' })
      }
      else {

        next()
      }

    }
  }
  else {
    next();
  }

});

export default router;
