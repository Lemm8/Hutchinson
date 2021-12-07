<template>
    <div class="container">
        <div v-if="auditoria_object.auditorias.length != 0" class="auditoria-title">
            <div class="auditoria-head">
                <i class="fa fa-calendar"></i>
                {{ auditoria_object.mes_nombre }}             
            </div>         
        </div>
        <div v-for="auditoria in auditoria_object.auditorias" :key="auditoria.id">
            <div class="auditor">            
                {{ auditoria.auditorNombre }}
                <i class="fa fa-plus" title="Añadir Auditoría"></i>
            </div>   
            <div class="tareas"> 
                <a href="#">Agregar auditoría</a>
            </div>    
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th scope="col">Id</th>
                        <th scope="col">Área</th>
                        <th scope="col"></th>                    
                        <th scope="col">Inicial</th>
                        <th scope="col">Target</th>
                        <th scope="col">Complete</th>
                        <th scope="col">Estado</th>
                        <th scope="col"></th>
                        <th scope="col"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row"> {{ auditoria.auditoriaId }} </th>                    
                        <td> {{ auditoria.area }} </td>                    
                        <td> <a href="#">Iniciar Auditoría</a> </td>
                                                                                    
                        <td> {{ auditoria.auditoriaFechaInicio }} </td>
                                    
                        <td v-if="auditoria.auditoriaFechaTarget"> 
                            {{ auditoria.auditoriaFechaTarget }}
                        </td>
                        <td v-else> 
                            Sin fecha
                        </td>

                        <td v-if="auditoria.auditoriaFechaCompleto"> 
                            {{ auditoria.auditoriaFechaCompleto }}
                        </td>
                        <td v-else> 
                            Sin fecha
                        </td>

                        <td v-if="auditoria.auditoriaEstado == 0">
                            <i class="fa fa-check-circle" aria-hidden="true" style="color: green"></i>
                        </td>

                        <td v-if="auditoria.auditoriaEstado == 1">
                            <i class="fa fa-check-circle" aria-hidden="true" style="color: red"></i>
                        </td>

                        <td v-if="auditoria.auditoriaEstado == 2">
                            <i class="fa fa-hourglass-half" aria-hidden="true" style="color: yellow"></i>
                        </td>

                        <td v-if="auditoria.auditoriaEstado == 3">
                            <i class="fa fa-hourglass-half" aria-hidden="true" style="color: red"></i>
                        </td>

                        <td> <a href="#"> <i class="fa fa-edit" title="Editar"></i> </a> </td>
                        <td> <a href="#"> <i @click="deleteAuditoria(auditoria.auditoriaId)" class="fa fa-trash" title="Borrar" ></i> </a> </td>
                    </tr>                
                </tbody>
            </table>        
        </div>
    </div>
</template>



<script>

import axios from 'axios'

export default {
    name: 'auditoria_componente',
    props: [
        'auditoria_object',
    ],
    methods: {
        deleteAuditoria(id){
            var r = confirm("¿Estás seguro de borrar la auditoría?");
            if ( r === true ) {
                axios.delete(`http://localhost:29596/api/auditorias/${id}`)
                .then( function( ) {
                    location.reload();
                });
            }            
        }
    }
}

</script>


<style>
.container {
    margin-bottom: 60px;
}
.auditoria-title {
    text-align: left;    
    margin-top: 30px;
    border-bottom: solid;
    border-bottom-color: #172646;
    font-size: 20px;    
}
.fa-calendar{
    color: #172646;
}

table {
    width: 100%;
}

thead {
    background-color: #172646;
    color: #ffffff;
}
.fa-plus {
    margin-left: 40px;
    margin-top: 4px;
    color: #172646;
}
.fa-plus:hover {
    margin-left: 40px;
    margin-top: 4px;
    color: black;
}
.auditor {    
    display: flex;
    justify-content: center;
    font-size: 18px;
    margin-top: 20px;    
}
.auditoria-head {
    margin-left: 10px;
}

</style>

