<template>
    <v-container>
      <v-row>
        <v-col>
          <h1>Dodaj zdjęcie</h1>
        </v-col>
        <v-col>
          <v-form
            v-model="isFormValid"
            ref="form"
          >
            <v-file-input
              required
              :rules="fileRules"
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
          file: null
        },
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

    }
}
</script>