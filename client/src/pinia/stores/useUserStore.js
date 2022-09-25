import { defineStore } from 'pinia'

export const useUserStore = defineStore('user',{
    state: () => ({
        id: null,
        name: null,
        role: null,
        token: null
    }),
    getters: {
        isLoggedIn: (state) => !!state.token
    },
    actions: {
        login(token){
            this.token = token
        },
        logout(){
            this.userId = null,
            this.userName = null,
            this.userRole = null,
            this.token = null
        }
    }
})