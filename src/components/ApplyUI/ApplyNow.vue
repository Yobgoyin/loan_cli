<script setup>
    import { ref, watch, onBeforeMount } from 'vue';
    import moment from 'moment';
    import axios from 'axios'
    import DisplayInfo from './Sections/DisplayInfo.vue';
    import FinancialInfo from './Sections/FinancialInfo.vue';
    import { useRouter } from 'vue-router';
    const { currentRoute } = useRouter();
    const header = ref("Your Quote")

    defineProps({
        guid: String
    })

    const totalRepayment = ref({
        base: 0,
        pmt: 0,
        establishmentFee: 300,
        term: 0
    });
    const total = ref(0)

    async function getQuoteApplicant() {
        try {
            const applicant = await axios.get(import.meta.env.VITE_APPLICANT_API_URL + currentRoute.value.params.guid);
            const url = import.meta.env.VITE_QUOTE_API_URL
            const response = await axios.get(url + applicant.data.data.id);
            totalRepayment.value.pmt = response.data.pmt
            totalRepayment.value.base = response.data.amount
            totalRepayment.value.term = response.data.term

            isEmailBlacklisted(applicant.data.data.email)
            isMobileBlacklisted(applicant.data.data.mobile)
        } catch (error) {
            console.error(error);
        }
    }
    onBeforeMount(() => {
        getQuoteApplicant()
    })
    watch(() => totalRepayment, (data) => {
        total.value = (data.value.term * data.value.pmt) + data.value.establishmentFee
    }, { deep: true })
    function redirectMoneyMe() {
        // `event` is the native DOM event
        window.open("https://www.moneyme.com.au/", '_blank')
    }
    const blackListed = ref({
        mobile: false,
        email: false
    })
    function isMobileBlacklisted(mobile) {
        let isBlacklisted = false;
        const blackList = ["09999999999", "09888888888", "09777777777"]
        console.log(blackList.includes(mobile))
        if (blackList.includes(mobile)) {
            isBlacklisted = true;
        }
        else {
            isBlacklisted = false;
        }
        blackListed.value.mobile = isBlacklisted;
    }

    function isEmailBlacklisted(mail) {
        let isBlacklisted = false;
        const blackList = ["rocketmail", "yahoo"]
        const domain = mail.split("@")[1];
        const domainOnly = domain.split(".")[0];

        if (blackList.includes(domainOnly)) {
            isBlacklisted = true;
        }
        else {
            isBlacklisted = false;
        }
        blackListed.value.email =  isBlacklisted
    }

    

</script>
<template>

    <h4 :class="[$tt('headline4')]">{{ header }}</h4>
    <DisplayInfo></DisplayInfo>
    <FinancialInfo @pmtChanged="(val) => pmt =val"></FinancialInfo>
    <ui-grid>
        <ui-grid-cell columns="4">
        </ui-grid-cell>
        <ui-grid-cell columns="4" nested>
            <ui-grid-cell v-if="blackListed.mobile != true &&  blackListed.email != true" columns="12">
                <ui-button raised @click="redirectMoneyMe">Apply Now</ui-button>
            </ui-grid-cell>
            <ui-grid-cell v-else columns="12">
                <p :class="[$theme.getTextClass('hint', 'light'),$tt('subtitle2')]">
                    <b>Mobile Number or Email is Blacklisted</b>
                </p>
            </ui-grid-cell>
            <ui-grid-cell columns="12">
                <p :class="[$theme.getTextClass('hint', 'light'),$tt('subtitle2')]">
                    Total repayment is <b>$ {{total}}</b>, made up of an establishment fee of <b>$ {{totalRepayment.establishmentFee}}</b>.
                    The repayment amount is based on the variables selected, is subject to our assessment and suitability,
                    and other important terms and conditions may apply.
                </p>
            </ui-grid-cell>
        </ui-grid-cell>
        <ui-grid-cell columns="4">
        </ui-grid-cell>
    </ui-grid>
</template>