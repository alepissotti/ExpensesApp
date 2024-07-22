<template>
    <div class="header-container">
        <img src="/logo.png" alt="Logo" class="logo" />
        <div class="user-info">
            <p>Hi {{ fullName }}!</p>
        </div>
    </div>
</template>

<script lang="ts">
import { useAccountStore } from '@/stores/account';
import {computed} from 'vue'
export default {
    name: 'HeaderComponent',
    setup() {
        const accountStore = useAccountStore()

        const fullName = computed((): string => accountStore.fullName)

        return {
            fullName
        }
    }
};
</script>

<style scoped>
.header-container {
    height: 50px;
    width: 100%;
    background-color: var(--primary-color);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); 
    border-bottom: 1px solid rgba(0, 0, 0, 0.1); 
    display: flex;
    align-items: center; 
    justify-content: space-between; 
    padding: 0 20px; 
}

.logo {
    height: 100%;
    object-fit: contain; 
}

.user-info {
    margin-left: auto; 
    font-size: 1rem;
    color: #fff; 
}
</style>
