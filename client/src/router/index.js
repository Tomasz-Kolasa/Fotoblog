import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeViewBase from '@/views/HomeViewBase.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    component: HomeViewBase,
    children: [
      {
        name: 'HomeView',
        path: '',
        component: () => import(/* webpackChunkName: "HomeView" */ '../views/HomeView.vue')
      }
    ]
  }
]

const router = new VueRouter({
  routes
})

export default router
