import { computed } from 'vue';
import { _ } from '@vc/locale.js';
import { authState } from '@/state/auth';
import { logout } from '@/services/login-service';

var rawMenu = [
  {
    name: 'dashboard',
    label: _('Dashboard'),
    to: '/dashboard',
    condition: () => authState.isLoggedIn,
  },
  {
    name: 'courses',
    label: _('Standalone courses'),
    to: '/standalone-courses',
    condition: () => authState.permissions.includes('standaloneCourses.view'),
  },
  {
    name: 'logout',
    label: _('Logout'),
    action: logout,
    condition: () => authState.isLoggedIn,
  },
  {
    name: 'about',
    label: _('About'),
    to: '/about',
  },
];

export var menuItems = computed(() =>
  rawMenu.filter(item => !item.condition || item.condition())
);