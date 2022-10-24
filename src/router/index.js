
import Preview from '@/components/Quote/Preview.vue'
import ApplyNow from '@/components/ApplyUI/ApplyNow.vue'
import { createRouter, createWebHistory } from 'vue-router'

const routes = [
    {
        path: '/Home/:guid', name: 'Home', component: Preview, props: route => (
            { guid: route.params.guid }
        ) },
    {
        path: '/ApplyNow/:guid', name: 'ApplyNow', component: ApplyNow, props: route => (
            { guid: route.params.guid}
        )
    }
]
const router = createRouter({
    history: createWebHistory(),
    routes: routes
})

export default router