import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import store from './store'

import App from './App.vue'
import HomeView from './views/Home/Index.vue'
import ContactView from './views/Contact.vue'

import './assets/index.css'

const routes = [
  { path: '/', component: HomeView },
  { path: '/contact', component: ContactView },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

const app = createApp(App)

app.use(store)

app.use(router).mount('#app')
