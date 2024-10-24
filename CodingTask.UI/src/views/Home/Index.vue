<script setup lang="ts">
import Product from './Product.vue'
import Cart from './Cart.vue'
</script>

<template>
  <div class="flex">
    <div class="basis-1/2">
      <Product />
    </div>
    <div class="basis-1/2">
      <Cart />
    </div>
  </div>
</template>
