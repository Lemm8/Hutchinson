<template>
    <div class="container">
        <div>
            <h2 id="title">Plataforma 6S</h2>
            <div id="codigo_colores">
                <h5>Código de colores</h5>
                <ul>
                    <li><i class="fa fa-check-circle" aria-hidden="true" style="color: green"></i> Área Auditada</li>
                    <li><i class="fa fa-check-circle" aria-hidden="true" style="color: red"></i> Auditada fuera de tiempo</li>
                    <li><i class="fa fa-hourglass-half" aria-hidden="true" style="color: red"></i> Fuera de tiempo</li>
                    <li><i class="fa fa-hourglass-half" aria-hidden="true" style="color: yellow"></i> Hoy vence</li>
                </ul>
            </div>
        </div>
        
        <div v-for="auditoria in meses_con_auditorias" :key="auditoria.id">
            <AuditoriaComponente v-bind:auditoria_object = "auditoria" />
        </div>
    </div>
</template>



<script>

import axios from 'axios'
import AuditoriaComponente from '../components/AuditoriaComponent.vue'

export default {
    name: 'auditorias',
    components: {
        AuditoriaComponente,
    },       
    data() {
        return {
            auditorias: {},
            meses_con_auditorias: [],
            auditorias_month: [
                {
                    "mes": 1,
                    "mes_nombre": "Enero",
                    "auditorias": []
                },
                {
                    "mes": 2,
                    "mes_nombre": "Febrero",                    
                    "auditorias": []
                },
                {
                    "mes": 3,
                    "mes_nombre": "Marzo",
                    "auditorias": []
                },
                {
                    "mes": 4,
                    "mes_nombre": "Abril",
                    "auditorias": []
                },
                {
                    "mes": 5,
                    "mes_nombre": "Mayo",
                    "auditorias": []
                },
                {
                    "mes": 6,
                    "mes_nombre": "Junio",
                    "auditorias": []
                },
                {
                    "mes": 7,
                    "mes_nombre": "Julio",
                    "auditorias": []
                },
                {
                    "mes": 8,
                    "mes_nombre": "Agosto",
                    "auditorias": []
                },
                {
                    "mes": 9,
                    "mes_nombre": "Septiembre",
                    "auditorias": []
                },
                {
                    "mes": 10,
                    "mes_nombre": "Octubre",
                    "auditorias": []
                },
                {
                    "mes": 11,
                    "mes_nombre": "Noviembre",
                    "auditorias": []
                },
                {
                    "mes": 12,
                    "mes_nombre": "Diciembre",
                    "auditorias": []
                },
            ],

        }
    },
    // YA CARGADO, LLAMAMOS A LA API
    mounted() {
        let vue = this
        axios.get('http://localhost:29596/api/auditorias')
        .then( function( response ) {
            vue.auditorias = response.data       
            vue.auditorias_month.forEach(function(item) {
                //var x = item.mes;
                for(let i = 0; i < vue.auditorias.length; i++) {
                    if( vue.auditorias[i].auditoriaMes == item.mes) {
                        item.auditorias.push(vue.auditorias[i]);
                    }                    
                }                
            });
            vue.auditorias_month.forEach(function(item) {
                //var x = item.mes;
                if (item.auditorias.length != 0) {
                    vue.meses_con_auditorias.push(item);
                }             
            });
        });        
    },
}

</script>


<style>
#title {
    text-align: center;
    margin-top: 30px;
    font-weight: bold;
}
#codigo_colores {
    float: right;
    margin-top: -60px;
}
</style>