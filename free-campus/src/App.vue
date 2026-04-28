<template>
  <Layout v-if="authState.isLoggedIn" />
  <HomeView v-else />
</template>

<script setup>
import Layout from '@/components/Layout.vue';
import LoginView from '@/views/LoginView.vue';
import { authState } from '@/state/auth.js';
import HomeView from './views/HomeView.vue';
</script>