<script setup>
    import { ref } from 'vue';
    import LoanDetails from './Sections/LoanDetails.vue';
    import ApplicantInfo from './Sections/ApplicantInfo.vue';
    const header = ref("Quote Calculator")
    const LoanDetailsValues = ref(null)
    defineProps({
        guid: String
    })
</script>
<template>
    <h4 :class="$tt('headline4')">{{ header }}</h4>
    <LoanDetails @dataChanged="(obj) => LoanDetailsValues = obj"></LoanDetails>
    <ApplicantInfo :loanDetails="LoanDetailsValues"></ApplicantInfo>
</template>