<template>
    <div class="grid">
      <div class="col-4">
        <h1>{{ title }}</h1>
      </div>
      <div class="col-8 mt-2 flex flex-row-reverse flex-wrap">
        <Button
          v-if="showButtonNew" 
          label="Nuevo"
          :icon="iconButtonNew"
          rounded
          @click="handleButtonNewClick"
        />
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  import { defineProps, defineEmits } from 'vue';
  
  interface Props {
      title: string
      showButtonNew: boolean
      iconButtonNew: string | undefined
  }
  
  const props = defineProps<Props>()

  const emits = defineEmits(['handleButtonNewClick'])

  const handleButtonNewClick = (): void => {
    emits('handleButtonNewClick')
  }

</script>
  
  <style scoped>
  .grid {
    border-bottom: 0.2px ridge var(--p-primary-color);
    margin: 0.2rem;
    padding: 0;
  }
  h1 {
      color: var(--p-primary-color);
  }
  .p-button {
    width: 120px;
  }
  </style>
  