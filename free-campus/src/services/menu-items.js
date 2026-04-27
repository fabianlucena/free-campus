import { reactive } from 'vue';
import { _ } from '../libs/i18n.js';

export var menuItems = reactive([
  { name: _('Home'), link: '/' },
  { name: _('About'), link: '/about' },
]);