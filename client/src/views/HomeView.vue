<template>
    <v-container>
      <v-row>
        <v-col v-if="isLoadingPhotos">
          loading...
        </v-col>
        <v-col v-if="isNoPhotosToShow">
          <h2>brak zdjęć</h2>
        </v-col>
        <v-col
          v-for="photo in photos"
          :key="photo.id"
          class="d-flex child-flex"
          cols="6"
        >
          <v-img
            :src="$http.defaults.baseURL + photo.originalUrl"
            lazy-src="@/assets/1.png"
            :aspect-ratio="16/9"
            class="grey lighten-2"
          >
            <template v-slot:placeholder>
              <v-row
                class="fill-height ma-0"
                align="center"
                justify="center"
              >
                <v-progress-circular
                  indeterminate
                  color="grey lighten-5"
                ></v-progress-circular>
              </v-row>
            </template>
          </v-img>
        </v-col>
      </v-row>
    </v-container>
</template>

<script>
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
    }
}
</script>