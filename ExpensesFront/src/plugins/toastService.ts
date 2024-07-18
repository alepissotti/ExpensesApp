// src/plugins/toastService.ts
import { createApp } from 'vue';
import ToastService from 'primevue/toastservice';
import Toast from 'primevue/toast';

const app = createApp({
  components: { Toast },
  template: '<Toast ref="toast" />',
});

app.use(ToastService);

const toastApp = app.mount(document.createElement('div'));
document.body.appendChild(toastApp.$el);

// Exporta el servicio de toast
export default toastApp.$toast;
