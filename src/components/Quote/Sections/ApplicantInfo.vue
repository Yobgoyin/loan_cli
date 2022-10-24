<script setup>
    import { watch, onMounted, ref, computed } from 'vue'
    import moment from 'moment';
    import axios from 'axios'
    import InputField from '../Form/InputField.vue';
    import SelectField from '../Form/SelectField.vue';
    import DatePicker from '../Form/DatePicker.vue';
    import Form from '../Form/Form.vue';
    import RouteBtn from '../../Routing/RouteButton.vue'
    import { useRouter } from 'vue-router';
    const { currentRoute } = useRouter();

    const props = defineProps({
            loanDetails: Object
        })
    
    const formData = ref({
        Salutation: "",
        FirstName: "",
        LastName: "",
        Email: "",
        Mobile: "",
        DOB: ""
    })
    const url = ref('http://localhost:8081/api/Applicants/');
    const routeConfig = ref({ destination: "ApplyNow", params: { guid: currentRoute.value.params.guid} })
    const selected = ref('')
    const isApplicantLegal = ref(true);
    const IptLbls = ref(["First Name ", "Last Name", "Your Email", "Mobile Number", "Date of Birth"])

    const QuoteData = computed(() => ref(props.loanDetails))
    async function getApplicant() {
        try {
            const response = await axios.get(import.meta.env.VITE_APPLICANT_API_URL + currentRoute.value.params.guid);
            formData.value.FirstName = response.data.data.firstName
            formData.value.LastName = response.data.data.lastName
            formData.value.Email = response.data.data.email
            formData.value.Mobile = response.data.data.mobile
            formData.value.Salutation = response.data.data.salutation
            formData.value.DOB = moment(response.data.data.dob).format('YYYY-MM-DD')
        } catch (error) {
            console.error(error);
        }
    }

    async function updateApplicantDetails(data, qdata) {
        const quote = Object.assign({}, qdata)
        quote.id = 0
        quote.guid = null
        formData.Quote = quote
        formData.Guid = currentRoute.value.params.guid
        formData.id = 0
        try {

            const url = import.meta.env.VITE_APPLICANT_API_URL + currentRoute.value.params.guid
            const result = await
                fetch(url, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(formData),
                }).then((response) =>
                    console.log(response.json())
                )
        } catch (error) {
            console.log(error.response);
        }
    }

    async function updateQuote(data) {
        const quote = Object.assign({}, data.value)
        try {

            const url = import.meta.env.VITE_APPLICANT_API_URL + currentRoute.value.params.guid
            const applicant = await axios.get(url);
            quote.ApplicantId = applicant.data.data.id
            const result = await
                fetch(import.meta.env.VITE_QUOTE_API_URL, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                    body: JSON.stringify(quote),
            }).then((response) =>
                console.log(response.json())
            )
        } catch (error) {
            console.log(error.response);
        }
    }

    function calculateQuote(data, qdata) {
        const obj = Object.assign({}, data)
        const qObj = Object.assign({}, qdata.value)
        checkApplicantAge(data.DOB)
        if (isApplicantLegal) {
            updateApplicantDetails(obj, qObj)
        }
    }

    function checkApplicantAge( date) {
        let dob = moment(date)
        let isLegal = false;
        let today = moment();

        var year = today.year() - dob.year()
        var month = today.month() - dob.month()
        var day = today.day() - dob.day()

        if (year == 17) {
            if (month == 0) {
                if (day >= 0) {
                    isLegal = true;
                }
                else {
                    isLegal = false;
                }
            }
            else if (month > 0) {
                isLegal = true;
            }
            else {
                isLegal = false;
            }
        }
        else if (year > 17) {
            isLegal = true;
        }
        else {
            isLegal = false;
        }
        isApplicantLegal.value = isLegal;
    }
    onMounted(() => {
        getApplicant()
    })


    watch(() => formData, (data) => {
        calculateQuote(data.value, QuoteData)
    }, { deep: true })


    watch(() => QuoteData, (data) => {
        console.log(data)
        if (data.value.value.term != 0 && data.value.value.amount != 0) {
            updateQuote(data.value)
        } 
    })

    const SalutationOptions = [
        {
            label: 'Choose Salutation',
            value: '',
            disabled: true
        },
        {
            label: 'Mr.',
            value: 'Mr'
        },
        {
            label: 'Ms.',
            value: 'Ms'
        },
        {
            label: 'Mrs.',
            value: 'Mrs'
        }
    ]
</script>
<template>
    <h4>Your Information</h4>
    <ui-divider></ui-divider>

    <ui-form item-margin-bottom="16" action-align="center">
        <Form v-model:salutation="formData.Salutation"
              v-model:first-name="formData.FirstName"
              v-model:last-name="formData.LastName"
              v-model:email="formData.Email"
              v-model:mobile="formData.Mobile"
              v-model:dob="formData.DOB"
              v-bind="formData" />

        <ui-form-field :class="actionClass" v-if="isApplicantLegal">
            <RouteBtn :routeConfig="routeConfig">
                <ui-button raised>Calculate Quote</ui-button>
            </RouteBtn>
        </ui-form-field>

        <ui-form-field :class="actionClass" v-else="isApplicantLegal">
            <ui-button raised disabled>Calculate Quote</ui-button>
        </ui-form-field>
    </ui-form>
</template>