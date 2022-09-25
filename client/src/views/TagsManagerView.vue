<template>
    <v-container>
      <v-row>
        <v-col>
          <Tags/>
        </v-col>
      </v-row>
    </v-container>
</template>

<script>
import Tags from "../components/tags/Tags.vue"
import {useUserStore} from '@/pinia/stores/useUserStore'
export default {
    name: "TagsManagerView",
    beforeRouteEnter: (to, from, next)=>{
      var user = useUserStore()
      if(!user.isLoggedIn)
      {
        next({name:'LoginView'})
      }
      next()
    },
    components: { Tags }
}
</script>
