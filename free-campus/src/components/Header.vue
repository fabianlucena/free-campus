<template>
  <Header >
    <ButtonMenu @click="emit('toggleMenu', { id: 1 })" />
    Free Campus
    <ButtonUser @click="emit('userClick', { id: 1 })" />
  </Header>
</template>

<script setup>
import ButtonMenu from '@vc/buttons/Menu.vue';
import ButtonUser from '@vc/buttons/User.vue';
import Header from '@vc/Header.vue';

const emit = defineEmits(['toggleMenu', 'userClick']);
</script>