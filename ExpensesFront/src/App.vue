<script setup lang="ts">
import { RouterView } from 'vue-router'
import HeaderComponent from '../src/components/HeaderComponent.vue'
</script>

<template>
  <HeaderComponent />
  <Toast />
  <div style="margin-top: 1rem;">
    <RouterView />
  </div>
</template>

<style scoped>

</style>
