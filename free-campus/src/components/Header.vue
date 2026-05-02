<template>
  <a
    href="#"
    @click.prevent.stop="() => router.push('/')"
  >
    <Header>
      <ButtonMenu v-if="showMenuButton" @click="emit('toggleMenu')" />
      <span class="title">{{ authState.currentOrganization?.title || 'Free Campus' }}</span>
      <span class="right">
        <ButtonLightDark :value="theme" @click="toggleTheme" />
        <ButtonUser v-if="authState.isLoggedIn" @click="emit('userClick')" />
        <ButtonLogin v-else @click.prevent.stop="router.push('/login')" />
      </span>
    </Header>
  </a>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from 'vue-router';
import ButtonMenu from '@vc/buttons/Menu.vue';
import ButtonUser from '@vc/buttons/User.vue';
import ButtonLogin from '@vc/buttons/Login.vue';
import ButtonLightDark from '@vc/buttons/LightDark.vue';
import Header from '@vc/Header.vue';
import { authState } from '@/state/auth';

const router = useRouter()
const emit = defineEmits(['toggleMenu', 'userClick', 'loginClick']);

defineProps({
  showMenu: {
    type: Boolean,
    default: true,
  },

  showMenuButton: {
    type: Boolean,
    default: true,
  },
});

const theme = ref(localStorage.getItem('theme'));
applyTheme();

function toggleTheme() {
  switch (theme.value) {
    case 'light':
      theme.value = 'dark';
      break;
    case 'dark':
      theme.value = '';
      break;
    default:
      theme.value = 'light';
  }

  applyTheme();
}

function applyTheme() {
  if (theme.value) {
    document.documentElement.setAttribute('data-theme', theme.value);
  } else if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
    document.documentElement.setAttribute('data-theme', 'dark');
  } else {
    document.documentElement.removeAttribute('data-theme');
  }

  localStorage.setItem('theme', theme.value);
}

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

a {
  color: inherit;
  text-decoration: none;
}
</style>