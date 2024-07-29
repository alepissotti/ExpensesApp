<template>
    <div class="page-container">
      <TitleComponent :title="'Profile'" :show-button-new="false" :icon-button-new="undefined"/>
      <Card class="col-12 md:col-8 mt-3">
        <template #title>
          <div class="text-2xl text-primary">
            <i class="text-2xl pi pi-user"></i>
            {{ fullName }}
          </div>
        </template>
        <template #content>
          <div class="grid mt-2">
            <div class="col-6">
              <div class="grid">
                <div class="col-3">User:</div>
                <div class="col-9 font-bold">{{ account?.userName }}</div>
              </div>
              <div class="grid">
                <div class="col-3">First name:</div>
                <div class="col-9 font-bold">{{ account?.firstName }}</div>
              </div>
              <div class="grid">
                <div class="col-3">Last name:</div>
                <div class="col-9 font-bold">{{ account?.lastName }}</div>
              </div>
              <div class="grid">
                <div class="col-3">Role:</div>
                <div class="col-9 font-bold">{{ account?.roleName }}</div>
              </div>
            </div>
            <Divider layout="vertical" />
            <div class="col-5 flex align-items-center justify-content-center">
              <Button @click="changePasswordFormVisible = true" :disabled="!hasPermissionToAccess('ACCOUNTS_GETALL')">
                  Change Password 
                  <i class="pi pi-key"></i>
              </Button>
            </div>
          </div>
        </template>
      </Card>
      <ChangePasswordComponent 
        :visible="changePasswordFormVisible"
        :loading="loading" 
        @handle-cancel="changePasswordFormVisible = false"
        @handle-submit="handleChangePassword"
      />
    </div>
  </template>
  
  <script setup lang="ts">
  import TitleComponent from '@/components/Generic/TitleComponent.vue';
  import ChangePasswordComponent from '@/components/Accounts/ChangePasswordComponent.vue';
  import { useAccountStore } from '@/stores/account';
  import { ref } from 'vue';
  import { hasPermissionToAccess } from '@/auth/auth';
  import { handleLogout } from '@/auth/auth';
  
  const accountStore = useAccountStore()
  const changePasswordFormVisible = ref<boolean>(false);
  const loading = ref<boolean>(false);
  
  const fullName = accountStore.fullName
  const account = accountStore.account
  const handleChangePassword = (form: any) => {
    loading.value = true
    accountStore.changePassword(accountStore?.loggedAccount?.id, form?.value.oldPassword, form?.value.newPassword, form?.value.repeatNewPassword)
                .then(() => {
                  changePasswordFormVisible.value = loading.value = false
                  handleLogout()
                })
                .finally(() => {
                  loading.value = false
                })
  }
  </script>
  
  <style scoped>

  </style>
  