import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import TestView from '../views/TestView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/home/:selectedOption/:selectedChart',
      name: 'home',
      component: HomeView,
      props: true
      },
      {
          path: '/test',
          name: 'test',
          component: TestView,
      }
  ]
})



export default router
