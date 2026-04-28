<template>
  <Header
    :showMenuButton="authState.isLoggedIn"
    @toggleMenu="showMenu = !showMenu"
  />
  <div class="main">
    <MainMenu v-if="authState.isLoggedIn &&showMenu" />
    <Body />
  </div>
</template>

<script setup>
import { ref } from 'vue';
import MainMenu from './MainMenu.vue';
import Header from './Header.vue';
import Body from './Body.vue';
import { authState } from '@/state/auth.js';

const showMenu = ref(true);
</script>

<style>
.main {
  display: flex;
  flex: 1;
  overflow: auto;
}
</style>