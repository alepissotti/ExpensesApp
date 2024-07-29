<template>
    <Drawer :visible="visible" position="right" @show="handleShow">
        <template #container>
            <div class="drawer-content">
                <div class="drawer-header-container">
                    <h3>
                        Cuentas
                        <i class="pi pi-user"></i>
                    </h3>
                </div>
                <div class="drawer-form-content">
                    <div class="mb-4 mt-2">
                        <InputGroup>
                            <InputText placeholder="Nombre" />
                        </InputGroup>
                    </div>
                    <div class="mb-4">
                        <InputGroup>
                            <InputText placeholder="Apellido" />
                        </InputGroup>
                    </div>
                    <div class="mb-4">
                        <InputGroup>
                            <InputText placeholder="Usuario" />
                        </InputGroup>
                    </div>
                    <div class="mb-4">
                        <InputGroup>
                            <Password placeholder="Contraseña" toogleMask :feedback="false"/>
                        </InputGroup>
                    </div>
                    <div class="mb-4">
                        <Select placeholder="Rol" class="w-full" />
                    </div>
                </div>
            </div>
            <Divider class="drawer-divider"/>
                <div class="drawer-button-container mb-2">
                    <Button severity="warn" @click="handleCancel">
                        Cancelar
                    </Button>
                    <Button severity="success" @click="handleSubmit" :loading="loading" loading-icon="pi pi-spin pi-spinner">
                        Aceptar
                    </Button>
                </div>   
        </template>
    </Drawer>
</template>
<script setup lang="ts">

import { defineProps, defineEmits } from 'vue';

interface Props {
    visible: boolean
    loading: boolean
}

const props = defineProps<Props>()
const emits = defineEmits(['handleCancel', 'handleSubmit'])

const handleShow = () :void => {

}

const handleCancel = (): void => {
    emits('handleCancel')
}

const handleSubmit = (): void => {

}

</script>
<style scoped>
    .header-drawer {
        background-color: red;
    }
</style>