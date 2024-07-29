<script setup lang="ts">
import { RouterView } from 'vue-router'
import HeaderComponent from '../src/components/HeaderComponent.vue'
import { isAutenticathed } from './auth/auth';
</script>

<template>
  <HeaderComponent v-if="isAutenticathed()"/>
  <Toast />
  <div style="margin-top: 1rem;">
    <RouterView />
  </div>
</template>

<style scoped>

</style>
