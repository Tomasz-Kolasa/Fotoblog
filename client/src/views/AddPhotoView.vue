<template>
    <v-container>
      <v-row>
        <v-col>
          <div style="width: 100%">
            <h1>Dodaj zdjęcie</h1>
            <v-img
              :src="previewSrc"
              alt="Podgląd dodawanego zdjęcia"
              max-width="100%"
              contain
            ></v-img>
          </div>
        </v-col>
        <v-col>
          <v-form
            v-model="isFormValid"
            enctype="multipart/form-data"
            :disabled="isSubmitBtnLoaderActive"
          >
            <v-file-input
              required
              :rules="fileRules"
              @change="setPreview"
              accept="image/*"
              label="Wybierz zdjęcie">
            </v-file-input>

            <v-text-field
              v-model="photoVm.title"
              required
              :rules="titleRules"
              placeholder="wpisz tytuł..."
            >
            </v-text-field>

            <v-text-field
              v-model="photoVm.description"
              placeholder="wpisz opis (opcjonalnie)..."
            >
            </v-text-field>

            <h3>wybierz tagi</h3>
            <v-progress-circular
              :style="{display: tagsLoaderDisplayStyle}"
              indeterminate
              color="primary"
            ></v-progress-circular>

            <v-checkbox v-for="tag in allTags" v-bind:key="tag.id"
              v-model="photoVm.tags"
              :label="tag.name"
              color="purple"
              :value="tag.id"
            ></v-checkbox>

            <v-btn
              color="success"
              outlined
              @click="savePhoto()"
              :loading="isSubmitBtnLoaderActive"
              :disabled="!isFormValid"
            >
              Zapisz
            </v-btn>
          </v-form>
        </v-col>
      </v-row>
    </v-container>
</template>

<script>
import placeholderImage from '@/assets/1.png'

export default {
    name: "AddPhotoView",
    data() {
      return {
        isFormValid: false,
        photoVm: {
          title: '',
          description: '',
          fileBase64: null,
          tags: []
        },
        allTags: [],
        tagsLoaderDisplayStyle: 'block',
        previewSrc: placeholderImage,
        isSubmitBtnLoaderActive: false,
        titleRules: [
          v => !!v || 'Pole wymagane',
          v => (v.length>=2 && v.length<=15) || '2-15 znaków'
        ],
        fileRules: [
          v => !!v || 'Zdjęcie jest wymagane',
          v => (v && v.size > 0) || 'Zdjęcie jest wymagane',
        ]
      }
    },
    methods: {
      async savePhoto(){
        this.isSubmitBtnLoaderActive = true
        // console.log(this.photoVm)

        const response = await this.$http.post('Photos/AddNew', this.photoVm)

        if(response && response.data.status){
            this.$toast.success('Sukces!')
        }

        this.isSubmitBtnLoaderActive = false

      },
      setPreview(){
        this.previewSrc = placeholderImage
        const file = document.querySelector('input[type=file]').files[0];
        // console.log(document.querySelector('input[type=file]').files)
        const reader = new FileReader();

        reader.addEventListener("load", () => {
          // convert image file to base64 string
          this.previewSrc = reader.result
          this.photoVm.fileBase64 = reader.result
        }, false);

        if (file) {
          reader.readAsDataURL(file);
        }
      },
      async getTags(){
        const response = await this.$http.get('Tags/GetAll')

        if(response && response.data.status){
          this.allTags = response.data.data
        }

        this.tagsLoaderDisplayStyle = 'none'
      }
    },
    mounted(){
      this.getTags()
    }
}
</script>