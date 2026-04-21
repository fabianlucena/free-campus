import { ref } from 'vue';

export var menuItems = ref([
  {
    name: 'Home',
    items: [
      { name: 'Sub Home 1', link: '/subhome1' },
      { name: 'Sub Home 2', link: '/subhome2' },
    ]
  },
  { name: 'About', link: '/about' },
  { name: 'Contact', link: '/contact' },
]);