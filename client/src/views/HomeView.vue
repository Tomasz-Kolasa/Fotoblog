<template>
    <v-container>
      <v-row>
        <v-col
        class="d-flex child-flex"
        cols="6">

          <v-img
            src="https://localhost:5000/Img/6f7d29e4-e9c0-4cd0-8955-a48582ac4882/orginal.jpg"
            :lazy-src="`https://picsum.photos/10/6?image=${5 + 10}`"
            aspect-ratio="16/9"
            class="grey lighten-2">
          </v-img>

        </v-col>
        <v-col
          v-for="n in 9"
          :key="n"
          class="d-flex child-flex"
          cols="6"
        >
          <v-img
            :src="link[0]"
            :lazy-src="`https://picsum.photos/10/6?image=${n * 5 + 10}`"
            aspect-ratio="16/9"
            class="grey lighten-2"
          >
            <template v-slot:placeholder>
              <v-row
                class="fill-height ma-0"
                align="center"
                justify="center"
              >
                <v-progress-circular
                  indeterminate
                  color="grey lighten-5"
                ></v-progress-circular>
              </v-row>
            </template>
          </v-img>
        </v-col>
      </v-row>
    </v-container>
</template>

<script>
export default {
    name: "HomeView",
    data(){
      return {
        link: []
      }
    },
    mounted(){

    }
}
</script>