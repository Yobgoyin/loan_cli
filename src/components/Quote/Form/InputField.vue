<script setup>
    defineProps({
        label: String,
        model: Object
    })
    defineEmits(['update:model'])
</script>

<template>
    <!--<ui-form-field class="form-item">
        <ui-textfield v-model="formData.mobile" helper-text-id="mobile-helper-text">
            Mobile
        </ui-textfield>
        <ui-textfield-helper id="mobile-helper-text"
                             :valid-msg="validMsg.mobile"></ui-textfield-helper>
    </ui-form-field>-->


    <ui-form-field class="required">
        <ui-textfield v-model="model" @input="$emit('update:model', $event.target.value)">{{label}}</ui-textfield>
    </ui-form-field>
</template>