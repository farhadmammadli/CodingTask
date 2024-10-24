<script setup lang="ts">
import { onMounted } from 'vue'
import { useStore } from 'vuex';
import Header from './components/Header.vue'

const store = useStore();

onMounted(() => {
  store.dispatch('cart/syncCartItems')
})
</script>

<template>
  <Header />
  <main class="max-w-screen-lg mx-auto py-8">
    <RouterView />
  </main>
</template>
