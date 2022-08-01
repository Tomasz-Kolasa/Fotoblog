<template>
    <v-container>
        <v-row>
            <v-col>
                <h2>Dodaj nowy tag</h2>
                <v-form
                    ref="form"
                    v-model="valid"
                >
                    <v-text-field
                        v-model="tagName"
                        required
                        :counter="15"
                        :rules="tagRules"
                        autofocus
                    >
                    </v-text-field>
                    <v-btn
                        color="warning"
                        @click="AddNewTag"
                        :disabled="!valid"
                        :loading="isSubmitBtnLoaderActive"
                        >
                        zapisz
                    </v-btn>
                    <v-btn
                        class="ml-3"
                        color="error"
                        @click="$router.push('/')"
                    >
                        anuluj
                    </v-btn>
                </v-form>
            </v-col>
        </v-row>
    </v-container>
</template>
<script>
export default {
    name: "AddNewTagView",
    data(){
        return {
            valid: false,
            tagName: "",
            isSubmitBtnLoaderActive: false,
            tagRules: [
                v => !!v || 'Pole wymagane',
                v => (v.length >= 3 && v.length <= 15) || '3 - 15 zanków'
            ]
        }
    },
    methods: {
        AddNewTag(){
            if(!this.valid)
            {
                return;
            }

            this.isSubmitBtnLoaderActive = true
            this.addTag();
        },
        async addTag(){

            var newTag = {
                name: this.tagName
            }

            const response = await this.$http.post('Tags/AddNew', newTag)

            if(response && response.data.status){
                this.$toast.success('Sukces!')
                this.$router.push("/")
            }

            this.isSubmitBtnLoaderActive = false
      }
    }
}
</script>