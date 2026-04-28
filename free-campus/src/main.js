import { createApp } from 'vue';
import './style.css';
import App from './App.vue';
import { router } from './components/router.js';
import { addLocaleModule, _ } from '@vc/locale.js';
import { tryAutoLogin } from '@/services/login-service';

await addLocaleModule('/src/locale/');

tryAutoLogin();

const app = createApp(App);
app.use(router);
app.mount('#app');
