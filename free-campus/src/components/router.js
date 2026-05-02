import { createRouter, createWebHistory } from 'vue-router';
import { authState } from '@/state/auth';

import HomeView from '../views/HomeView.vue';
import AboutView from '../views/AboutView.vue';
import LoginView from '../views/LoginView.vue';
import DashboardView from '../views/DashboardView.vue';
import MyCoursesView from '../views/MyCoursesView.vue';
import AvailableCoursesView from '../views/AvailableCoursesView.vue';

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
    condition: () => !authState.isLoggedIn,
  },
  {
    path: '/dashboard',
    component: DashboardView,
    condition: () => authState.isLoggedIn,
  },
  {
    path: '/my-courses',
    component: MyCoursesView,
    condition: () => authState.permissions.includes('myCourses.view'),
  },
  {
    path: '/available-courses',
    component: AvailableCoursesView,
    condition: () => authState.permissions.includes('availableCourses.view'),
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