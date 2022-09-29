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
                    placeholder="login"
                    required
                    :counter="20"
                    :rules="rules"
                    autofocus
                >
                </v-text-field>
                <v-text-field
                    v-model="loginVm.password"
                    placeholder="hasło"
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
        v-if="setCredsDialog"
        v-model="setCredsDialog"
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
                        placeholder="utwórz login"
                        required
                        :counter="20"
                        :rules="rules"
                        autofocus
                    >
                    </v-text-field>
                    <v-text-field
                        v-model="credsVm.password"
                        placeholder="utwórz hasło"
                        :append-icon="isShowCredsPassword ? 'mdi-eye' : 'mdi-eye-off'"
                        required
                        :type="isShowCredsPassword?'text':'password'"
                        :counter="20"
                        :rules="rules"
                        @click:append="isShowCredsPassword = !isShowCredsPassword"
                    >
                    </v-text-field>
                    <v-text-field
                        v-model="credsVm.confirmPassword"
                        placeholder="powtórz hasło"
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
    </v-container>
</template>
<script>
    import { useUserStore } from '@/pinia/stores/useUserStore'
    import Axios from 'axios'

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

                setCredsDialog: false,
                isCredsFormValid: false,
                isCredsLoader: false,
                credsVm: {
                    userName: '',
                    password: '',
                    confirmPassword: ''
                },
                confirmPwdRules:[
                    v => !!v || 'Pole wymagane',
                    v => (v == this.credsVm.password) || 'Hasła muszą być identyczne'
                ],
                isShowCredsPassword: false
            }
        },
        methods:{
            async login(){
                var self = this

                this.isLoading = true
                const response = await this.$http.post('Auth/Login',this.loginVm).catch(
                    (response)=>{
                        response
                        self.isLoading = false})

                if(response && response.data.status)
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
                this.setCredsDialog = false
                this.isCredsLoader = false
                this.isLoading = false
                this.$refs.credsForm.reset()
                this.user.logout()
            },
            openDialog(){
                this.setCredsDialog = true
            },
            async SetAdminCreds(){
                this.isCredsLoader = true

                const response = await this.$http.post(
                    'Auth/RegisterAdmin',this.credsVm,{
                        headers:{
                            Authorization: `bearer ${this.user.token}`
                        }
                    }).catch((response)=>response)

                if(response && response.data.status)
                {
                    this.loginVm.userName = ''
                    this.loginVm.password = ''
                    this.closeDialog()
                    this.$toast.success("Super! Możesz już się logować!")
                }

                this.isCredsLoader = false
                this.isLoading = false
            }
        }
    }
</script>