import { reactive } from 'vue';
import { _ } from '@vc/locale.js';
import { authState } from '@/state/auth';

export var menuItems = reactive([
  { name: _('Dashboard'), condition: () => authState.isLoggedIn, to: '/dashboard' },
  { name: _('About'),     condition: () => authState.isLoggedIn, to: '/about' },
]);