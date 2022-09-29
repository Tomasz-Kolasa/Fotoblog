export const navigationItems =  [
    { title: 'Home', icon: 'mdi-home', target: '/', showAnonymus: true, showAdmin: true},
    { title: 'Tagi', icon: 'mdi-tag-multiple', target: '/manage-tags', showAnonymus: false, showAdmin: true },
    { title: 'Dodaj', icon: 'mdi-upload', target: '/add-photo', showAnonymus: false, showAdmin: true },
    { title: 'Ustawienia', icon: 'mdi-cog-outline', target: '/settings', showAnonymus: false, showAdmin: true },
    { title: 'Zaloguj', icon: 'mdi-account', target: '/login', showAnonymus: true, showAdmin: false },
    { title: 'Wyloguj', icon: 'mdi-logout', target: '/logout', showAnonymus: false, showAdmin: true }
  ]