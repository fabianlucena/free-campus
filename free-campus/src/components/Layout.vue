<template>
  <Header
    @toggleMenu="showMenu = !showMenu"
  />
  <div class="main">
    <MainMenu
      v-if="showMenu"
      :items="menuItems"
    />
    <Body />
  </div>
</template>

<script setup>
import { ref } from 'vue';
import MainMenu from './MainMenu.vue';
import Header from './Header.vue';
import Body from './Body.vue';
import { menuItems } from '../services/menu-items.js';

const showMenu = ref(true);
</script>

<style>
.main {
  display: flex;
  flex: 1;
  overflow: auto;
}
</style>