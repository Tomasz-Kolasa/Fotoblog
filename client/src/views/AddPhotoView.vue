<template>
    <v-container>
      <v-row>
        <v-col>
          <div style="width: 100%">
            <h1>Dodaj zdjęcie</h1>
            <v-img
              :src="previewSrc"
              :hidden="!(!!previewSrc)"
              alt="Podgląd dodawanego zdjęcia"
            ></v-img>
          </div>
        </v-col>
        <v-col>
          <v-form
            v-model="isFormValid"
            enctype="multipart/form-data"
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
              color="red"
              :value="tag.id"
            ></v-checkbox>

            <v-btn
              color="success"
              outlined
              @click="savePhoto()"
              :loading="isSubmitBtnLoaderActive"
              :disabled="!isFormValid"
            >
              Wyślij
            </v-btn>
          </v-form>
        </v-col>
      </v-row>
    </v-container>
</template>

<script>

export default {
    name: "AddPhotoView",
    data() {
      return {
        isFormValid: false,
        photoVm: {
          title: '',
          description: '',
          file: null,
          tags: []
        },
        allTags: [],
        tagsLoaderDisplayStyle: 'block',
        previewSrc: '',
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
      savePhoto(){
        this.isSubmitBtnLoaderActive = true
        console.log(this.photoVm)
      },
      setPreview(){
        this.previewSrc = ''
        const file = document.querySelector('input[type=file]').files[0];
        // console.log(document.querySelector('input[type=file]').files)
        const reader = new FileReader();

        reader.addEventListener("load", () => {
          // convert image file to base64 string
          this.previewSrc = reader.result
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