
<template>
  <v-container>
      <v-row>
        <img src="../assets/6sss.png" class="logo" />
        <v-col class="titulo-pagina">Plataforma 6S</v-col>
      </v-row>
      <v-row no-gutters>
        <v-col class="titulo">AUDITORES 6S</v-col>
      </v-row>
      <v-row>
        <v-col class="titulo">OPERACIONES</v-col>
        <v-col class="titulo">OFICINAS</v-col>
      </v-row>
      <v-col class="pt-13">
        <v-row>
          <v-col md="20">
            <Auditor v-bind:auditores="info"></Auditor>
          </v-col>
          <v-col md="1">
          </v-col>
          <v-col md="20">
            <Auditor v-bind:auditores="info"></Auditor>
          </v-col>
        </v-row>
    </v-col>
  </v-container>
</template>

<script>
import axios from 'axios';
import Auditor from '../components/Auditor.vue';
  export default {
    name: 'Auditores',
    components: {
      'Auditor': Auditor
    },

    data: () => {
      return {
        info : []
      }
    },
      mounted () {
        axios
          .get('https://localhost:29596/api/auditores')
          .then(response => {
            console.log(response)
            this.info = response.data
            console.log(this.info)
          })
      }
    }
</script>
<style>
.logo{
  height: 7rem;
}
.auditores {
  margin: 0 auto;
}
.titulo-pagina {
  padding-top: 2rem;
  padding-bottom: 1.3rem;
  margin-right: 8rem;
  text-align: center;
  font-weight: bold;
  font-size: 1.4rem;
  color: #7F7F7F;
  
}
.titulo {
  text-align: center;
  font-size: 1rem;
  
}
.button {
 padding: 1rem;
}
.text-center {
  padding-top: 4rem;
}
</style>
