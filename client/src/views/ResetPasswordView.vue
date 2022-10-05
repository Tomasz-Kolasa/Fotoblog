<template>
    <v-container>
      <v-row v-if="isSendLinkView">
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
              v-model="emailAddress"
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
      <v-row v-if="isChangePasswordView">
        <v-col
          :cols="$vuetify.breakpoint.name=='xs'?12:6"
          class="d-flex align-center justify-center"
        >
          <h2 class="text-h4">zmień hasło</h2>
        </v-col>
        <v-col :cols="$vuetify.breakpoint.name=='xs'?12:6">
          <v-form
            v-model="isValid"
            :disabled="isLoading"
          >
            <v-text-field
              v-model="changePwdVm.newPwd"
              placeholder="nowe hasło..."
              :rules="rules"
              :counter="20"
              :type="isShowPassword?'text':'password'"
              :append-icon="isShowPassword ? 'mdi-eye' : 'mdi-eye-off'"
              @click:append="isShowPassword = !isShowPassword"
            ></v-text-field>
            <v-text-field
              class="mb-3"
              v-model="changePwdVm.confirmNewPwd"
              placeholder="powtórz nowe hasło..."
              :rules="confirmPwdRules"
              :counter="20"
              :type="isShowPassword?'text':'password'"
              :append-icon="isShowPassword ? 'mdi-eye' : 'mdi-eye-off'"
              @click:append="isShowPassword = !isShowPassword"
            ></v-text-field>
            <v-btn
              color="success"
              outlined
              @click="changePassword()"
              :disabled="!isValid"
              :loading="isChangingPwd"
              >
              zmień
          </v-btn>
          </v-form>
        </v-col>
      </v-row>
    </v-container>
  </template>
  <script>
    export default{
      name:'ResetPasswordView',
      props:['email','token'],
      data(){
        return{
          isSendLinkView: true,
          isFormValid: false,
          isLoading: false,
          emailAddress: '',
          emailRules:[
            v => !!v || 'Wpisz email',
            v => !!v.match(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/) || 'Wprowadź prawidłowy email'
          ],

          isChangePasswordView: false,
          isValid: false,
          isChangingPwd: false,
          dialog: false,
          changePwdVm:{
            email: '',
            token: '',
            newPwd:'',
            confirmNewPwd:''
          },
          isShowPassword: false,
          rules: [
                  v => !!v || 'Pole wymagane',
                  v => (v.length>=6 && v.length<=20) || '6-20 znaków'
          ],
          confirmPwdRules:[
                  v => !!v || 'Pole wymagane',
                  v => (v == this.changePwdVm.newPwd) || 'Hasła muszą być identyczne'
          ],
        }
      },
      methods:{
        async resetPassword(){
          this.isLoading = true
          
          const response = await this.$http.post('Auth/SendResetPassword',{},{
            params:{
              email: this.emailAddress
            }
          }).catch(response=>response)
  
          if(response && response.data && response.data.status===true){
            this.$toast.success('Jeśli podany @ był prawidłowy wysłaliśmy link.')
          }
  
          this.isLoading = false
        },
        async changePassword(){
          this.isChangingPwd = true

          this.changePwdVm.email = this.email
          this.changePwdVm.token = this.token
          
          const response = await this.$http.post('Auth/ResetPassword',this.changePwdVm).catch(response=>response)
  
          if(response && response.data && response.data.status===true){
            this.$toast.success('Nowe hasło zostało nadane.')
            this.$toast.success('Możesz się zalogować.')
            this.$router.push('/login')
          }
  
          this.isChangingPwd = false
        }
      },
      beforeMount(){
        this.isSendLinkView = (this.email && this.token)?false:true
        this.isChangePasswordView = !this.isSendLinkView
      }
    }
  </script>