<template>
  <Card>
    <h3>{{ course.title }}</h3>
    <span>{{ course.type.title }}</span>
    <p>{{ course.description }}</p>
    <Button>{{ _('Enroll') }}</Button>
  </Card>
</template>

<script setup>
import Card from '@vc/Card.vue';
import Button from '@vc/Button.vue';
import { _ } from '@vc/locale.js';

defineProps({
  course: {
    type: Object,
    required: true
  }
});
</script>
