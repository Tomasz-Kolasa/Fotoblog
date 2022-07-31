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
              alert("Wystąpił ogólny błąd.")
              break; 
            case 10:
              alert("Tag już istnieje.")
              break;
            case 11:
              alert("Tag nie istnieje.")
              break; 
            default:
              alert("Axios response interceptor - response error code: "+error.response.data.errorCode)
              break;  
          }
        } else {
          // an error that should not normally happen
          alert("Wystąpił błąd, przepraszamy.")
          console.log('err: registration->response->no errorCode prop')
          console.log('error.response.data.errors:')
          console.log(error.response.data.errors)
        }
    }
    else if (error.request) {
      alert("Brak odpowiedzi z serwera, przepraszamy")
    } else {
      alert("Wystąpił błąd, przepraszamy.")
    }
  }
  else{
    alert("Błąd sieci, przepraszamy.")
  }
  // return Promise.reject(error)
});

export default Axios;