<template>
  <section>
    <h2>Standalone Courses</h2>
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
import { getStandaloneCourses } from '@/services/standalone-course-service';

const courses = ref([]);
getStandaloneCourses()
  .then(fetchedCourses => {
    courses.value = fetchedCourses;
  })
  .catch(error => {
    console.error('Error fetching standalone courses:', error);
  });
</script>

<style scoped>
</style>