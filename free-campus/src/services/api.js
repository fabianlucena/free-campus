import { authState } from '@/state/auth.js';

const baseURL = 'https://localhost:7140/';

export async function requestJson(service, options) {
  if (options.body && typeof options.body === 'object') {
    options.body = JSON.stringify(options.body);
  }

  options.headers = {
    'Content-Type': 'application/json',
    ...options.headers,
  };

  if (authState.sessionToken) {
    options.headers['Authorization'] = `Bearer ${authState.sessionToken}`;
  }

  const response = await fetch(`${baseURL}${service}`, options);
  if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
  }

  if (response.status === 204) {
    return null;
  }

  if (!response.headers.get('Content-Type')?.includes('application/json')) {
    throw new Error('Response is not valid JSON');
  }

  return await response.json();
}

export async function postJson(service, body) {
  return await requestJson(service, {
    method: 'POST',
    body,
  });
}