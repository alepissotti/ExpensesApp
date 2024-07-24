<template>
    <div v-for="(menuModule, index) in menuModules">
        <AccordionPanel :value="`${index}`" :key="index">
            <AccordionHeader>
                <div class="header-content">
                    <span>{{ menuModule.name }}</span>
                    <i :class="`${menuModule.icon}`" v-if="menuModule.icon" style="font-weight: 700;"></i>
                </div>
            </AccordionHeader>
            <AccordionContent>
                <div v-for="(menuItem, index) in menuModule.childrens">
                    {{ menuItem.name }}
                </div>
            </AccordionContent>
        </AccordionPanel>
    </div>
</template>
<script setup lang="ts">
import menuJson from '../../data/menu.json'
import type MenuModule from '@/models/menuModule';
import { ref } from 'vue';
const menuModules = ref<MenuModule[]>(menuJson)
</script>
<style scoped>
.header-content {
  display: flex;
  align-items: center; 
  gap: 2px;
}
.header-content span {
  margin-right: 8px; 
}

</style>