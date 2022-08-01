<template>
    <v-container>
        <v-row>
            <v-col>
                <h2>Zmień nazwę taga</h2>
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
                        @click="ChangeTagName"
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
    name: "EditTagView",
    data(){
        return {
            valid: false,
            tagName: "",
            tagId: 0,
            isSubmitBtnLoaderActive: false,
            tagRules: [
                v => !!v || 'Pole wymagane',
                v => (v.length >= 3 && v.length <= 15) || '3 - 15 zanków'
            ]
        }
    },
    methods: {
        ChangeTagName(){
            if(!this.valid)
            {
                return;
            }

            this.isSubmitBtnLoaderActive = true
            this.updateTag();
        },
        async updateTag(){

            var updatedTag = {
                id: this.tagId,
                newName: this.tagName
            }

            const response = await this.$http.put('Tags/Update', updatedTag)
            
            if(response.data && response.data.status){
                this.$toast.success('Sukces!')
                this.$router.push("/")
            }

            this.isSubmitBtnLoaderActive = false
      }
    },
    mounted()
    {
        this.tagName = this.$route.params.tagName
        this.tagId = this.$route.params.tagId
    }

}
</script>