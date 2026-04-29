import { postJson } from './api.js';
import { authState, setAuthState } from '@/state/auth.js';
import { router } from '@/components/router.js';

async function doLogin(service, data) {
  const session = await postJson(service, data);
  localStorage.setItem('deviceToken', session.deviceToken);
  localStorage.setItem('autoLoginToken', session.autoLoginToken);
  sessionStorage.setItem('sessionToken', session.token);
  setAuthState({
    sessionToken: session.token,
    user: session.user,
    companies: session.data.companies,
    currentOrganization: session.data.currentOrganization,
    roles: session.data.roles,
    permissions: session.data.permissions
  });
  router.push('/dashboard');
}

export async function login(data) {
  await doLogin('/v1/login', data);
}

export async function autoLogin() {
  const deviceToken = localStorage.getItem('deviceToken');
  if (!deviceToken) {
    return;
  }

  const autoLoginToken = localStorage.getItem('autoLoginToken');
  if (!autoLoginToken) {
    return;
  }

  await doLogin('/v1/auto-login', { deviceToken, autoLoginToken });
}

export async function tryAutoLogin() {
  try {
    await autoLogin();
  } catch {}
}

export async function logout() {
  try {
    await postJson('/v1/logout');
  } catch (error) {
    console.warn('Logout request failed:', error);
  }

  sessionStorage.removeItem('sessionToken');
  localStorage.removeItem('autoLoginToken');
  setAuthState();
  router.push('/');
}