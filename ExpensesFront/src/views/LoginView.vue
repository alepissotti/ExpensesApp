<template>
    <div class="container">
      <div class="image">
        <!-- Contenido de la imagen -->
      </div>
      <div class="login">
        <Card>
          <template #title>Acceso a la app</template>
          <template #content>
            <div>
              <InputGroup :class="{ 'p-invalid': usernameError }">
                <InputGroupAddon>
                  <i class="pi pi-user"></i>
                </InputGroupAddon>
                <InputText 
                  v-model="username" 
                  placeholder="Username" 
                />
              </InputGroup>
              <small v-if="usernameError" class="p-error">{{ usernameError }}</small>
            </div>
            <div style="margin-top: 1rem;">
              <InputGroup :class="{ 'p-invalid': passwordError }">
                <InputGroupAddon>
                  <i class="pi pi-key"></i>
                </InputGroupAddon>
                <Password
                  v-model="password" 
                  :feedback="false"
                  placeholder="Password"
                  toggleMask
                  @keyup.enter="login"
                />
              </InputGroup>
              <small v-if="passwordError" class="p-error">{{ passwordError }}</small>
            </div>
            <Button 
              label="Acceder" 
              :severity="loading ?'contrast' :'info'"
              :loading="loading"
              @click="login"
              class="p-button"
            ></Button>
          </template>
        </Card>
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  import { ref } from 'vue';
  import { useAccountStore } from '@/stores/account';
  import { handleLogin } from '@/auth/auth';
  import { useRouter, } from 'vue-router';
  
  const username = ref<string | null>(null);
  const password = ref<string | null>(null);
  const usernameError = ref<string | null>(null);
  const passwordError = ref<string | null>(null);
  const loading = ref<boolean>(false);
  const accountStore = useAccountStore()
  const router = useRouter()
  
  const login = () => {
    usernameError.value = null;
    passwordError.value = null;
  
    if (!username.value) {
      usernameError.value = 'El nombre de usuario es obligatorio';
    }
    if (!password.value) {
      passwordError.value = 'La contraseña es obligatoria';
    }
  
    if (!usernameError.value && !passwordError.value) {
        loading.value = true
        accountStore.login(`${username.value}`, `${password.value}`)
                    .then(response => {
                      handleLogin(response.token, router)
                    })
                    .finally(() => {
                        loading.value = false
                    })
    }
  }
  </script>
  
  <style scoped>
  html, body {
    height: 100%;
    margin: 0;
  }
  
  .container {
    display: flex;
    flex-direction: row; 
    height: 100vh; 
    width: 100%; 
    padding: 0;
    margin: 0;
  }
  
  .login, .image {
    flex: 1; 
    height: 100%;
  }
  
  .image {
    background: url('expenses_app_login.jpg') no-repeat center center;
    background-size: cover; 
  }
  
  .login {
    display: flex;
    align-items: center;
    justify-content: center;
  }
  
  .p-card {
    margin: 0 1rem;
    min-width: 70%;
  }
  
  .p-button {
    margin-top: 1rem;
    width: 100%;
  }
  
  </style>
  