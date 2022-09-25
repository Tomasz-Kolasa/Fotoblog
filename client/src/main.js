import Vue from 'vue'
import App from './App.vue'
import VueToast from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-sugar.css';
import router from './router'
import { createPinia, PiniaVuePlugin } from 'pinia'
import vuetify from './plugins/vuetify'
import './axios.js'

Vue.use(VueToast, {
  duration: 5000
});

Vue.use(PiniaVuePlugin)
const pinia = createPinia()

Vue.config.productionTip = false

new Vue({
  router,
  pinia,
  vuetify,
  // Toast,
  render: h => h(App)
}).$mount('#app')