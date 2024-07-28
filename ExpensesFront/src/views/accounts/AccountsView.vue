<template >
    <div class="page-container">
        <TitleComponent :title="'Cuentas'"/>
        <Card class="col-12 mt-2">
            <template #content>
                <DataTable 
                    :value="accounts"
                    striped-rows
                >
                    <Column field="firstName" header="Nombre"></Column>
                    <Column field="lastName" header="Apellido"></Column>
                    <Column field="userName" header="Usuario"></Column>
                    <Column field="roleName" header="Rol"></Column>
                    <Column header="Acciones">
                        <template #body="{data}">
                            <div class="col-12">
                                <Button 
                                    v-tooltip.top="'Cambiar contraseña'" 
                                    class="mr-2" 
                                    rounded 
                                    icon="pi pi-key"
                                    @click="handleOpenChangePassword(data.id)" 
                                >
                                </Button>
                                <Button 
                                    v-tooltip.top="'Editar'" 
                                    class="mr-2" 
                                    rounded 
                                    icon="pi pi-pencil" 
                                >
                                </Button>
                                <Button 
                                    v-tooltip.top="'Eliminar'" 
                                    class="mr-2" 
                                    rounded 
                                    icon="pi pi-trash"
                                    @click="handleOpenDeleteDialog(data.id)" 
                                >
                                </Button>
                            </div>
                        </template>
                    </Column>
                </DataTable>
            </template>
        </Card>
        <ChangePasswordComponent 
            :loading="loadingChangePassword"
            :visible="visibleChangePassword"
            @handle-cancel="visibleChangePassword = false"
            @handle-submit="handleChangePassword" 
        />
        <DeleteDialogComponent 
            :loading="loadingDeleteDialog"
            :visible="visibleDeleteDialog"
            @handle-confirm="handleConfirm"
        />
    </div>
</template>
<script setup lang="ts">
import TitleComponent from '@/components/Generic/TitleComponent.vue';
import Account from '@/models/account';
import { useAccountStore } from '@/stores/account';
import { ref, onMounted } from 'vue';
import ChangePasswordComponent from '@/components/Accounts/ChangePasswordComponent.vue';
import DeleteDialogComponent from '@/components/Generic/DeleteDialogComponent.vue';

const page = ref<number>(1)
const pageSize = ref<number>(5)
const accounts = ref<Account[]>([])
const accountStore = useAccountStore()
const selectedAccount = ref<string | undefined>(undefined)
const loadingChangePassword = ref<boolean>(false)
const visibleChangePassword = ref<boolean>(false)
const loadingDeleteDialog = ref<boolean>(false)
const visibleDeleteDialog = ref<boolean>(false)

onMounted(() => {
    getAccounts()
})

const getAccounts = (): void => {
    accountStore.getAccounts(page.value, pageSize.value)
                .then(response => {
                    accounts.value = response
                })
                .finally(() => {

                })
}
const handleOpenChangePassword = (id: string): void => {
    selectedAccount.value = id
    visibleChangePassword.value = true
}
const handleChangePassword = (form: any): void => {
    loadingChangePassword.value = true
    accountStore.changePassword(selectedAccount.value, form?.value.oldPassword, form?.value.newPassword, form?.value.repeatNewPassword)
                .then(() => {
                    visibleChangePassword.value = false
                })
                .finally(() => {
                    loadingChangePassword.value = false
                })
}
const handleOpenDeleteDialog = (id: string): void => {
    selectedAccount.value = id
    visibleDeleteDialog.value = true
}
const handleConfirm = (confirm: any): void => {
    if (confirm) {
        loadingDeleteDialog.value = true
        deleteAccount()
    }
    else {
        loadingDeleteDialog.value = visibleDeleteDialog.value = false
    }
}
const deleteAccount = (): any => {
    accountStore.deleteAccount(selectedAccount.value)
                .then(() => {
                    visibleDeleteDialog.value = false
                    getAccounts()
                })
                .finally(() => {
                    loadingDeleteDialog.value = false
                })
}

</script>
<style scoped>
    
</style>