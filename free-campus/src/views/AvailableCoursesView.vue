<template>
  <section>
    <h2>Available Courses</h2>
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
import { getAvailableCourses } from '@/services/available-course-service';

const courses = ref([]);
getAvailableCourses()
  .then(fetchedCourses => {
    courses.value = fetchedCourses;
  })
  .catch(error => {
    console.error('Error fetching available courses: ', error);
  });
</script>

<style scoped>
</style>