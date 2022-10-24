<script setup>
    import Quote from './components/Quote/Preview.vue'
</script>

<template>
    <ui-grid>
        <ui-grid-cell columns="3"></ui-grid-cell>
        <ui-grid-cell columns="6">
            <router-view></router-view>
        </ui-grid-cell>
        <ui-grid-cell columns="3"></ui-grid-cell>
    </ui-grid>
</template>