<template>
  <MenuBar
    class="vertical"
    :items="menuItems"
  />
</template>

<script setup>
import MenuBar from '@vc/MenuBar.vue';
import { menuItems } from '../services/menu-items.js';
</script>

<style scoped>
nav {
  height: 100%;
}
</style>
