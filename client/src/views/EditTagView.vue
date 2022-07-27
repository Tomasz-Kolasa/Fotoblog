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
            isSubmitBtnLoaderActive: false,
            tagRules: [
                v => !!v || 'Pole wymagane',
                v => (v.length <= 15) || 'Maksymalnie 15 zanków'
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
                id: this.$route.params.tagId,
                newName: this.tagName
            }

            const { data } = await this.$http.put('Tags/Update', updatedTag)
            
            if(data.status && data.status == true){
                alert('Sukces!')
                this.$router.push("/")
            }
            else if(data.status){
                alert(data.status.errorCode)
            }

            this.isSubmitBtnLoaderActive = false
      }
    },
    mounted()
    {
        this.tagName = this.$route.params.tagName
    }

}
</script>