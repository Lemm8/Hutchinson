<template>
    <div>
        <v-dialog
            v-model="dialog"
            width="400"
            persistent
            >

            <v-card>
                <v-card-title
                class="headline grey lighten-2"
                primary-title
                >
                    Autenticación
                </v-card-title>

                <v-card-text>
                    <p class="mt-5">Para ingresar puede utilizar su usuario XA, Windows o Local</p>
                    <v-form ref="form" v-model="valid" lazy-validation>
                        <v-text-field
                        v-model="username"
                        label="Usuario"
                        :rules="requiredRules"
                        required
                        ></v-text-field>

                        <v-text-field
                        v-model="password"
                        label="Contraseña"
                        type="password"
                        :rules="requiredRules"
                        required
                        ></v-text-field>
                    </v-form>

                    <div class="red--text" v-if="error">{{error}}</div>

                </v-card-text>

                <v-divider></v-divider>
    
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn text @click="close(false)">Cancelar</v-btn>
                    <v-btn color="primary" text @click="auth()">Ingresar</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>


<script>
  export default {
    props: ['dialog'],
    data () {
      return {
        loading: false,
        username:'',
        password:'',
        error:'',
        valid: true,
        saving:false,
        requiredRules: [
            v => !!v || 'Campo requerido', 
        ]
      }
    },
    methods: {
        async auth() {

            if (!this.$refs.form.validate()) 
                return;
            
            this.loading = true;

            try {
                
                await this.$store.dispatch('auth/login', { username: this.username, password: this.password })

                this.close()

                this.error = '';
                
            } catch (err) {
                
                let text = 'Error interno. Comuniquese con sistemas';

                if(err.data)
                    this.error = err.data.message;
                else
                    this.error = text
            }
            finally {

                this.loading = false;
            }


        },

        close(){

            this.$refs.form.reset()
            this.menu = false
            this.error = ''

            this.$emit('close')

        }

        
    }
  }
</script>