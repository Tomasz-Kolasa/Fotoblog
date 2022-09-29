<template>
  <div>
    <v-btn
      text
      plain
      @click="openDialog()"
    >
      zmień hasło
    </v-btn>
    <v-dialog
      v-if="dialog"
      v-model="dialog"
      persistent
      max-width="800">
      <v-card>
        <v-card-title
          class="text-h5">
            Zmień hasło
        </v-card-title>
        <v-card-text>
          <v-form
            v-model="isValid"
            :disabled="isLoading"
          >
            <v-text-field
              v-model="changePwdVm.oldPwd"
              placeholder="stare hasło..."
              autofocus
              :rules="rules"
              :counter="20"
              :type="isShowPassword?'text':'password'"
              :append-icon="isShowPassword ? 'mdi-eye' : 'mdi-eye-off'"
              @click:append="isShowPassword = !isShowPassword"
            ></v-text-field>
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
              @click="changePwd()"
              :disabled="!isValid"
              :loading="isLoading"
              >
              zmień
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
  </div>
</template>
<script>
  import { useUserStore } from '@/pinia/stores/useUserStore'

  export default{
      name: 'change-password',
      data(){
        return{
          isValid: false,
          isLoading: false,
          dialog: false,
          changePwdVm:{
            userId: 0,
            oldPwd:'',
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
          user: useUserStore()
        }
      },
      methods:{
        openDialog(){
          this.dialog = true
        },
        closeDialog(){
          this.dialog = false
          this.changePwdVm={
            oldPwd:'',
            newPwd:'',
            confirmNewPwd:''
          }
        },
        async changePwd(){
          this.isLoading = true
          this.changePwdVm.userId = this.user.id
          
          const response = await this.$http.put('Settings/ChangePassword',this.changePwdVm)
            .catch(response=>response)
          
          if(response && response.data.status===true){
            this.closeDialog()
            this.$toast.success('Ok, hasło zmienione.')
          }
          
          this.isLoading = false
        }
      }
  }
</script>