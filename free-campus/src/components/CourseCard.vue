<template>
  <Card>
    <h3>{{ course.title }}</h3>
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
