<template>
    <div class="card flex justify-center">
        <Drawer v-model:visible="visible">
            <template #container="{ closeCallback }">
                <div class="flex flex-col h-full">
                    <div class="header-drawer">
                        ExpensesApp
                    </div>
                </div>
                <Accordion value="0">
                    <MenuModuleComponent />
                </Accordion>
            </template>
        </Drawer>
        <Button icon="pi pi-bars" @click="visible = true"></Button>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import MenuModuleComponent from '@/components/Menu/MenuModuleComponent.vue';

const visible = ref<boolean>(false);

</script>
<style scoped>
   .header-drawer {
    background-color: var(--p-primary-color);
    height: 50px;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    color: #fff;
    font-size: 18px;
   } 
</style>