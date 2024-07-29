<template>
    <Drawer :visible="visible" :position="'right'" @show="handleShow">
        <template #container>
            <div class="drawer-content">
                <div class="drawer-header-container">
                    <h3>
                        Change Password
                        <i class="pi pi-user-edit"></i>
                    </h3>
                </div>
                <div class="drawer-form-content">
                    <div class="mb-4 mt-2">
                        <InputGroup :class="{ 'p-invalid': oldPasswordError }">
                            <InputGroupAddon>
                                <i class="pi pi-key"></i>
                            </InputGroupAddon>
                            <Password
                                v-model="form.oldPassword" 
                                :feedback="false"
                                placeholder="Old password"
                                toggleMask
                            />
                        </InputGroup>
                        <small v-if="oldPasswordError" class="p-error">{{ oldPasswordError }}</small>
                    </div>
                    <div class="mb-4">
                        <InputGroup :class="{ 'p-invalid': newPasswordError }">
                            <InputGroupAddon>
                                <i class="pi pi-key"></i>
                            </InputGroupAddon>
                            <Password
                                v-model="form.newPassword" 
                                :feedback="false"
                                placeholder="New Password"
                                toggleMask
                            />
                        </InputGroup>
                        <small v-if="newPasswordError" class="p-error">{{ newPasswordError }}</small>
                    </div>
                    <div class="mb-4">
                        <InputGroup :class="{ 'p-invalid': repeatNewPasswordError }">
                            <InputGroupAddon>
                                <i class="pi pi-key"></i>
                            </InputGroupAddon>
                            <Password
                                v-model="form.repeatNewPassword" 
                                :feedback="false"
                                placeholder="Repeat new Password"
                                toggleMask
                                @keyup.enter="handleSubmit"
                            />
                        </InputGroup>
                        <small v-if="repeatNewPasswordError" class="p-error">{{ repeatNewPasswordError }}</small>
                    </div>
                </div>
                <Divider class="drawer-divider"/>
                <div class="drawer-button-container mb-2">
                    <Button severity="warn" @click="handleCancel">
                        Cancel
                    </Button>
                    <Button severity="success" @click="handleSubmit" :loading="loading" loading-icon="pi pi-spin pi-spinner">
                        Ok
                    </Button>
                </div>
            </div>
        </template>
    </Drawer>
</template>

<script setup lang="ts">
import { defineProps, ref, defineEmits, onMounted } from 'vue';

interface ChangePasswordForm {
    oldPassword: string | null,
    newPassword: string | null,
    repeatNewPassword: string | null
}

const form = ref<ChangePasswordForm>({ oldPassword: null, newPassword: null, repeatNewPassword: null })
const oldPasswordError = ref<string | null>(null)
const newPasswordError = ref<string | null>(null)
const repeatNewPasswordError = ref<string | null>(null)

interface Props {
    visible: boolean
    loading: boolean
}

const props = defineProps<Props>()
const emits = defineEmits(['handleCancel', 'handleSubmit'])
const handleCancel = () => {
    emits('handleCancel')
}
const handleSubmit = () => {
    oldPasswordError.value = newPasswordError.value = repeatNewPasswordError.value = null
    
    if (!form.value.oldPassword) {
        oldPasswordError.value = 'Old password is requiered'
    }
    if (!form.value.newPassword) {
        newPasswordError.value = 'New password is requiered'
    }
    if (!form.value.repeatNewPassword) {
        repeatNewPasswordError.value = 'Repeat new password is requiered'
    }
    if (form.value.newPassword != form.value.repeatNewPassword){
        repeatNewPasswordError.value = form.value.repeatNewPassword ?'Passwords dont match' :repeatNewPasswordError.value
        newPasswordError.value = form.value.repeatNewPassword ?'Passwords dont match' :newPasswordError.value
    }
    if (!(oldPasswordError.value || newPasswordError.value || repeatNewPasswordError.value)){
        emits('handleSubmit', form)
    }
}
const handleShow = () => {
    form.value = { oldPassword: null, newPassword: null, repeatNewPassword: null }
}
</script>

<style scoped>



</style>
