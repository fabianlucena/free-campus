import { reactive } from 'vue';
import { _ } from '@vc/locale.js';
import { authState } from '@/state/auth';

export var menuItems = reactive([
  {
    name: _('Dashboard'),
    to: '/dashboard',
    condition: () => authState.isLoggedIn,
  },
  {
    name: _('Logout'),
    to: '/logout',
    condition: () => authState.isLoggedIn,
  },
  {
    name: _('About'),
    to: '/about',
    condition: () => authState.isLoggedIn,
  },
]);