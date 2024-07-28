<template>
    <Dialog
        :visible="visible"
        modal
        header="Eliminación"
        :closable="false"
    >
        <p>¿Esta seguro de eliminar el registro?</p>
        <div class="flex justify-end gap-3 mt-4">
            <Button 
                label="Cancelar" 
                severity="warn" 
                @click="handleConfirm(false)"
            />
            <Button 
                label="Ok" 
                @click="handleConfirm(true)" 
                :loading="loading" 
                loading-icon="pi pi-spin pi-spinner"
            />
        </div>
    </Dialog>
</template>
<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';

interface Props {
    visible: boolean,
    loading: boolean
}

const props = defineProps<Props>()

const emits = defineEmits(['handleConfirm'])

const handleConfirm = (value: boolean): void => {
    console.log(value)
    emits('handleConfirm', value)
}

</script>
<style scoped>
    
</style>