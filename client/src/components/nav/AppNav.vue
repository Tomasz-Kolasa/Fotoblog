<template>
  <div>
      <v-app-bar fixed app>
        <v-app-bar-nav-icon
        @click="drawer = true"
        class="d-flex d-sm-none"
        >
        </v-app-bar-nav-icon>

        <v-toolbar-title>Fotoblog</v-toolbar-title>
        <v-spacer class="d-none d-sm-flex"></v-spacer>
        <span class="d-flex">{{user.name}}</span>
        <v-spacer class="d-none d-sm-flex"></v-spacer>
        
        <v-btn
        v-for="navItem in navItems"
        :key="navItem.title"
        :to="navItem.target"
        icon
        class="d-none d-sm-flex"
        :title="navItem.title"
        >
          <v-icon>{{navItem.icon}}</v-icon>
      </v-btn>
    </v-app-bar>

    <v-navigation-drawer
    app
    v-model="isDrawer"
    mini-variant
    mini-variant-width="75px"
    disable-resize-watcher
    >
        <v-list
        dense
        nav
        >
            <v-list-item
              v-for="navItem in navItems"
              :key="navItem.title"
              link
            >
              <v-list-item-content class="d-block text-center">
                <v-btn
                  :to="navItem.target"
                  :title="navItem.title"
                  icon
                  
                  >
                    <v-icon>{{navItem.icon}}</v-icon>
                </v-btn>
              </v-list-item-content>
            </v-list-item>
        </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
  import {useUserStore} from "@/pinia/stores/useUserStore"
  import {navigationItems} from './nav-items.js'

    export default {
      name: 'AppNav',
      data () {
        return {
          drawer: false,
          isToBeClosed: false,
          user: useUserStore(),
        }
      },
      computed:{
        isDrawer: function(){
          if(this.$vuetify.breakpoint.name == 'xs' && this.drawer) return true
          return false
        },
        navItems: function(){
          return navigationItems.filter(
            x => (!this.user.isAdmin && x.showAnonymus) || (this.user.isAdmin && x.showAdmin) )
        }
      },
      mounted(){
      }
    }
  </script>