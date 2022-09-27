import { defineStore } from 'pinia'
import jwt_decode from 'jwt-decode'

export const useUserStore = defineStore('user',{
    state: () => ({
        id: null,
        name: null,
        role: null,
        token: null
    }),
    getters: {
        isLoggedIn: (state) => !!state.token,
        isAdmin: (state) => !!state.token && state.role=='Admin',
        isTempAdmin: (state) => !!state.token && state.role=='TempAdmin'
    },
    actions: {
        login(token){
            this.token = token

            const tokenDecoded = jwt_decode(token)
            this.id = tokenDecoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]
            this.name = tokenDecoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
            this.role = tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
        },
        logout(){
            this.id = null,
            this.name = null,
            this.role = null,
            this.token = null
        }
    }
})