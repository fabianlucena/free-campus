<template>
  <Form
    @submit.prevent="onSubmit"
  >
    <h2>Login</h2>
    <InputField :label="_('Username')" name="username" v-model="data.username"/>
    <InputField :label="_('Password')" name="password" type="password" v-model="data.password"/>
  </Form>
</template>

<script setup>
import { reactive, toRaw } from 'vue';
import Form from '@vc/Form.vue';
import InputField from '@vc/InputField.vue';
import { _ } from '@vc/locale.js';
import { login } from '../services/login-service.js';

const data = reactive({
  username: 'admin',
  password: '1234',
});

async function onSubmit() {
  await login(toRaw(data));
}
</script>
