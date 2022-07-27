<template>
    <v-container>
      <v-row>
        <v-col>
          <TagsComponent/>
        </v-col>
      </v-row>
    </v-container>
</template>

<script>
import TagsComponent from "../components/tags/TagsComponent.vue";
export default {
    name: "HomeView",
    components: { TagsComponent }
}
</script>
