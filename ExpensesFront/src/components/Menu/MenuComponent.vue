<template>
    <div class="card flex justify-center">
        <Drawer v-model:visible="visible">
            <template #container="{ closeCallback }">
                <div class="flex flex-col h-full">
                    <div class="header-drawer">
                        <span>ExpensesApp</span>
                        <i class="pi pi-wallet"></i>
                    </div>
                </div>
                <Accordion>
                    <MenuModuleComponent @handleCloseMenu="visible = false"/>
                </Accordion>
                <div class="footer-drawer">
                    <Button class="logout-btn" @click="logout">
                        Logout
                        <i class="pi pi-sign-out"></i>
                    </Button>
                </div>
            </template>
        </Drawer>
        <Button icon="pi pi-bars" @click="visible = true"></Button>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import MenuModuleComponent from '@/components/Menu/MenuModuleComponent.vue';
import { handleLogout } from '@/auth/auth';

const visible = ref<boolean>(false);

const logout = (): void => {
    visible.value = false
    handleLogout()
}

</script>
<style scoped>
   .header-drawer, .footer-drawer {
    background-color: var(--p-primary-color);
    height: 50px;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    color: #fff;
    font-size: 18px;
    gap: 8px;
   }
   .footer-drawer {
    margin-top: auto;
    padding: 0;
   }
   .logout-btn {
    width: 100%;
    height: 100%;
   } 
</style>