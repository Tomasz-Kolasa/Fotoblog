<template>
  <div>
    <v-row align="center"
    justify="end">
      <v-btn
      color="indigo"
      outlined
      class="ma-4"
      link
      @click="$router.push('/add-new-tag')">
        Dodaj tag
      </v-btn>
    </v-row>
    <v-data-table
      :headers="headers"
      :items="tags"
      hide-default-footer
      :loading="loading"
    >
      <template v-slot:[`item.actions`]="{ item }">
        <v-row>
          <v-btn color="primary" class="mr-2" @click="$router.push(`/edit-tag/${item.id}/${item.name}`)">edytuj</v-btn>
          <v-btn
            color="error"
            @click="openRemoveDialog(item)"
          >
            usuń
          </v-btn>
        </v-row>
      </template>
    </v-data-table>


    <v-dialog
      v-if="dialog"
      v-model="dialog"
      persistent
      max-width="500"
    >
      <v-card>
        <v-card-title class="text-h5">
          Potwierdź
        </v-card-title>
        <v-card-text>Tag <strong>{{removeVm.name}}</strong> zostanie usunięty</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="primary"
            text
            @click="closeRemoveDialog()"
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
    name: 'tags-manager',
    data () {
      return {
        tags: [],
        headers: [
          {
            text: 'Id',
            value: 'id'
          },
          {
            text: 'nazwa taga',
            value: 'name'
          },
          {
            text: 'opcje',
            value: 'actions'
          }
        ],
        dialog: false,
        isDeleteTagBtnLoaderActive: false,
        removeVm: null,
        loading: false
      }
    },
    methods: {
      async getTags(){
        this.loading = true
        
        const response = await this.$http.get('Tags/GetAll')

        if(response && response.data.status){
          this.tags = response.data.data
        }

        this.loading = false
      },
      async deleteTag(){
        this.isDeleteTagBtnLoaderActive = true

        const response = await this.$http.delete('Tags/Remove/'+this.removeVm.id)
        
        if(response && response.data.status){
          this.getTags()
          this.dialog = false
        }

        this.isDeleteTagBtnLoaderActive = false
      },
      openRemoveDialog(item){
        this.removeVm = item
        this.dialog = true
      },
      closeRemoveDialog(){
        this.dialog = false
        this.removeVm = null
      }
    },
    mounted() {
      this.getTags();
    }
  }
</script>
