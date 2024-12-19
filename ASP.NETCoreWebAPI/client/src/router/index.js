import { createRouter, createWebHistory } from 'vue-router';
import UserLoginPage from '../components/UserLoginPage.vue';

const routes = [
    {
        path: '/login',
        name: 'login',
        component: UserLoginPage
    },
    // Diğer sayfalar eklenebilir
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
