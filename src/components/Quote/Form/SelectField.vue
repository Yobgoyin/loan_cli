<script setup>
    defineProps({
        label: String,
        options: Object,
        modelValue : String,
        title: String
    })
    defineEmits(['update:modelValue'])
</script>

<template>

    <ui-form-field>
        <ui-select :options="options" :value="modelValue"  @input="$emit('update:modelValue', $event.target.value)">{{label}}</ui-select>
    </ui-form-field>
</template>