import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import PrimeVue from 'primevue/config'
import Aura from '@primevue/themes/aura'

import App from './App.vue'
import router from './router'
import axiosInstance from './plugins/axios'


import Card from 'primevue/card'
import InputGroup from 'primevue/inputgroup'
import InputText from 'primevue/inputtext'
import InputGroupAddon from 'primevue/inputgroupaddon'
import IconField from 'primevue/iconfield'
import Password from 'primevue/password'
import Button from 'primevue/button'
import Toast from 'primevue/toast'
import ToastService from 'primevue/toastservice'

const app = createApp(App)

app.use(PrimeVue, {
    theme: {
        preset: Aura
    }
})
app.use(createPinia())
app.use(router)
app.use(ToastService)

//Prime components
app.component('Card', Card)
app.component('InputGroup', InputGroup)
app.component('InputText', InputText)
app.component('InputGroupAddon', InputGroupAddon)
app.component('IconField', IconField)
app.component('Password', Password)
app.component('Button', Button)
app.component('Toast', Toast)

//Axios
app.config.globalProperties.$axios = axiosInstance

app.mount('#app')
