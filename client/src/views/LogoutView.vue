<template>
    <v-container>
        <v-row>
            <v-col class="pt-16">
                <h4 class="text-h5 text-center">do zobaczenia...</h4>
            </v-col>
        </v-row>
    </v-container>
</template>
<script>
    import { useUserStore } from '@/pinia/stores/useUserStore'
    import Axios from 'axios'

    export default {
        name: 'LogoutView',
        beforeRouteEnter: (to, from, next)=>{
        var user = useUserStore()
        if(!user.isAdmin)
        {
            next({name:'HomeView'})
        }
        next()
        },
        data(){
            return{
                user: useUserStore()
            }
        },
        methods: {
            logout(){
                this.user.logout()
                delete Axios.defaults.headers.Authorization
                this.$toast.success('Wylogowano.')
                setTimeout(()=>{
                    this.$router.push({name:'HomeView'})
                }, 2000)

            }
        },
        mounted(){
            this.logout()
        }
    }
</script>