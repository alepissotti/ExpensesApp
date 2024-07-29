<template>
    <div v-for="(menuModule, index) in menuModules">
        <AccordionPanel :value="`${index}`" :key="index">
            <AccordionHeader>
                <div class="header-content">
                    <span>{{ menuModule.name }}</span>
                    <i :class="`${menuModule.icon}`" v-if="menuModule.icon"></i>
                </div>
            </AccordionHeader>
            <AccordionContent>
                <div v-for="(menuItem, index) in menuModule.childrens">
                    <router-link :key="index" :to="{name: menuItem.navigateTo}" class="item-content" @click="handleCloseMenu">
                        <span>{{ menuItem.name }}</span>
                        <i :class="`${menuItem.icon}`" v-if="menuItem.icon"></i>
                    </router-link>
                </div>
            </AccordionContent>
        </AccordionPanel>
    </div>
</template>
<script setup lang="ts">
import menuJson from '../../data/menu.json'
import type MenuModule from '@/models/menuModule';
import { RouterLink } from 'vue-router';
import { ref, defineEmits } from 'vue';

const menuModules = ref<MenuModule[]>(menuJson)
const emits = defineEmits(['handleCloseMenu'])

const handleCloseMenu = (): void => {
    emits('handleCloseMenu')
}
</script>
<style scoped>
.header-content, .item-content {
  display: flex;
  align-items: center; 
  gap: 2px;
}
.header-content span, .item-content span {
  margin-right: 8px; 
}

.item-content {
    margin-bottom: .5rem;
}

.p-accordionheader *, .pi-user:before {
    font-weight: 600 !important;
    color: var(--p-primary-color);
}
a {
    color: inherit;
    text-decoration: none;
}
</style>