<script setup>
    import { ref } from 'vue'


    function redirectMoneyMe() {
        // `event` is the native DOM event
        window.open("https://www.moneyme.com.au/", '_blank')
    }

</script>
<template>
    <ui-top-app-bar 
        nav-id="nav-menu"
        content-selector="#app"
        :type="type"
        :title="title"
                    >
        <template #toolbar="{ toolbarItemClass }">
            <ui-icon-button :class="toolbarItemClass" @click="redirectMoneyMe"
                            icon="money"></ui-icon-button>
        </template>
    </ui-top-app-bar>
    <ui-drawer nav-id="nav-menu" type="modal">
        <ui-drawer-header>
            <ui-drawer-title>Header here</ui-drawer-title>
        </ui-drawer-header>
        <ui-drawer-content>
            <ui-list>
                <ui-item active>
                    <ui-item-first-content>
                        <ui-icon>arrow_back</ui-icon>
                    </ui-item-first-content>
                    <ui-item-text-content>Back</ui-item-text-content>
                </ui-item>
                <ui-list-divider></ui-list-divider>
            </ui-list>
        </ui-drawer-content>
    </ui-drawer>

</template>