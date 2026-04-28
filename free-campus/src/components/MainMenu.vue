<template>
  <MenuBar
    class="vertical"
    :items="menuItems"
    @action="onAction"
  />
</template>

<script setup>
import MenuBar from '@vc/MenuBar.vue';
import { menuItems } from '@/state/menu-items.js';
import { router } from '@/components/router.js';

function onAction(evt, item) {
  evt.preventDefault();
  evt.stopPropagation();

  if (item.action) {
    item.action();
  }

  if (item.to) {
    router.push(item.to);
    return;
  }
}

</script>

<style scoped>
nav {
  height: 100%;
}
</style>
