const translations = {
  'About': 'Acerca de',
  'Home': 'Inicio',
};

export function _(key) {
  return translations[key] || key;
}