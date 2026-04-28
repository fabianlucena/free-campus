import { reactive } from 'vue';
import { _ } from '@vc/locale.js';

export var menuItems = reactive([
  { name: _('Home'), to: '/' },
  { name: _('About'), to: '/about' },
]);