import { getJson } from './api.js';

export async function getAvailableCourses() {
  return await getJson('/v1/available-courses');
}