<template>
  <div>
    <v-row align="center"
    justify="end">
      <v-btn
      color="indigo"
      outlined
      class="ma-4"
      @click="$router.push('/addNewTag')">
        Dodaj tag
      </v-btn>
    </v-row>
    <v-simple-table>
      <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left">id</th>
              <th class="text-left">nazwa</th>
              <th class="text-left">opcje</th>
            </tr>
          </thead>
          <tbody v-if="tags">
            <tr v-for="tag in tags" :key="tag.id">
              <td>{{tag.id}}</td>
              <td>{{tag.name}}</td>
              <td>
                <v-row>
                  <v-btn color="primary" class="mr-2" @click="goToEditPage(tag.id, tag.name)">edytuj</v-btn>
                  <v-btn
                    color="error"
                    @click.stop="openDialogAndRememberTagId(tag.name, tag.id)"
                  >
                    usuń
                  </v-btn>
                </v-row>
              </td>
            </tr>
          </tbody>
          <tbody v-else>
            <tr>
              <td colspan="100">
                <v-row
                  align="center"
                  justify="center"
                >
                  <p>wczytuję...</p>
                </v-row>
              </td>
            </tr>
          </tbody>
      </template>
    </v-simple-table>
    <v-dialog
      v-model="dialog"
      persistent
      max-width="290"
    >
      <v-card>
        <v-card-title class="text-h5">
          Potwierdź
        </v-card-title>
        <v-card-text>Tag <strong>{{tagNameToBeDeleted}}</strong> zostanie usunięty</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="primary"
            text
            @click="dialog = false"
          >
            Nie
          </v-btn>
          <v-btn
            color="error"
            :loading=isDeleteTagBtnLoaderActive
            text
            @click="deleteTag()"
          >
            Tak
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>

  export default {
    name: 'TagsComponent',
    data () {
      return {
        tags: undefined,
        dialog: false,
        isDeleteTagBtnLoaderActive: false,
        tagNameToBeDeleted: '',
        tagIdToBeDeleted: 0
      }
    },
    methods: {
      async getTags(){
        const { data } = await this.$http.get('Tags/GetAll')
        this.tags = data.data
      },
      goToEditPage(tagId, tagName)
      {
        this.$router.push("/editTag/"+tagId+"/"+tagName)
      },
      async deleteTag()
      {
        this.isDeleteTagBtnLoaderActive = true

        try{
            const { data } = await this.$http.delete('Tags/Remove/'+this.tagIdToBeDeleted)
            
            if(data.status && data.status == true){
              this.tags = undefined
              this.dialog = false
              this.getTags()
            }
            else if(data.status){
                alert(data.status.errorCode)
            }
        } catch{
            // exception thrown in axios.js
        } finally{
            this.isDeleteTagBtnLoaderActive = false
        }
      },
      openDialogAndRememberTagId(tagNameToBeDeleted, tagIdToBeDeleted)
      {
        this.tagNameToBeDeleted = tagNameToBeDeleted
        this.tagIdToBeDeleted = tagIdToBeDeleted
        this.dialog=true
      }
    },
    mounted() {
      this.getTags();
    }
  }
</script>
