<template>
  <div>
    <v-main>
      <v-container>
        <v-row>
          <v-col>
            <h1 class="my-8 text-h4 text-center">Fotoblog</h1>
            <v-divider></v-divider>
          </v-col>
        </v-row>
      </v-container>
      <router-view /> <!-- template expanded here -->
    </v-main>
    <v-footer color="white" :app="true" :absolute="true">
      <v-col class="text-center">
        <v-divider></v-divider>
        <div class="my-8">
          {{ new Date().getFullYear() }} — Fotoblog
        </div>
      </v-col>
    </v-footer>
  </div>
</template>
<script>
export default {
  name: 'HomeViewBase',
  data(){
    return {
      states: this.$root.states
    }
  }
}
</script>
