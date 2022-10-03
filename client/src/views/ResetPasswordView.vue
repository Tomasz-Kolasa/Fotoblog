<template>
    <v-container>
      <v-row>
        <v-col
          :cols="$vuetify.breakpoint.mobile?12:6"
        >
          <h2 class="text-h4 mt-5">Zmiana hasła</h2>
          <p class="mt-5">wpisza adres email podany przy rejestracji a wyślemy Ci link umożliwiający zmianę hasła</p>
        </v-col>
        <v-col
          :cols="$vuetify.breakpoint.mobile?12:6"
          class="mt-5"
        >
          <v-form
            v-model="isFormValid"
            :disabled="isLoading"
          >
            <v-text-field
              v-model="email"
              :rules="emailRules"
              placeholder="wpisz email..."
              color="success"
            >
            </v-text-field>
            <v-btn
              :disabled="!isFormValid"
              :loading="isLoading"
              color="success"
              @click="resetPassword()"
              class="mt-3"
            >
              wyślij
            </v-btn>
          </v-form>
        </v-col>
      </v-row>
    </v-container>
  </template>
  <script>
    export default{
      name:'ResetPasswordView',
      data(){
        return{
          isFormValid: false,
          isLoading: false,
          email: '',
          emailRules:[
            v => !!v || 'Wpisz email',
            v => !!v.match(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/) || 'Wprowadź prawidłowy email'
          ]
        }
      },
      methods:{
        async resetPassword(){
          this.isLoading = true
          
          const response = await this.$http.post('Auth/SendResetPassword',{},{
            params:{
              email: this.email
            }
          }).catch(response=>response)
  
          if(response && response.data && response.data.status===true){
            this.$toast.success('Jeśli podany @ był prawidłowy wysłaliśmy link.')
          }
  
          this.isLoading = false
        }
      }
    }
  </script>