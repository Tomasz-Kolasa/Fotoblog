import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';
import pl from 'vuetify/src/locale/pl.ts'

Vue.use(Vuetify);

const vuetify = new Vuetify({
    lang: {
        locales: {pl},
      current: 'pl'
    },
    theme: {
      options: {
        customProperties: true
      },
      themes: {
        light: {
          primary: '#006699',
          secondary: '#F7C202',
          error: '#f44336',
          background: '#ffffff'
        }
      }
    }
  });

export default vuetify
