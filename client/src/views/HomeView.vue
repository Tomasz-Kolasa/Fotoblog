<template>
    <v-container>
      <v-row>
        <v-col v-if="isLoadingPhotos">
          loading...
        </v-col>
        <v-col v-if="isNoPhotosToShow">
          <h2>brak zdjęć</h2>
        </v-col>
        <photo-item
          v-for="photo in photos"
          :key="photo.id"
          :photo="photo"
          :photosStates="photosStates"
          @delete-photo="deletePhoto(photo.id)"
        >
        </photo-item>
      </v-row>
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
        photosStates:{
          beingDeleted: []
        }
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
        this.photosStates.beingDeleted.push(id)

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
        
        this.photosStates.beingDeleted = this.photosStates.beingDeleted.filter(e => e != id)
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