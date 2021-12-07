<template>
  <v-layout row wrap>
    <v-flex lg4 v-for="item in this.auditores " v-bind:key="item.userId" class="d-flex">
      <v-card
        class="card d-flex flex-column"
        style="border: 1px solid black;"
        rounded="0"
        flat>
        <div 
          class="justify-center card-title"
          >
          {{item.auditorNombre}}
        </div>
        <v-responsive class="avatar">
          <v-avatar size="80" color="indigo">
            <v-icon dark size="80">
              mdi-account-circle
            </v-icon>
          </v-avatar>
        </v-responsive>
          <div 
            class="en-progreso" 
            v-if="item.auditoriaFechaCompleto == null"
            >En proceso</div>
          <div 
            class="validado" 
            v-else
            >Validado: {{item.auditoriaFechaCompleto.split("T")[0]}}</div>
      </v-card>
    </v-flex>
  </v-layout>
</template>
  
<script>

export default{
  props: {
    auditores: {
      type:Array,
      required: true
    }
  },

}
</script>

<style>
.avatar{
  padding-top: 0.5rem;
}
.en-progreso{
  font-size: 0.8rem;
  text-align: center;
  border: 1px solid black;
  background-color: yellow;
  width: 100%;
}
.card{
  height: 9rem;
  width: 25rem;
  text-align: center;
  border: 1px solid black;
}
.card-title{
  font-size: 1rem;
  border: 1px solid black;
  width: 100%;
}
.validado{
  font-size: 0.8rem;
  text-align: center;
  border: 1px solid black;
  background-color: #D7E4BD;
  width: 100%;
}
</style>

