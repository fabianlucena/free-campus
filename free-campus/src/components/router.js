import { createRouter, createWebHistory } from 'vue-router';
import { authState } from '@/state/auth';

import HomeView from '../views/HomeView.vue';
import AboutView from '../views/AboutView.vue';
import LoginView from '../views/LoginView.vue';
import DashboardView from '../views/DashboardView.vue';

const routes = [
  {
    path: '/',
    component: HomeView,
  },
  {
    path: '/about',
    component: AboutView,
  },
  {
    path: '/login',
    component: LoginView,
    condition: () => !authState.isLoggedIn,
  },
  {
    path: '/dashboard',
    component: DashboardView,
    condition: () => authState.isLoggedIn,
  },
];

export const router = createRouter({
  history: createWebHistory(),
  routes,
})

router.beforeEach((to, from) => {
  const condition = to.matched[0]?.condition;
  if (condition && !condition()) {
    return authState.isLoggedIn ? '/dashboard' : '/';
  }
});