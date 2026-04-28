import { postJson } from './api.js';
import { authState, setAuthState } from '@/state/auth.js';
import { router } from '@/components/router.js';

export async function logout() {
  await postJson('logout');
  setAuthState();
  router.push('/');
}

export async function login(data) {
  const session = await postJson('login', data);
  localStorage.setItem('deviceToken', session.deviceToken);
  localStorage.setItem('autoLoginToken', session.autoLoginToken);
  sessionStorage.setItem('sessionToken', session.token);
  setAuthState({
    sessionToken: session.token,
    user: session.user,
    companies: session.data.companies,
    currentCompany: session.data.currentCompany,
    roles: session.data.roles,
    permissions: session.data.permissions
  });
  router.push('/');
}