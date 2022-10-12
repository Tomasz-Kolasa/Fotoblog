<template>
    <v-container>
        <v-row class="mt-16">
            <v-col>
                <h2 class="tex-h2 mb-4 text-center">
                    <v-icon>mdi-cog-outline</v-icon>
                    ustawienia</h2>

                <v-list>
                    <v-list-group
                    :value="false"
                    prepend-icon="mdi-account-cog-outline"
                    >
                        <template v-slot:activator>
                            <v-list-item-title>Ustawienia konta</v-list-item-title>
                        </template>
                        <v-list-item>
                            <v-list-item-content>
                                <change-password></change-password>
                            </v-list-item-content>
                        </v-list-item>
                    </v-list-group>
                </v-list>
            </v-col>
        </v-row>
    </v-container>
</template>
<script>
    import { useUserStore } from '@/pinia/stores/useUserStore'
    import changePassword from '@/components/settings/account-settings/change-password.vue'
    export default{
        name: 'SettingsView',
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
                
            }
        },
        components:{
            'change-password':changePassword
        }
    }
</script>