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
import Tags from "../components/tags/Tags.vue";
export default {
    name: "HomeView",
    components: { Tags }
}
</script>
