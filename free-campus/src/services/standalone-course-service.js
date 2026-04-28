import { getJson } from './api.js';

export async function getStandaloneCourses() {
  return await getJson('/v1/standalone-courses');
}