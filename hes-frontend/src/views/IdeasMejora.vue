<template>
    <div>
        módulo "ideas mejora" dentro de este proyecto o pueden crear otro proyecto específico y referenciarlo con un href en la navegación 
        <div @click="navigate()">regresar</div>
    </div>
</template>

<script>
export default {
    data() {
        return  {

        }
    },

    methods: {
        navigate() {
            this.$router.push({name: 'Home'})
        }
    }
}
</script>