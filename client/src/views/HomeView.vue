<template>
  <div>
    <v-container>
      <v-row>
        <v-col class="pt-10 text-subtitle1 text-sm-h6 font-weight-thin font-italic">
          <q>Nie istnieją reguły opisujące dobrą fotografię,<br>są tylko dobre fotografie</q>
        </v-col>
      </v-row>
    </v-container>
    <v-container fluid class="px-5">
      <tags-bar :selected.sync="selectedTags"></tags-bar>
      <v-row v-if="isLoadingPhotos">
        <v-col
        v-for="n in 12"
        :key="n"
        cols="12"
        lg="6"
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
          <h2 class="mt-16 text-h5 font-weight-thin text-center">brak zdjęć</h2>
        </v-col>
        <photo-item
          v-for="photo in filteredPhotos"
          :key="photo.id"
          :photo="photo"
          @delete-photo="openDeleteDialog(photo)"
          @edit-photo="openUpdateDialog(photo)"
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
              @click="deletePhoto()"
            >
              Tak
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>

      <v-dialog
        v-if="updateDialog"
        v-model="updateDialog"
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
                  v-model="isUpdateFormValid"
                  :disabled="isUpdatingPhoto"
              >
              
                <div>
                  <v-menu
                    ref="menu"
                    v-model="menu"
                    :close-on-content-click="false"
                    transition="scale-transition"
                    offset-y
                    min-width="auto"
                  >
                    <template v-slot:activator="{ on, attrs }">
                      <v-text-field
                        v-model="updateVm.createdAt"
                        label="Data dodania"
                        prepend-icon="mdi-calendar"
                        readonly
                        v-bind="attrs"
                        v-on="on"
                      ></v-text-field>
                    </template>
                    <v-date-picker
                      v-model="updateVm.createdAt"
                      :active-picker.sync="activePicker"
                      :max="(new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)"
                      min="1981-01-01"
                      @change="save"
                    ></v-date-picker>
                  </v-menu>
                </div>

                <div class="d-flex align-center mb-10">
                  <span class="align-self-end">Tagi:</span>
                  <v-progress-linear
                    v-if="isLoadingTags"
                    indeterminate
                    color="green"
                  ></v-progress-linear>

                  <v-checkbox
                    v-for="tag in allUpdateTags"
                    :key="tag.id"
                    v-model="updateVm.tags"
                    :label="tag.name"
                    color="indigo"
                    :value="tag"
                    hide-details
                    class="ml-3"
                  ></v-checkbox>
                </div>
                  <v-text-field
                      v-model="updateVm.title"
                      label="Tytuł"
                      required
                      :counter="15"
                      :rules="titleRules"
                      autofocus
                  >
                  </v-text-field>
                  <v-text-field
                      v-model="updateVm.description"
                      label="Opis"
                      :counter="30"
                      :rules="descriptionRules"
                  >
                  </v-text-field>
                  <div class="d-flex justify-end my-4">
                    <v-btn
                        color="error"
                        outlined
                        @click="closeUpdateDialog()"
                    >
                        anuluj
                    </v-btn>
                    <v-btn
                      color="success"
                      outlined
                      class="ml-3"
                      @click="updatePhoto()"
                      :disabled="!isUpdateFormValid"
                      :loading="isUpdatingPhoto"
                      >
                      zapisz
                    </v-btn>
                  </div>
              </v-form>
            </v-card-text>
          </v-card>
      </v-dialog>
    </v-container>
  </div>
</template>

<script>
import PhotoItem from '@/components/photo-item/PhotoItem.vue'
import TagsBar from '@/components/tags/TagsBar.vue'

export default {
    name: "HomeView",
    data(){
      return {
        photos: [],
        selectedTags: [],
        isLoadingPhotos: true,
        
        deleteDialog: false,
        isDeletingPhoto: false,
        deleteVm: null,

        updateDialog: false,
        isUpdatingPhoto: false,
        updateVm: null,
        isUpdateFormValid: false,
        allUpdateTags: [],
        isLoadingTags:false,
        titleRules: [
          v => !!v || 'Pole wymagane',
          v => (v.length>=2 && v.length<=15) || '2-15 znaków'
        ],
        descriptionRules: [
          v => (v.length<=30) || 'max 30 znaków'
        ],
        activePicker: null,
        date: null,
        menu: false,
      }
    },
    watch: {
      menu (val) {
        val && setTimeout(() => (this.activePicker = 'YEAR'))
      }
    },
    methods: {

      save (date) {
        this.$refs.menu.save(date)
      },
      async getAllPhotos(){
        const response = await this.$http.get('Photos/GetAll').catch((response)=>{response})

        if(response && response.data.status===true){
          this.photos = response.data.data
        }
        this.isLoadingPhotos = false
      },
      removePhoto(id){
        this.photos = this.photos.filter(p => p.id != id)
      },
      async deletePhoto(){
        this.isDeletingPhoto = true

        const self = this
        
        const response = await this.$http.delete('photos/delete?id='+this.deleteVm.id)
            .catch(function (response) {
                if(response && response.data)
                {
                    var errorCode = response.data.errorCode
                    if(30 == errorCode) // photo not exists
                    {
                        self.removePhoto(self.deleteVm.id)
                    }
                }
            })

        if(response && response.data.status===true)
        {
          this.removePhoto(this.deleteVm.id)
          this.$toast.success("Zdjęcie zostało usunięte.")
        }
        
        this.isDeletingPhoto = false
        this.deleteDialog = false
      },
      async updatePhoto(){

        this.isUpdatingPhoto = true

        const self = this
        
        const response = await this.$http.put('photos/Update', this.updateVm)
            .catch(function (response) {
                if(response && response.data)
                {
                    var errorCode = response.data.errorCode
                    if(30 == errorCode) // photo not exists
                    {
                        self.removePhoto(self.updateVm.id)
                    }
                }
            })

        if(response && response.data.status===true)
        {
          this.getAllPhotos()
          this.$toast.success("Załatwione.")
        }
        
        this.isUpdatingPhoto = false
        this.closeUpdateDialog()
      },
      openDeleteDialog(photo){
        this.deleteVm = photo
        this.deleteDialog = true
      },
      closeDeleteDialog(){
        this.deleteDialog = false
        this.deleteVm = null
      },
      openUpdateDialog(photo){
        this.updateVm = JSON.parse(JSON.stringify(photo))
        this.updateVm.description = (this.updateVm.description) ? this.updateVm.description : ""
        this.updateVm.createdAt = this.updateVm.createdAt.substr(0,10); // need correct date format for v-model in date picker
        this.isLoadingTags = true
        this.getTags()
        this.updateDialog = true
      },
      closeUpdateDialog(){
        this.updateDialog = false
        this.updateVm = null
        this.allUpdateTags = []
      },
      async getTags(){
        const response = await this.$http.get('Tags/GetAll').catch((response)=>{response})

        if(response && response.data.status===true){
          this.allUpdateTags = response.data.data
        }

        this.isLoadingTags = false
      }
    },
    computed: {
      isNoPhotosToShow(){
        return !this.isLoadingPhotos && this.photos.length==0
      },
      filteredPhotos:{
        get: function(){
          if(this.selectedTags.length==0){
            return this.photos
          } else {
            return this.photos.filter( (p) => {
              var isTagInPhoto = false

              for(var i=0; i<this.selectedTags.length; i++)
              { 
                var matchedPhotoTags = p.tags.filter(t=>t.id==this.selectedTags[i])
                if(matchedPhotoTags.length>0){
                  isTagInPhoto = true
                  break
                }
              }
              return isTagInPhoto
            })
          }
        }
      }
    },
    mounted(){
      this.getAllPhotos()
    },
    components: {
      'photo-item': PhotoItem,
      'tags-bar': TagsBar
    }
  }
</script>