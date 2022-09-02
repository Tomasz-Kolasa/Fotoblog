import Vue from 'vue'
import App from './App.vue'
import VueToast from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-sugar.css';
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import './axios.js'

Vue.use(VueToast, {
  duration: 5000
});

Vue.config.productionTip = false

new Vue({
  router,
  store,
  vuetify,
  // Toast,
  render: h => h(App)
}).$mount('#app')