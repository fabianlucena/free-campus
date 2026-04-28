import { createApp } from 'vue';
import './style.css';
import App from './App.vue';
import { router } from './components/router.js';
import { addLocaleModule, _ } from '@vc/locale.js';

await addLocaleModule('/src/locale/');

const app = createApp(App);
app.use(router);
app.mount('#app');
