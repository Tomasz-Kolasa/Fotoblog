<template>
  <v-container>
    <v-row>
      <v-col v-if="isLoading">
        <h2 class="text-h2 my-10">Hello</h2>
        <h3 class="text-h4">
          potwierdzam email  <span class="text-h3">{{email}}</span>
          <v-progress-circular
            indeterminate
            :size="60"
            color="indigo"
            class="ml-5 mb-2"
          ></v-progress-circular>
        </h3>
      </v-col>
      <v-col v-else>
        <p class="text-h4">{{result}}</p>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
    export default{
        name:'ConfirmEmailView',
        props:['email','token'],
        data(){
            return{
              isLoading: true,
              result:'Nie udało się potwierdzić adresu email.'
            }
        },
        methods:{
          async confirmEmail(){
            var confirm = {
              email: this.email,
              token: this.token
            }
            var result = await this.$http.post('Auth/ConfirmEmail', confirm).catch(response=>{response})

            if(result && result.data.status===true){
              this.result = 'Możesz się już zalogować'
              this.$toast.success('Adres email został potwierdzony')
            }

            this.isLoading = false
          }
        },
        mounted(){
          this.confirmEmail()
        }
    }
</script>