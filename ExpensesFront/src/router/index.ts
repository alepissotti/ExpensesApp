import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import {isAutenticathed, handleRetriveData} from '../auth/auth'

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
    },
    {
      path: '/profile',
      name: 'profile',
      component: () => import('../views/accounts/Profile.vue'),
      meta: {requieresAuth: true}
    },
    {
      path: '/accounts',
      name: 'accounts',
      component: () => import('../views/accounts/Accounts.vue'),
      meta: {requieresAuth: true}
    }
  ]
})

router.beforeEach((to, _, next) => {

  if (to?.name != 'login'){
    handleRetriveData()
  }
  
  if (to?.meta?.requieresAuth && !isAutenticathed()) {
    next({name: 'login'})
  }
  else {
    next()
  }
})

export default router
