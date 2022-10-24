<script setup>
    import { ref, watchEffect, onBeforeMount } from 'vue';
    import axios from 'axios';
    import RowDisplay from './Helper/RowDisplay.vue';
    import RouteBtn from '../../Routing/RouteButton.vue'

    import { useRouter } from 'vue-router';
    const { currentRoute } = useRouter();
    const applicantInfoHeader = ref({
        title: "Your Information",
        btnLbls: ["Edit"]
    })

    const routeConfig = {
            destination: "Home",
            guid: currentRoute.value.params.guid
        }
    const url = ref('http://localhost:8081/api/Applicants/');
    let details = ref({
        title: "",
        info: ""
    })
    const infos = ref([])

    function addData(title, info) {
        let details = {
            title: "",
            info: ""
        }
        details.title = title
        details.info = info
        return details
    }

    async function getApplicant() {
        try {
            const response = await axios.get(import.meta.env.VITE_APPLICANT_API_URL + currentRoute.value.params.guid);
            infos.value.push(addData("Name", response.data.data.salutation + " " + response.data.data.firstName + " " + response.data.data.lastName))
            infos.value.push(addData("Mobile", response.data.data.mobile))
            infos.value.push(addData("Email", response.data.data.email))
        } catch (error) {
            console.error(error);
        }
    }

    onBeforeMount(() => {
        // component is rendered as part of the initial request
        // pre-fetch data on server as it is faster than on the client
        getApplicant()
    })
</script>
<template>
    <ui-grid>
        <ui-grid-cell columns="3">
            <h4>Your Information</h4>
        </ui-grid-cell>
        <ui-grid-cell columns="7">
        </ui-grid-cell>
        <ui-grid-cell columns="2">
            <RouteBtn :routeConfig="routeConfig">
                <ui-button outlined :class="$theme.getThemeClass('secondary')">Edit</ui-button>
            </RouteBtn>
        </ui-grid-cell>
    </ui-grid>
    <ui-divider></ui-divider>
    <div v-for="info in  infos">
        <RowDisplay :details="info"></RowDisplay>
    </div>
</template>

