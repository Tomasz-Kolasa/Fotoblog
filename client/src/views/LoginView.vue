<template>
    <v-container>
        <v-row>
            <v-col
            :cols="$vuetify.breakpoint.name=='xs'?12:6"
            class="d-flex align-center justify-center"
            >
                <h2 class="text-h4">zaloguj się</h2>
            </v-col>
            <v-col :cols="$vuetify.breakpoint.name=='xs'?12:6">
                <v-form
                ref="form"
                v-model="isFormValid"
                :disabled="isLoading"
            >
                <v-text-field
                    v-model="loginVm.userName"
                    placeholder="login..."
                    required
                    :counter="20"
                    :rules="rules"
                    autofocus
                >
                </v-text-field>
                <v-text-field
                    v-model="loginVm.password"
                    placeholder="hasło..."
                    :append-icon="isShowPassword ? 'mdi-eye' : 'mdi-eye-off'"
                    required
                    :type="isShowPassword?'text':'password'"
                    :counter="20"
                    :rules="rules"
                    @click:append="isShowPassword = !isShowPassword"
                >
                </v-text-field>
                <v-btn
                    color="success"
                    outlined
                    @click="login()"
                    :disabled="!isFormValid"
                    :loading="isLoading"
                    class="mt-3"
                    >
                    zaloguj
                </v-btn>
            </v-form>
            </v-col>
        </v-row>

        <v-dialog
        v-if="credsDialog"
        v-model="credsDialog"
        persistent
        max-width="800"
        >
        <v-card>
            <v-card-title class="text-h5">
                Ustawianie nowego konta
            </v-card-title>
            <v-card-subtitle>
                <p class="my-3">Ustaw login i hasło, aby móc zarządzać swoim serwisem</p>
            </v-card-subtitle>
            <v-card-text>
                <v-form
                    ref="credsForm"
                    v-model="isCredsFormValid"
                    :disabled="isCredsLoader"
                >
                    <v-text-field
                        v-model="credsVm.userName"
                        placeholder="utwórz login..."
                        required
                        :counter="20"
                        :rules="rules"
                        autofocus
                    >
                    </v-text-field>
                    <v-text-field
                        v-model="credsVm.email"
                        placeholder="twój email..."
                        required
                        :counter="20"
                        :rules="emailRules"
                    >
                    </v-text-field>
                    <v-text-field
                        v-model="credsVm.password"
                        placeholder="utwórz hasło..."
                        :append-icon="isShowCredsPassword ? 'mdi-eye' : 'mdi-eye-off'"
                        required
                        :type="isShowCredsPassword?'text':'password'"
                        :counter="20"
                        :rules="rules"
                        @click:append="isShowCredsPassword = !isShowCredsPassword"
                    >
                    </v-text-field>
                    <v-text-field
                        class="mb-3"
                        v-model="credsVm.confirmPassword"
                        placeholder="powtórz hasło..."
                        :append-icon="isShowCredsPassword ? 'mdi-eye' : 'mdi-eye-off'"
                        required
                        :type="isShowCredsPassword?'text':'password'"
                        :counter="20"
                        :rules="confirmPwdRules"
                        @click:append="isShowCredsPassword = !isShowCredsPassword"
                    >
                    </v-text-field>
                    <v-btn
                        color="success"
                        outlined
                        @click="SetAdminCreds()"
                        :disabled="!isCredsFormValid"
                        :loading="isCredsLoader"
                        >
                        utwórz
                    </v-btn>
                    <v-btn
                        class="ml-3"
                        color="error"
                        outlined
                        @click="closeDialog()"
                    >
                        anuluj
                    </v-btn>
                </v-form>
            </v-card-text>
            </v-card>
        </v-dialog>
        <InfoDialog :visible.sync="isInfoDialog_notSentEmail">
          <template v-slot:title>
            Hmmm...
          </template>
          <template v-slot:text>
            <p>Utworzyliśmy konto, ale nie udało się wysłać @ z linkiem aktywacyjnym.</p>
            <p>Możesz spróbować wysłać go ponownie <router-link to="/resend-email">tutaj</router-link>,</p>
            <p>lub</p>
            <p>zalogować się za pomocą nowo utworzonego konta i spróbować wysłać później.</p>
          </template>
        </InfoDialog>

        <InfoDialog :visible.sync="isInfoDialog_unconfirmedEmail">
          <template v-slot:title>
            Potwierdź email
          </template>
          <template v-slot:text>
            <p>Podczas rejstracji konta wysłaliśmy Ci email z linkiem aktywacyjnym, ale najwyraźniej nie kliknąłeś go jeszcze.</p>
            <p>Jeśli nie otrzymałeś tego maila, możesz ponownie wysłać go <router-link to="/resend-email">tutaj</router-link>.</p>
          </template>
        </InfoDialog>
    </v-container>
</template>
<script>
    import { useUserStore } from '@/pinia/stores/useUserStore'
    import Axios from 'axios'
    import InfoDialog from '@/components/InfoDialog.vue'

    export default {
        name: 'LoginView',
        beforeRouteEnter: (to, from, next)=>{
          var user = useUserStore()
          if(user.isAdmin)
          {
              next({name:'HomeView'})
          }
          next()
        },
        components: {
          InfoDialog
        },
        data(){
            return{
                loginVm:{
                    userName: '',
                    password: ''
                },
                isFormValid: false,
                isLoading: false,
                isShowPassword: false,
                rules: [
                    v => !!v || 'Pole wymagane',
                    v => (v.length>=6 && v.length<=20) || '6-20 znaków'
                ],
                user: useUserStore(),

                credsDialog: false,
                isCredsFormValid: false,
                isCredsLoader: false,
                credsVm: {
                    userName: '',
                    email:'',
                    password: '',
                    confirmPassword: ''
                },
                confirmPwdRules:[
                    v => !!v || 'Pole wymagane',
                    v => (v == this.credsVm.password) || 'Hasła muszą być identyczne'
                ],
                emailRules:[
                    v => !!v || 'Pole wymagane',    
                    v => !!v.match(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/) || 'Wprowadź prawidłowy email'
                ],
                isShowCredsPassword: false,

                isInfoDialog_notSentEmail: false,
                isInfoDialog_unconfirmedEmail: false,
            }
        },
        methods:{
            async login(){
                var self = this

                this.isLoading = true
                const response = await this.$http.post('Auth/Login',this.loginVm).catch(
                    (response)=>{
                        if(response.data && 43==response.data.errorCode)
                        {
                          this.isInfoDialog_unconfirmedEmail = true
                        }
                        self.isLoading = false})

                if(response && response.data.status===true)
                {
                    const token = response.data.data
                    this.user.login(token)

                    if(this.user.isTempAdmin)
                    {
                        this.openDialog()
                    }
                    else
                    {
                        Axios.defaults.headers = {
                            Authorization: `bearer ${token}`
                        }
                        this.$toast.success("Zalogowano!.")
                        this.$router.push("/")
                    }
                }
            },
            closeDialog(){
                this.credsDialog = false
                this.isCredsLoader = false
                this.isLoading = false
                this.$refs.credsForm.reset()
                this.user.logout()
            },
            openDialog(){
                this.credsDialog = true
            },
            async SetAdminCreds(){
              var isEmailConfirmationLinkNotSent = false
              this.isCredsLoader = true

              const response = await this.$http.post(
                  'Auth/RegisterAdmin',this.credsVm,{
                      headers:{
                          Authorization: `bearer ${this.user.token}`
                      }
                  }).catch(function (response) {

                      if(response.data)
                      {   
                          isEmailConfirmationLinkNotSent = (42 == response.data.errorCode) ? true:false
                      }
                  })

                if(response && response.data.status===true)
                {
                  this.loginVm.userName = ''
                  this.loginVm.password = ''
                  var name = this.credsVm.userName
                  this.closeDialog()
                  this.$toast.success(`Super, ${name}, zapisaliśmy wszystko.`)
                  this.$toast.success('Wysłaliśmy Ci także @ z linkiem aktywacyjnym.',{duration: 0})
                }

                if(isEmailConfirmationLinkNotSent){
                  this.loginVm.userName = ''
                  this.loginVm.password = ''
                  this.closeDialog()

                  this.isInfoDialog_notSentEmail = true
                }

                this.isCredsLoader = false
                this.isLoading = false
            }
        }
    }
</script>