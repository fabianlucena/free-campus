<template>
  <section>
    <h2>{{ _('My Courses') }}</h2>
    <Cards>
      <CourseCard
        v-for="course in courses" :key="course.id"
        :course="course"
      />
    </Cards>
  </section>
</template>

<script setup>
import { ref } from 'vue';
import Cards from '@vc/Cards.vue';
import CourseCard from '@/components/CourseCard.vue';
import { getMyCourses } from '@/services/my-course-service';
import { _ } from '@vc/locale.js';

const courses = ref([]);
getMyCourses()
  .then(fetchedCourses => {
    courses.value = fetchedCourses;
  })
  .catch(error => {
    console.error('Error fetching my courses: ', error);
  });
</script>

<style scoped>
</style>