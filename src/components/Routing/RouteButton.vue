<script setup>
    defineProps({
        routeConfig: Object
    })
    import { useRouter } from 'vue-router';
    const { router } = useRouter();

</script>

<style scoped>
    a:link {
        text-decoration: inherit;
        color: inherit;
        cursor: auto;
    }

    a:visited {
        text-decoration: inherit;
        color: inherit;
        cursor: auto;
    }
</style>
<template>
    <router-link v-bind:to="{name: routeConfig.destination, params:routeConfig.params}">
        <slot />
    </router-link>
</template>
