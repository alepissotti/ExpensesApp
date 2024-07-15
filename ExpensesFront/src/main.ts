import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import axiosInstance from './plugins/axios'

const app = createApp(App)

app.use(createPinia())
app.use(router)

//Axios
app.config.globalProperties.$axios = axiosInstance

app.mount('#app')
