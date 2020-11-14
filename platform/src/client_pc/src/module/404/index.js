export default [
  {
    path: '*',
    name: '404',
    component: () => import('./404')
  },
  {
    path: '/admin/*',
    name: '404',
    component: () => import('./404')
  }
];
