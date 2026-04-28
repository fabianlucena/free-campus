<template>
  <Header >
    <ButtonMenu v-if="showMenuButton" @click="emit('toggleMenu')" />
    <span class="title">Free Campus</span>
    <span class="right">
      <ButtonLightDark />
      <ButtonUser v-if="authState.isLoggedIn" @click="emit('userClick')" />
      <ButtonLogin v-else @click="emit('loginClick')" />
    </span>
  </Header>
</template>

<script setup>
import ButtonMenu from '@vc/buttons/Menu.vue';
import ButtonUser from '@vc/buttons/User.vue';
import ButtonLogin from '@vc/buttons/Login.vue';
import ButtonLightDark from '@vc/buttons/LightDark.vue';
import Header from '@vc/Header.vue';
import { authState } from '@/state/auth';

const emit = defineEmits(['toggleMenu', 'userClick', 'loginClick']);

defineProps({
  showMenuButton: {
    type: Boolean,
    default: true,
  },
});
</script>

<style>
.title {
  flex: 1;
  text-align: center;
}

.right {
  display: flex;
  gap: .3em;
}
</style>