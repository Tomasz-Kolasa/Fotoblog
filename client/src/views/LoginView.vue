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
                    zapisz
                </v-btn>
            </v-form>
            </v-col>
        </v-row>
    </v-container>
</template>
<script>
    import { useUserStore } from '@/pinia/stores/useUserStore'
    import Axios from 'axios'

    export default {
        name: 'LoginView',
        beforeRouteEnter: (to, from, next)=>{
        var user = useUserStore()
        if(user.isLoggedIn)
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
                user: useUserStore()
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
                    Axios.defaults.headers = {
                        Authorization: `bearer ${token}`
                    }
                    this.$toast.success("Zalogowano!.")
                    this.$router.push("/")
                }
            }
        }
    }
</script>