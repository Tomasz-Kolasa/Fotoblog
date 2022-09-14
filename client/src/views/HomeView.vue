<template>
    <v-container>
      <v-row v-if="isLoadingPhotos">
        <v-col
        v-for="n in 12"
        :key="n"
        cols="6"
        class="d-flex child-flex"
        >
          <v-skeleton-loader
          class="mb-6"
          elevation="2"
          type="image, article"
          ></v-skeleton-loader>
        </v-col>
      </v-row>
      <v-row>  
        <v-col v-if="isNoPhotosToShow">
          <h2 class="text-h4 text-center">brak zdjęć</h2>
        </v-col>
        <photo-item
          v-for="photo in photos"
          :key="photo.id"
          :photo="photo"
          @delete-photo="openDeleteDialog(photo)"
          @edit-photo="openEditDialog(photo)"
        >
        </photo-item>
      </v-row>
      <v-dialog
        v-if="deleteDialog"
        v-model="deleteDialog"
        persistent
        max-width="500"
      >
        <v-card>
          <v-card-title class="text-h5">
            Potwierdź
          </v-card-title>
          <v-card-text>Zdjęcie zostanie usunięte</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              color="success"
              outlined
              @click="closeDeleteDialog()"
            >
              Nie
            </v-btn>
            <v-btn
              color="error"
              outlined
              :loading="isDeletingPhoto"
              @click="deletePhoto(deleteVm.id)"
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
            Edytuj zdjęcie
          </v-card-title>
            <v-card-text>
              <v-form
                  ref="form"
                  v-model="isEditFormValid"
              >
                <v-row align="center" class="my-3">
                  <v-progress-linear
                    v-if="isLoadingTags"
                    indeterminate
                    color="green"
                  ></v-progress-linear>

                  <v-checkbox
                    v-for="tag in allEditTags"
                    :key="tag.id"
                    v-model="allEditSelectedTags"
                    :label="tag.name"
                    color="indigo"
                    :value="tag.id"
                    hide-details
                    class="ml-3"
                  ></v-checkbox>
                </v-row>
                  <v-text-field
                      v-model="editVm.title"
                      required
                      :counter="15"
                      :rules="titleRules"
                      autofocus
                  >
                  </v-text-field>
                  <v-text-field
                      v-model="editVm.description"
                      :counter="30"
                      :rules="descriptionRules"
                  >
                  </v-text-field>
                  <div class="d-flex justify-end my-4">
                    <v-btn
                        color="error"
                        outlined
                        @click="closeEditDialog()"
                    >
                        anuluj
                    </v-btn>
                    <v-btn
                      color="success"
                      outlined
                      class="ml-3"
                      @click="changePhotoDetails()"
                      :disabled="!isEditFormValid"
                      :loading="isEditingPhoto"
                      >
                      zapisz
                    </v-btn>
                  </div>
              </v-form>
            </v-card-text>
          </v-card>
      </v-dialog>
    </v-container>
</template>

<script>
import PhotoItem from '@/components/photo-item/PhotoItem.vue'

export default {
    name: "HomeView",
    data(){
      return {
        photos: [],
        isLoadingPhotos: true,
        
        deleteDialog: false,
        isDeletingPhoto: false,
        deleteVm: null,

        editDialog: false,
        isEditingPhoto: false,
        editVm: null,
        isEditFormValid: false,
        allEditTags: [],
        allEditSelectedTags: [],
        isLoadingTags:false,
        titleRules: [
          v => !!v || 'Pole wymagane',
          v => (v.length>=2 && v.length<=15) || '2-15 znaków'
        ],
        descriptionRules: [
          v => (v.length<=30) || 'max 30 znaków'
        ],
      }
    },
    methods: {
      async getAllPhotos(){
        const response = await this.$http.get('Photos/GetAll').catch((response)=>{response})

        if(response && response.data.status){
          this.photos = response.data.data
        }
        this.isLoadingPhotos = false
      },
      removePhoto(id){
        this.photos = this.photos.filter(p => p.id != id)
      },
      async deletePhoto(id){
        this.isDeletingPhoto = true

        const self = this
        
        const response = await this.$http.delete('photos/delete?id='+id)
            .catch(function (response) {
                if(response && response.data)
                {
                    var errorCode = response.data.errorCode
                    if(30 == errorCode) // photo not exists
                    {
                        self.removePhoto(id)
                    }
                }
            })

        if(response && response.data.status)
        {
          this.removePhoto(id)
          this.$toast.success("Zdjęcie zostało usunięte.")
        }
        
        this.isDeletingPhoto = false
        this.deleteDialog = false
      },
      openDeleteDialog(photo){
        this.deleteVm = photo
        this.deleteDialog = true
      },
      closeDeleteDialog(){
        this.deleteDialog = false
        this.deleteVm = null
      },
      openEditDialog(photo){
        this.editVm = {...photo}
        this.isLoadingTags = true
        this.getTags()
        this.editDialog = true
      },
      closeEditDialog(){
        this.editDialog = false
        this.editVm = null
      },
      async getTags(){
        const response = await this.$http.get('Tags/GetAll').catch((response)=>{response})

        if(response && response.data.status){
          this.allEditTags = response.data.data
          this.checkCheckboxesWithCurrentTags()
        }

        this.isLoadingTags = false
      },
      checkCheckboxesWithCurrentTags(){
          for(var photoTag of this.editVm.tags){
            this.allEditSelectedTags.push(photoTag.id)
          }
      }
    },
    computed: {
      isNoPhotosToShow(){
        return !this.isLoadingPhotos && this.photos.length==0
      }
    },
    mounted(){
      this.getAllPhotos()
    },
    components: {
      'photo-item': PhotoItem
    }
  }
</script>