<template>
  <div>
    <v-container>
      <v-row>
        <v-col>
          <nav>
            <router-link to="/">Home</router-link>
          </nav>
        </v-col>
      </v-row>
    </v-container>
    <v-main>
      <v-container>
        <v-row>
          <v-col>
            <h1>Fotoblog</h1>
          </v-col>
        </v-row>
      </v-container>
      <router-view /> <!-- template expanded here -->
    </v-main>
    <v-footer>
      <v-container>
        <v-row>
          <v-col>
            footer
          </v-col>
        </v-row>
      </v-container>
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
