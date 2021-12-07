<template>
  <v-app>
    <div id="nav"></div>
    <v-app-bar app color="#172646" dark clipped-left>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>

      <v-navigation-drawer
        v-model="drawer"
        app
        absolute
        fixed
        temporary
        :style="{ top: $vuetify.application.top + 'px', zIndex: 6 }"
        light        
      >
        <v-list nav dense>
          <v-list-item-group v-model="group">
            <v-list-item @click="navigate('Home')">
              <v-list-item-title >Inicio</v-list-item-title>
            </v-list-item>

            <v-list-item @click="navigate('ideas-mejora')">
              <v-list-item-title >Indicadores</v-list-item-title>
            </v-list-item>

            <v-list-group :value="true">
              <template v-slot:activator>
                <v-list-item-title>Dashboard</v-list-item-title>
              </template>
              <v-list-item v-for="(module, index) in modules" :key="index" @click="navigate(module.to)">
                <v-list-item-title  >{{module.title}}</v-list-item-title>
              </v-list-item>
            </v-list-group>
          </v-list-item-group>
        </v-list>
      </v-navigation-drawer>
      <v-toolbar-title>
        <v-btn icon large>
          <v-avatar size="32px">
            <img :src="require('./assets/hutchinson.png')" alt="Hutchinson" />
          </v-avatar>
        </v-btn>
        <span class="hidden-sm-and-down">HES</span>
      </v-toolbar-title>

      <v-spacer></v-spacer>

      <div class="mr-3 font-weight-medium">
        {{ profile.nombre || "Iniciar Sessión" }}
      </div>

      <v-avatar size="32px">
        <v-menu v-if="profile.nombre" offset-y>
          <template v-slot:activator="{ on }">
            <img
              :src="require('./assets/hutchinson.png')"
              alt="Avatar"
              v-on="on"
            />
          </template>
          <v-list>
            <v-list-item @click="logout()">
              <v-list-item-title>Cerrar Sessión</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
        <img
          v-else
          src="@/assets/user.png"
          alt="Avatar"
          @click="loginDialog = true"
        />
      </v-avatar>
    </v-app-bar>

    <v-main>
      <v-alert dense type="warning" v-if="isIE()">
        For better performance use chrome or firefox instead
      </v-alert>

      <router-view />
    </v-main>

    <Login :dialog="loginDialog" @close="loginDialog = false" />
  </v-app>
</template>

<script>
import Login from "./components/Login";

export default {
  name: "App",

  components: {
    Login,
  },

  data() {
    return {
      loginDialog: false,
      drawer: false,
      group: null,
      modules:[
        {title: 'Propuestas de mejora', to:'ideas-mejora'},
        {title: 'Control EEP', to:'ideas-mejora'},
        {title: 'Administración SSMA', to:'ideas-mejora'}
      ]
    };
  },
  mounted: function () {},
  watch: {
    group() {
      this.drawer = false;
    },
  },
  methods: {
    navigate(to) {
      this.$router.push({ name: to });
    },

    isIE() {
      if (
        navigator.userAgent.indexOf("MSIE") !== -1 ||
        navigator.appVersion.indexOf("Trident/") > -1 ||
        /Edge\/\d./i.test(navigator.userAgent)
      ) {
        return true;
      }

      return false;
    },

    logout: function () {
      this.$store.dispatch("auth/logout");

      if (this.$route.name != "Home") this.$router.push({ name: "Home" });
    },
  },

  computed: {
    profile() {
      return this.$store.state.auth.profile;
    },
  },
};
</script>

