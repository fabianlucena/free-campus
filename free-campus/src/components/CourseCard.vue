<template>
  <Card>
    <h3>{{ course.title }}</h3>
    <span>{{ course.type.title }}</span>
    <p>{{ course.description }}</p>
  </Card>
</template>

<script setup>
import Card from '@vc/Card.vue';

defineProps({
  course: {
    type: Object,
    required: true
  }
});
</script>
