import { createApp } from 'vue'
import App from './App.vue'
import NavigationApp from './components/Navigation/Navigation.vue'
import router from '@/router'

import BalmUI from 'balm-ui'; // Official Google Material Components
import BalmUIPlus from 'balm-ui-plus'; // BalmJS Team Material Components
import 'balm-ui-css';

const mixin = {
    API_URL: "http://localhost:8081/api/"
}

//import validatorRules from './config/validator-rules'
 createApp(NavigationApp)
    .use(BalmUI, {
        $theme: {
            'background': '#fff',
            'primary': '#212121',
            'on-primary': '#fff',
            'on-secondary': '#000',
            'surface': '#fff',
            'on-surface': '#000',
            'error': '#b00020',
            'on-error': '#fff'
            // (Optional) New in 9.28.0, See ThemeColor type in APIs.
        },
        UiTopAppBar: {
            fixed: true,
        }
    })
    .use(BalmUIPlus)
    .mount('#appNavigation')

const app = createApp(App, {mixins: [mixin]})
    app.use(BalmUI, {
        $theme: {
            'background': '#fff',
            'primary': '#212121',
            'on-primary': '#fff',
            'on-secondary': '#000',
            'surface': '#fff',
            'on-surface': '#000',
            'error': '#b00020',
            'on-error': '#fff'
        },
        //$validator: validatorRules
    })
    .use(BalmUIPlus)
    .use(router)
    .mount('#app')



//const app = createApp(App)

//app.config.globalProperties.API_URL = "http://localhost:8081/api/";
//app.use(BalmUI, {
//    $theme: {
//        'background': '#fff',
//        'primary': '#212121',
//        'on-primary': '#fff',
//        'on-secondary': '#000',
//        'surface': '#fff',
//        'on-surface': '#000',
//        'error': '#b00020',
//        'on-error': '#fff'
//    },
//    //$validator: validatorRules
//})
//    .use(BalmUIPlus)
//    .use(router)
//    .mount('#app')