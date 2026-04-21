<template>
  <Header >
    <ButtonMenu @click="emit('toggleMenu', { id: 1 })" />
    Free Campus
    <span class="right">
      <ButtonLightDark />
      <ButtonUser @click="emit('userClick', { id: 1 })" />
    </span>
  </Header>
</template>

<script setup>
import ButtonMenu from '@vc/buttons/Menu.vue';
import ButtonUser from '@vc/buttons/User.vue';
import ButtonLightDark from '@vc/buttons/LightDark.vue';
import Header from '@vc/Header.vue';

const emit = defineEmits(['toggleMenu', 'userClick']);
</script>

<style>
.right {
  display: flex;
  gap: .3em;
}
</style>