<template>
  <div>
    <v-row align="center"
    justify="end">
      <v-btn color="indigo" outlined class="ma-4">Dodaj tag</v-btn>
    </v-row>
    <v-simple-table>
      <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left">id</th>
              <th class="text-left">nazwa</th>
              <th class="text-left">opcje</th>
            </tr>
          </thead>
          <tbody v-if="tags">
            <tr v-for="tag in tags" :key="tag.id">
              <td>{{tag.id}}</td>
              <td>{{tag.name}}</td>
              <td>
                <v-row>
                  <v-btn color="primary" class="mr-2" @click="goToEditPage(tag.id, tag.name)">edytuj</v-btn>
                  <v-btn color="error">usuń</v-btn>
                </v-row>
              </td>
            </tr>
          </tbody>
          <tbody v-else>
            <tr>
              <td colspan="100">
                <v-row
                  align="center"
                  justify="center"
                >
                  <p>wczytuję...</p>
                </v-row>
              </td>
            </tr>
          </tbody>
      </template>
    </v-simple-table>
  </div>
</template>

<script>

  export default {
    name: 'TagsComponent',
    data () {
      return {
        tags: undefined
      }
    },
    methods: {
      async getTags(){
        const { data } = await this.$http.get('Tags/GetAll')
        this.tags = data.data
      },
      goToEditPage(tagId, tagName)
      {
        this.$router.push("/editTag/"+tagId+"/"+tagName)
      }
    },
    mounted() {
      this.getTags();
    }
  }
</script>
