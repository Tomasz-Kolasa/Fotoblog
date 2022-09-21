<template>
  <div>
    <v-row align="center"
    justify="end">
      <v-btn
      color="indigo"
      class="ma-4"
      outlined
      @click="openAddDialog()">
        Dodaj tag
      </v-btn>
    </v-row>
    <v-data-table
      :headers="headers"
      :items="tags"
      hide-default-footer
      :loading="tableLoading"
    >
      <template v-slot:[`item.actions`]="{ item }">
        <v-row>
          <v-btn color="primary"
            outlined
            class="mr-2"
            @click="openEditDialog(item)">
              edytuj
          </v-btn>
          <v-btn
            outlined
            color="error"
            @click="openRemoveDialog(item)"
          >
            usuń
          </v-btn>
        </v-row>
      </template>
    </v-data-table>

    <v-dialog
      v-if="removeDialog"
      v-model="removeDialog"
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
            color="success"
            outlined
            @click="closeRemoveDialog()"
          >
            Nie
          </v-btn>
          <v-btn
            color="error"
            outlined
            :loading=isRemoveTagBtnLoaderActive
            @click="deleteTag()"
          >
            Tak
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-dialog
      v-if="editDialog"
      v-model="editDialog"
      persistent
      max-width="800"
    >
      <v-card>
        <v-card-title class="text-h5">
          Edytuj tag
        </v-card-title>
          <v-card-text>
            <v-form
                ref="form"
                v-model="isEditFormValid"
                :disabled="isEditTagBtnLoaderActive"
            >
                <v-text-field
                    v-model="editVm.name"
                    required
                    :counter="15"
                    :rules="tagRules"
                    autofocus
                >
                </v-text-field>
                <v-btn
                    color="success"
                    outlined
                    @click="changeTagName()"
                    :disabled="!isEditFormValid"
                    :loading="isEditTagBtnLoaderActive"
                    >
                    zapisz
                </v-btn>
                <v-btn
                    class="ml-3"
                    color="error"
                    outlined
                    @click="closeEditDialog()"
                >
                    anuluj
                </v-btn>
            </v-form>
          </v-card-text>
        </v-card>
    </v-dialog>

    <v-dialog
      v-if="addDialog"
      v-model="addDialog"
      persistent
      max-width="800"
    >
      <v-card>
        <v-card-title class="text-h5">
          Dodaj tag
        </v-card-title>
          <v-card-text>
            <v-form
                ref="form"
                v-model="isAddFormValid"
            >
                <v-text-field
                    v-model="addVm.name"
                    required
                    :counter="15"
                    :rules="tagRules"
                    autofocus
                >
                </v-text-field>
                <v-btn
                    color="success"
                    outlined
                    @click="addTag()"
                    :disabled="!isAddFormValid"
                    :loading="isAddSubmitBtnLoaderActive"
                    >
                    zapisz
                </v-btn>
                <v-btn
                    class="ml-3"
                    color="error"
                    outlined
                    @click="closeAddDialog()"
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
        removeDialog: false,
        isRemoveTagBtnLoaderActive: false,
        removeVm: null,

        editDialog: false,
        isEditTagBtnLoaderActive: false,
        editVm: null,
        isEditFormValid: false,
        tagRules: [
            v => !!v || 'Pole wymagane',
            v => (v.length >= 3 && v.length <= 15) || '3 - 15 zanków'
        ],

        addDialog: false,
        isAddSubmitBtnLoaderActive: false,
        addVm: null,
        isAddFormValid: false,

        tableLoading: false
      }
    },
    methods: {
      async getTags(){
        this.tableLoading = true
        
        const response = await this.$http.get('Tags/GetAll').catch((response)=>{response})

        if(response && response.data.status){
          this.tags = response.data.data
        }

        this.tableLoading = false
      },
      async deleteTag(){
        this.isRemoveTagBtnLoaderActive = true

        const response = await this.$http.delete('Tags/Remove/'+this.removeVm.id).catch((response)=>{response})
        
        if(response && response.data.status){
          this.getTags()
          this.removeDialog = false
          this.$toast.success('Sukces!')
        }

        this.isRemoveTagBtnLoaderActive = false
      },
      async changeTagName(){
        this.isEditTagBtnLoaderActive = true

        var updatedTag = {
            id: this.editVm.id,
            newName: this.editVm.name
        }

        const response = await this.$http.put('Tags/Update', updatedTag).catch((response)=>{response})
        
        if(response && response.data.status){
            this.$toast.success('Sukces!')
            this.getTags()
        }

        this.isEditTagBtnLoaderActive = false
        this.closeEditDialog()
      },
      async addTag(){
        this.isAddSubmitBtnLoaderActive = true

          const response = await this.$http.post('Tags/AddNew', this.addVm).catch((response)=>{response})

          if(response && response.data.status){
              this.$toast.success('Sukces!')
              this.getTags()
          }

          this.isAddSubmitBtnLoaderActive = false
          this.closeAddDialog();
      },
      openRemoveDialog(item){
        this.removeVm = item
        this.removeDialog = true
      },
      closeRemoveDialog(){
        this.removeDialog = false
        this.removeVm = null
      },
      openEditDialog(item){
        this.editVm = {...item}
        this.editDialog = true
      },
      closeEditDialog(){
        this.editDialog = false
        this.editVm = null
      },
      openAddDialog(){
        this.addVm = { name:''}
        this.addDialog = true
      },
      closeAddDialog(){
        this.addDialog = false
        this.addVm = null
      }
    },
    mounted() {
      this.getTags();
    }
  }
</script>
