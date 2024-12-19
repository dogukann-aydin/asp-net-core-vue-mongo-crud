import { createApp } from 'vue';
import App from './App.vue';
import { createRouter, createWebHistory } from 'vue-router'; // Vue Router import edildi

// CrudPage bileşeni dahil edildi
import CrudPage from './components/CrudPage.vue'; // CrudPage bileşeni import edildi

// Kullanıcı login sayfası bileşenini dahil et
import UserLoginPage from './components/UserLoginPage.vue';

// Router için route tanımlamaları
const routes = [
    {
        path: '/crud', // CRUD sayfası için URL
        component: CrudPage, // CrudPage bileşeni ile eşleşir
    },
    {
        path: '/login', // Giriş sayfası için URL
        component: UserLoginPage, // UserLoginPage bileşeni ile eşleşir
    },
    // Diğer sayfa rotaları burada tanımlanabilir
];

// Router konfigürasyonu
const router = createRouter({
    history: createWebHistory(),
    routes, // yukarıdaki rotalar burada kullanılacak
});

createApp(App)
    .use(router) // Router uygulamaya ekleniyor
    .mount('#app');
