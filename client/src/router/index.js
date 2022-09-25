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
      },
      {
        name: 'ManageTagsView',
        path: '/manage-tags',
        component: () => import(/* webpackChunkName: "ManageTagsView" */ '../views/TagsManagerView.vue')
      },
      {
        name: 'AddPhotoView',
        path: '/add-photo',
        component: () => import(/* webpackChunkName: "AddPhotoView" */ '../views/AddPhotoView.vue')
      },
      {
        name: 'LoginView',
        path: '/login',
        component: () => import(/* webpackChunkName: "LoginView" */ '../views/LoginView.vue')
      }
    ]
  }
]

const router = new VueRouter({
  routes
})

export default router
