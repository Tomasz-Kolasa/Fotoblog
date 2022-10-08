<template>
  <div>
    <v-main>
      <router-view /> <!-- template expanded here -->
    </v-main>
    <v-footer color="white" app absolute>
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
