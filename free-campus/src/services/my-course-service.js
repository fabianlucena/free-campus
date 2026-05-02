import { getJson } from './api.js';

export async function getMyCourses() {
  return await getJson('/v1/my-courses');
}