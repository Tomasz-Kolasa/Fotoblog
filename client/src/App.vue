<template>
  <v-app>
    <Nav />
    <router-view/>
  </v-app>
</template>

<script>
import Nav from '@/components/nav/AppNav.vue'

export default {
  name: 'App',

  data: () => ({
    //
  }),
  components: { Nav }
};
</script>

<style>
  body{
    font-family: sans-serif
  }
</style>
