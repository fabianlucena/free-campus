import { computed } from 'vue';
import { _ } from '@vc/locale.js';
import { authState } from '@/state/auth';
import { logout } from '@/services/login-service';

var rawMenu = [
  {
    name: _('Dashboard'),
    to: '/dashboard',
    condition: () => authState.isLoggedIn,
  },
  {
    name: _('Logout'),
    action: logout,
    condition: () => authState.isLoggedIn,
  },
  {
    name: _('About'),
    to: '/about',
  },
];

export var menuItems = computed(() =>
  rawMenu.filter(item => !item.condition || item.condition())
);