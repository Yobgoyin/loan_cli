<script setup>
    defineProps({
        details: Object
    })
</script>
<template>
    <ui-grid>
        <ui-grid-cell columns="3">
            <h4>{{details.title}}</h4>
        </ui-grid-cell>
        <ui-grid-cell columns="7">
        </ui-grid-cell>
        <ui-grid-cell columns="2">
            <ui-button outlined :class="$theme.getThemeClass('secondary')">{{details.btnLbls[0]}}</ui-button>
        </ui-grid-cell>
    </ui-grid>
</template>