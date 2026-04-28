import { postJson } from './api.js';

export async function login(data) {
  return await postJson('login', data);
}