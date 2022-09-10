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
        >
        </photo-item>
      </v-row>
    </v-container>
</template>

<script>
import Photo from '@/components/Photo.vue'

export default {
    name: "HomeView",
    data(){
      return {
        photos: [],
        isLoadingPhotos: true,
      }
    },
    methods: {
      async getAllPhotos(){

        // https://localhost:5000/api/Downloads/photos/d13f0453-d1b1-4fb0-bc6b-680ac6ed6c96/original.jpg

        const response = await this.$http.get('Photos/GetAll')

        if(response && response.data.status){
          this.photos = response.data.data
          console.log(this.photos)
        }
        this.isLoadingPhotos = false
      },
      photoLink(photoVm, photoType="thumbnail"){
        var apiUrl = this.$http.defaults.baseURL + 'Downloads/'
        var photoLink = photoVm.imagePath.replace('\\','/') + '/' + photoType + photoVm.imageExtension
        return apiUrl+photoLink
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
      'photo-item': Photo
    }
}
</script>