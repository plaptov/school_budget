// Composables
import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    component: () => import('@/layouts/default/Default.vue'),
    children: [
      {
        path: 'students',
        name: 'Students',
        component: () => import('@/components/Students.vue'),
      },
    ],
  },
]

const router = createRouter({
  history: createWebHistory(process.env['BASE_URL']),
  routes,
})

export default router
