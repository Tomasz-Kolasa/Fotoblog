import Axios from 'axios';
import Vue from 'vue'

Axios.defaults.baseURL='https://localhost:7141/api/' 
Vue.prototype.$http = Axios