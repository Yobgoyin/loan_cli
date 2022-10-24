<script setup>
    defineProps(
        {
            details: Object
        })
    import { ref } from 'vue'
    import { useGrid } from 'balm-ui';
    const $grid = useGrid();
    const data = ref(0)
    $grid.set('margin', 'desktop', '5px');
    const emit = defineEmits(['dataChanged'])
    function sliderClick() {
        emit('dataChanged', data.value)
    }
</script>

<template>
    <ui-grid>
        <ui-grid-cell :columns="{default:12, tablet:8}">
            <h5 :class="[$tt('subtitle2')]">{{details.title}}</h5>
            <ui-slider type="discrete"
                       v-bind:min="details.min"
                       v-bind:max="details.max"
                       :step="details.step"
                       v-model.number="data"
                       v-on:click ="sliderClick"
                       ></ui-slider>
        </ui-grid-cell>
    </ui-grid>
    <ui-grid>
        <ui-grid-cell :columns="{default:2, tablet:8}">
            <label :class="[$tt('subtitle2')]">
                {{details.min}}
            </label>
        </ui-grid-cell>
        <ui-grid-cell :columns="{default:8, tablet:8}" style="text-align:center;">
            <label :class="[$tt('subtitle2')]">
                {{details.label}}
            </label>
        </ui-grid-cell>
        <ui-grid-cell :columns="{default:2, tablet:8}">
            <label :class="[$tt('subtitle2')]">
                {{details.max}}
            </label>
        </ui-grid-cell>
    </ui-grid>

</template>