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
            <label :class="[$theme.getTextClass('secondary', 'light'),$tt('headline5')]">{{details.title}}</label>
        </ui-grid-cell>
        <ui-grid-cell columns="6">

        </ui-grid-cell>
        <ui-grid-cell columns="3" nested>
            <ui-grid-cell columns="12">
                <label :class="[$theme.getThemeClass('secondary', 'light'),$tt('headline5')]">{{details.info}}</label>
            </ui-grid-cell>
        </ui-grid-cell>
    </ui-grid>

    <ui-grid>
        <ui-grid-cell columns="9" style="border-bottom: 1px dotted grey;">

        </ui-grid-cell>
        <ui-grid-cell columns="3" >
                <label :class="$theme.getTextClass('secondary', 'light')">{{details.subtitle}}</label>
        </ui-grid-cell>
    </ui-grid>
</template>