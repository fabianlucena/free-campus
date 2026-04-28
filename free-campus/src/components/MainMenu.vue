<template>
  <MenuBar
    class="vertical"
    :items="menuItems"
    :itemType="RouterLink"
  />
</template>

<script setup>
import { RouterLink } from 'vue-router';
import MenuBar from '@vc/MenuBar.vue';
import { menuItems } from '../state/menu-items.js';
</script>

<style scoped>
nav {
  height: 100%;
}
</style>
