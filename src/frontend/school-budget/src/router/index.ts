// Composables
import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    component: () => import('@/layouts/default/Default.vue'),
    children: [
      {
        path: 'students',
        name: 'students',
        component: () => import('@/components/Students.vue'),
      },
      {
        path: 'adults',
        name: 'adults',
        component: () => import('@/components/Adults.vue'),
      },
    ],
  },
]

const router = createRouter({
  history: createWebHistory(process.env['BASE_URL']),
  routes,
})

export default router
