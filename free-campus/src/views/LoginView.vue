<template>
  <Form
    @submit.prevent="onSubmit"
  >
    <h2>Login</h2>
    <InputField label="Username" name="username" v-model="data.username"/>
    <InputField label="Password" name="password" type="password" v-model="data.password"/>
  </Form>
</template>

<script setup>
import { reactive, toRaw } from 'vue';
import Form from '@vc/Form.vue';
import InputField from '@vc/InputField.vue';
import { login } from '../services/login-service.js';

const data = reactive({
  username: 'admin',
  password: '1234',
});

async function onSubmit() {
  console.log(await login(toRaw(data)));
}
</script>
