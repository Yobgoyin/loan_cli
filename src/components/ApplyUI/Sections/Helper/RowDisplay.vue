<script setup>
    defineProps({
        details: Object
    })
    import { useGrid } from 'balm-ui';
    const $grid = useGrid();
    $grid.set('margin', 'desktop', '10px');
    import { useTheme } from 'balm-ui';
    const $theme = useTheme();

</script>
<template>
    <ui-grid>
        <ui-grid-cell columns="3">
            <label :class="$theme.getTextClass('hint', 'light')">{{details.title}}</label>
        </ui-grid-cell>
        <ui-grid-cell columns="6">

        </ui-grid-cell>
        <ui-grid-cell columns="3">
            <label :class="$theme.getTextClass('hint', 'light')">{{details.info}}</label>
        </ui-grid-cell>
    </ui-grid>
</template>