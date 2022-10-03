<template>
  <v-container>
    <v-row>
      <v-col
        :cols="$vuetify.breakpoint.mobile?12:6"
      >
        <h2 class="text-h4 mt-5">Email weryfikacyjny</h2>
        <h3 class="text-h5 mt-5">wyślij ponownie email weryfikacyjny</h3>
        <p class="mt-5">jeśli istnieje konto z adresem email, który tutaj wpiszesz, wyślemy link aktywacyjny</p>
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
            @click="resendEmail()"
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
    name:'ResendEmailView',
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
      async resendEmail(){
        this.isLoading = true
        
        const response = await this.$http.post('Auth/ResendEmail',{},{
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