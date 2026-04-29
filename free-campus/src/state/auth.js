import { reactive } from 'vue';

export const authState = reactive({
});

export function setAuthState(newState) {
  newState ||= {};
  authState.sessionToken = newState.sessionToken || null;
  authState.isLoggedIn = !!newState.user;
  authState.user = newState.user;
  authState.Organizations = newState.Organizations || [];
  authState.currentOrganization = newState.currentOrganization || null;
  authState.roles = newState.roles || [];
  authState.permissions = newState.permissions || [];
}

setAuthState();