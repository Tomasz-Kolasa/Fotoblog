<template>
    <v-container>
      <v-row>
        <v-col>
          <h1>Dodaj zdjęcie</h1>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <div style="width: 100%">
            <v-img
              :src="previewSrc"
              alt="Podgląd dodawanego zdjęcia"
              max-width="100%"
              contain
            ></v-img>

            <v-progress-linear
              v-model="uploadProgress"
              height="25"
              color="amber"
              :active="isProgressBar"
            >
              <strong>{{ Math.ceil(uploadProgress) }}%</strong>
            </v-progress-linear>

          </div>
        </v-col>
        <v-col>
          <v-form
            ref="form"
            v-model="valid"
            :disabled="isSubmitBtnLoaderActive"
          >
            <v-file-input
              ref="fileupload"
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
              :rules="descriptionRules"
              placeholder="wpisz opis (opcjonalnie)..."
            >
            </v-text-field>

            <v-progress-circular
              :style="{display: (isLoadingTags?'block':'none')}"
              indeterminate
              color="primary"
            ></v-progress-circular>

            <p class="red--text accent-3" :style="{display: (isAllTagsLoadingFailed?'block':'none')}">błąd wczytywania tagów :/</p>

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
              :disabled="!valid"
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
import {plcImg} from '@/utils/placeholder.js'

export default {
    name: "AddPhotoView",
    data() {
      return {
        valid: false,
        photoVm: {
          title: '',
          description: '',
          tags: []
        },
        allTags: [],
        isAllTagsLoadingFailed: false,
        isLoadingTags: true,
        previewSrc: placeholderImage,
        isSubmitBtnLoaderActive: false,
        isProgressBar: false,
        uploadProgress: 0,
        titleRules: [
          v => !!v || 'Pole wymagane',
          v => (v.length>=2 && v.length<=15) || '2-15 znaków'
        ],
        descriptionRules: [
          v => (v.length<=15) || 'mniej niż 30 znaków'
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

        const file = document.querySelector('input[type=file]').files[0];

        const formData = new FormData()

        formData.append('Title',this.photoVm.title)
        formData.append('Description',this.photoVm.description)
        this.photoVm.tags.forEach(element => {
          formData.append('Tags[]',element)
        })
        formData.append('File', file)

        this.uploadProgress = 0
        this.isProgressBar = true

        const response = await this.$http.post(
          'Photos/AddNew',
          formData,
          {
            headers: { "Content-Type": "multipart/form-data" },
            onUploadProgress: (progressEvent) => {

              const totalLength = progressEvent.lengthComputable ?
              progressEvent.total :
              progressEvent.target.getResponseHeader('content-length')
              || progressEvent.target.getResponseHeader('x-decompressed-content-length');

                if (totalLength !== null) {
                  this.uploadProgress = Math.round( (progressEvent.loaded * 100) / totalLength )
                }
            }
          }
        )

        if(response && response.data.status){
            this.photoVm.title = ''
            this.photoVm.description = ''
            this.photoVm.tags = []
            this.$refs.fileupload.reset()
            this.previewSrc = plcImg
            this.$refs.form.resetValidation()
            this.$toast.success('Sukces!')
            // this.$router.go()
        }

        this.isSubmitBtnLoaderActive = false
        this.isProgressBar = false

      },
      setPreview(){
        this.previewSrc = plcImg
        const file = document.querySelector('input[type=file]').files[0];
        const reader = new FileReader();

        reader.addEventListener("load", () => {
          // convert image file to base64 string
          this.previewSrc = reader.result
          // console.log(reader.result)
        }, false);

        if (file) {
          reader.readAsDataURL(file);
        }
      },
      async getTags(){
        const response = await this.$http.get('Tags/GetAll')

        if(response && response.data.status){
          this.allTags = response.data.data
          this.isAllTagsLoadingFailed = false
        } else {
          this.isAllTagsLoadingFailed = true
        }

        this.isLoadingTags = false
      }
    },
    mounted(){
      this.getTags()
    }
}
</script>