import Axios from 'axios';
import Vue from 'vue'

Axios.defaults.baseURL='https://localhost:5000/api/' 
Vue.prototype.$http = Axios

Axios.interceptors.response.use(response => response, error => {
    if(error.response) {
      if(error.response.status == 400) {
        if(error.response.data.errorCode)
        {
          switch(error.response.data.errorCode) {
            case 0:
              Vue.$toast.error("Wystąpił ogólny błąd.")
              break; 
            case 10:
              Vue.$toast.error("Tag już istnieje.")
              break;
            case 11:
              Vue.$toast.error("Tag nie istnieje.")
              break; 
            default:
              Vue.$toast.error("Axios response interceptor - response error code: "+error.response.data.errorCode)
              break;  
          }
        } else {
          // an error that should not normally happen
          Vue.$toast.error("Wystąpił błąd, przepraszamy.")
          console.log('err: registration->response->no errorCode prop')
          console.log('error.response.data.errors:')
          console.log(error.response.data.errors)
        }
    }
    else if (error.request) {
      Vue.$toast.error("Brak odpowiedzi z serwera, przepraszamy")
    } else {
      Vue.$toast.error("Wystąpił błąd, przepraszamy.")
    }
  }
  else{
    Vue.$toast.error("Błąd sieci, przepraszamy.")
  }
  // return Promise.reject(error)
});

export default Axios;