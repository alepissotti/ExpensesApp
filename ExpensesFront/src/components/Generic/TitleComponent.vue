<template>
    <div class="title-container">
      <h1>{{ title }}</h1>
    </div>
  </template>
  
  <script setup lang="ts">
  import { defineProps } from 'vue';
  
  interface Props {
      title: string
  }
  
  const props = defineProps<Props>()


</script>
  
  <style scoped>
  .title-container {
    border-bottom: 0.2px ridge var(--p-primary-color);
  }
  h1 {
      color: var(--p-primary-color);
      margin-bottom: 0.5rem;
  }
  
  </style>
  