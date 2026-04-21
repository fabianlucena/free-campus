<template>
  <Header
    @toggleMenu="showMenu = !showMenu"
  />
  <MenuBar
    v-if="showMenu"
    :items="menuItems"
  />
  <Body />
</template>

<script setup>
import { ref } from 'vue';
import MenuBar from '@vc/MenuBar.vue';
import Header from './Header.vue';
import Body from './Body.vue';
import { menuItems } from '../services/menu-items.js';

const showMenu = ref(true);
</script>