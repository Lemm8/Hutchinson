<template>
  <div>
    <div class="text-right">
      <img src="@/assets/hes-right.png" />
    </div>
    <v-container>      
      <div class="text-center">
        <img src="@/assets/hes-main.png"  class="hes-main"/>
      </div>
      <div class="mt-5">
        <v-row>
          <v-col cols="12" xs="12" sm="12" md="3" lg="3" v-for="(module,index) in modules" :key="index">
            <div class="d-flex align-center justify-center">
              <div class="module hoverable" @click="navigate(module.to)">
                <div class="text-center"><img :src="require('@/assets/' + module.image)" /></div>
                <div class="text-center pa-3 module-description ">{{module.title}}</div>
              </div>
            </div>
          </v-col>
        </v-row>
      </div>
    </v-container>
  </div>
</template>

<style scoped>
.module {
  max-width: 250px;
}
.module-description {
  background-color: rgb(22, 54, 92);
  color: white;
  font-weight: 500;
  height: 72px;
  min-width: 250px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.text-right{
  width: 100%;
  height: 100%;
}
.hoverable:hover {
  cursor: pointer;
}
.hes-main {
   width: 42%;
  height: 42%;
}
</style>

<script>

export default {
  name: "Home",
  data() {
    return {
      modules: [
        {title: 'SISTEMA DE IDEAS DE MEJORA', image: 'ideas-mejora.png', to: 'ideas-mejora'},
        {title: 'I-QRQC SYSTEM', image: 'blank.png', to: 'qrqc_system'},
        {title: 'POLICY DEPLOYMENT (STRATEGY)', image: 'blank.png', to: 'policy_dev'},
        {title: 'SISTEMA 6S', image: '6sss.png', to: 'sistema_6s'},
        {title: 'PROYECTOS DE MEJORA', image: 'proyectos-mejora.png', to: 'proyectos_mejora'},
        {title: 'HES TRAINING', image: 'blank.png', to: 'hes_training'}
      ]
    }
  },

  methods: {
    navigate(to) {
      this.$router.push({name: to})
    }
    
  },
};
</script>