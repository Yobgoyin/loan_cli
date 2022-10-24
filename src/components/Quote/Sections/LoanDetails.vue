<script setup>
    import { ref, watch  } from 'vue'
    import Slider from '../Form/Slider.vue';
    import SelectField from '../Form/SelectField.vue';
    import axios from 'axios'
    import { useRouter } from 'vue-router';
    const { currentRoute } = useRouter();

    const QuoteDetails = ref({ min: 0, max: 15000, label: "How much do you need?", step: 100, title: "Amount" })
    const TermDetails = ref({ min: 0, max: 64, label: "Pick your term", step: 1, title: "Term" })
    const ProductOptions = [
        {
            label: 'Choose Product',
            value: '',
            disabled: true
        },
        {
            label: 'ProductA',
            value: '1'
        },
        {
            label: 'ProductB',
            value: '2'
        },
        {
            label: 'ProductC',
            value: '3'
        }
    ]
    const sQuoteValue = ref(0)
    const sTermValue = ref(0)
    const product = ref();
    const quoteDetails = ref({term:"", amount: ""})

    watch(sQuoteValue, (val) => {
        quoteDetails.value.amount = val
        updateQuote(quoteDetails)
    })
    watch(sTermValue, (val) => {
        quoteDetails.value.term = val
        updateQuote(quoteDetails)
    })


    
    async function updateQuote(data) {
        const quote = Object.assign({}, data.value)
        if (quote.term != "" && quote.amount != "") {
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
    }

</script>
<template>
    <h4>Loan Details</h4>
    <ui-divider></ui-divider>
    <ui-grid>
        <ui-grid-cell columns="12">

            <ui-form-field>
                <ui-select :options="ProductOptions" v-model="product"></ui-select>
            </ui-form-field>
        </ui-grid-cell>
        <ui-grid-cell columns="12">
            <Slider :details="QuoteDetails" @dataChanged="(num) => sQuoteValue = num"></Slider>
        </ui-grid-cell>
        <ui-grid-cell columns="12">
            <Slider :details="TermDetails" @dataChanged="(num) => sTermValue = num"></Slider>
        </ui-grid-cell>
    </ui-grid>
</template>