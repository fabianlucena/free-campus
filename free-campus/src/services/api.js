import { authState } from '@/state/auth.js';

const baseUrl = import.meta.env.VITE_API_BASE_URL.replace(/\/+$/, '');

export async function requestJson(service, options) {
  let { baseUrl: optionsBaseUrl, ...fetchOptions } = options;

  if (fetchOptions.body && typeof fetchOptions.body === 'object') {
    fetchOptions.body = JSON.stringify(fetchOptions.body);
  }

  fetchOptions.headers = {
    'Content-Type': 'application/json',
    ...fetchOptions.headers,
  };

  if (authState.sessionToken) {
    fetchOptions.headers['Authorization'] = `Bearer ${authState.sessionToken}`;
  }

  if (optionsBaseUrl) {
    if (optionsBaseUrl.endsWith('/')) {
      optionsBaseUrl = optionsBaseUrl.slice(0, -1);
    }
  } else {
    optionsBaseUrl = baseUrl;
  }

  const response = await fetch(`${optionsBaseUrl}${service}`, fetchOptions);
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

export async function getJson(service) {
  return await requestJson(service, {
    method: 'GET',
  });
}

export async function postJson(service, body) {
  return await requestJson(service, {
    method: 'POST',
    body,
  });
}