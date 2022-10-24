<script setup>

    import RouteBtn from '../../Routing/RouteButton.vue'

defineProps({
    firstName: String,
    lastName: String,
    salutation: String,
    email: String,
    mobile: String,
    dob: Date
})

    defineEmits(['update:firstName', 'update:lastName', 'update:salutation', 'update:email', 'update:mobile', 'update:dob'])

    const SalutationOptions = [
        {
            label: 'Choose Salutation',
            value: ''
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

        <ui-grid>
            <ui-grid-cell>
                <ui-form-field>
                    <ui-select :options="SalutationOptions" v-model="salutation" @change="$emit('update:salutation', $event.target.value)">Salutation</ui-select>
                </ui-form-field>
            </ui-grid-cell>
            <ui-grid-cell>
                <ui-form-field>
                    <ui-textfield v-model="firstName" @input="$emit('update:firstName', $event.target.value)">First Name</ui-textfield>
                </ui-form-field>
            </ui-grid-cell>
            <ui-grid-cell>
                <ui-form-field>
                    <ui-textfield v-model="lastName" @input="$emit('update:lastName', $event.target.value)">Last Name</ui-textfield>
                </ui-form-field>
            </ui-grid-cell>
        </ui-grid>
        <ui-grid>
            <ui-grid-cell>
                <ui-form-field>
                    <ui-textfield v-model="email" @input="$emit('update:email', $event.target.value)">Email</ui-textfield>
                </ui-form-field>
            </ui-grid-cell>
            <ui-grid-cell>
                <ui-form-field>
                    <ui-textfield v-model="mobile" @input="$emit('update:mobile', $event.target.value)">Mobil</ui-textfield>
                </ui-form-field>
            </ui-grid-cell>
            <ui-grid-cell>
                <ui-form-field>
                    <ui-datepicker v-model="dob" @input="$emit('update:dob', $event.target.value)"
                                   placeholder="Select Date.."
                                   toggle
                                   clear>
                        Date of Birth
                    </ui-datepicker>
                </ui-form-field>
            </ui-grid-cell>
        </ui-grid>
</template>