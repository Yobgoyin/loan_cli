<script setup>
    defineProps({
        label: String,
        model: Object
    })
</script>

<template>
    <ui-datepicker v-model="model"
                   :config="config"
                   v-bind:placeholder="label"
                   toggle
                   clear></ui-datepicker>
</template>