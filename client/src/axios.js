import Axios from 'axios';
import Vue from 'vue'

if(process.env.NODE_ENV=='production'){
  Axios.defaults.baseURL= 'https://fotoblog-api-app.azurewebsites.net/api/'
} else{
  Axios.defaults.baseURL='https://localhost:5000/api/'
}

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
            case 20:
              Vue.$toast.error("Niedozwolone rozszerzenie pliku.")
              break; 
            case 21:
              Vue.$toast.error("Przesłany plik jest pusty.")
              break; 
            case 22:
              Vue.$toast.error("Nie udało się zapisać pliku.")
              break; 
            case 30:
              Vue.$toast.warning("Zdjęcie zostało usunięte wcześniej w innej przeglądarce.")
              break; 
            case 40:
                Vue.$toast.error("Nieprawidłowe dane logowania.")
                break; 
            case 41:
              Vue.$toast.error("Konto administratora już istnieje.")
              break;
            case 42:
              Vue.$toast.error("Nie udało się wysłać maila.")
              break;
            case 43:
              Vue.$toast.warning("Twój adres email jest jeszcze niepotwierdzony.")
              break
            case 44:
              Vue.$toast.error("Niewłaściwy bądź nieaktualny link.")
              break
            case 45:
              Vue.$toast.error("Nie udało się wysłać maila.")
              break
            case 46:
              Vue.$toast.error("Nieprawidłowy link.")
              break
            case 50:
              Vue.$toast.error("Wystąpił błąd 50.")
              break;
            case 51:
              Vue.$toast.error("Nieprawidłowe hasło.")
              break; 
            default:
              Vue.$toast.error("Wystąpił nieoczekiwany błąd.")
              console.log(`TBD handle err code!!: Axios response interceptor - response error code: ${error.response.data.errorCode}`)
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
    else if(error.response.status == 401)
    {
      Vue.$toast.error("Odmowa dostępu.")
    }
    else if(error.response.status == 403)
    {
      Vue.$toast.error("Brak uprawnień.")
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
  return Promise.reject(error.response)
});

export default Axios;