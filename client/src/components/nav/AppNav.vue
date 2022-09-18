<template>
    <v-navigation-drawer app>
        <v-list-item>
            <v-list-item-content>
                <v-list-item-title class="text-h6">
                Fotoblog
                </v-list-item-title>
                <v-list-item-subtitle>
                subtitle
                </v-list-item-subtitle>
            </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>

        <v-list
        dense
        nav
        >
            <v-list-item
                v-for="item in items"
                :key="item.title"
                link
            >
                <v-list-item-icon>
                    <v-icon>{{ item.icon }}</v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                    <v-list-item-title>
                      <router-link :to="item.target" class="mr-3">{{ item.title }}</router-link></v-list-item-title>
                </v-list-item-content>
            </v-list-item>
        </v-list>
    </v-navigation-drawer>
</template>

<script>
    export default {
      name: 'AppNav',
      data () {
        return {
          items: [
            { title: 'Home', icon: 'mdi-view-dashboard', target: '/' },
            { title: 'Tagi', icon: 'mdi-tag-multiple', target: '/manage-tags' },
            { title: 'Dodaj zdjęcie', icon: 'mdi-image', target: '/add-photo' },
          ],
          right: null,
        }
      },
    }
  </script>