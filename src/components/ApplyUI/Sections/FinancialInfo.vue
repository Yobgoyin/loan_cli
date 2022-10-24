<script setup>
    import { ref, watch, onBeforeMount } from 'vue';
    import moment from 'moment';
    import axios from 'axios'
    import InfoHeader from './Header/InfoHeader.vue';
    import RowDisplay from './Helper/RowDisplay.vue';
    import FinanceDisplay from './Helper/FinanceDisplay.vue';
    import { useRouter } from 'vue-router';
    const { currentRoute } = useRouter();

    const financeInfoHeader = ref({
        title: "Finance Details",
        btnLbls: ["Edit"]
    })
    async function getQuoteApplicant() {
        try {
            const applicant = await axios.get(import.meta.env.VITE_APPLICANT_API_URL + currentRoute.value.params.guid);
            const url = import.meta.env.VITE_QUOTE_API_URL
            const response = await axios.get(url + applicant.data.data.id);
            infos.value.push(addData("Finance Amount", "$ " + response.data.amount, "over " + response.data.term + " months"))
            infos.value.push(addData("Repayments from", "$ " + response.data.pmt , "$ "+ (response.data.pmt)/30 + " weekly (based on 30 days)"))
        } catch (error) {
            console.error(error);
        }
    }
    onBeforeMount(() => {
        getQuoteApplicant()
    })
    const infos = ref([])

    function addData(title, info, sub) {
        let details = {
            title: "",
            info: "",
            subtitle: ""
        }
        details.title = title
        details.info = info
        details.subtitle = sub
        return details
    }
</script>
<template>
    <ui-grid>
        <ui-grid-cell columns="12">
            <h4>
                Finance Details
            </h4>
        </ui-grid-cell>
    </ui-grid>
    <ui-divider></ui-divider>
    <div v-for="info in  infos">
        <FinanceDisplay :details="info"></FinanceDisplay>
    </div>
</template>