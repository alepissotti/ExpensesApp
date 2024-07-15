import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import {isAutenticathed} from '../auth/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: {requieresAuth: true}
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/LoginView.vue')
    }
  ]
})

router.beforeEach((to, _, next) => {
  if (to?.meta?.requieresAuth && !isAutenticathed()) {
    next({name: 'login'})
  }
  else {
    next()
  }
})

export default router
