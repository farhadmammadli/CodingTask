<script setup lang="ts">
import { computed } from 'vue'
import { useStore } from 'vuex'
import { Button } from '@/components/ui/button'
import { Search, CircleUser, ShoppingBag } from 'lucide-vue-next'
import { RootState } from '@/store';

const store = useStore<RootState>()

const cartItemsCount = computed(() => store.state.cart.items.length)
</script>

<template>
  <header class="h-16 border-b border-solid border-[#F3F5F7]">
    <div class="max-w-screen-lg mx-auto h-16 flex items-center justify-between">
      <div class="logo">Shopping App</div>
      <nav class="flex gap-10">
        <RouterLink class="text-[#6C7275]" active-class="text-[#141718]" to="/">Home</RouterLink>
        <RouterLink class="text-[#6C7275]" active-class="text-[#141718]" to="/shop">Shop</RouterLink>
        <RouterLink class="text-[#6C7275]" active-class="text-[#141718]" to="/product">Product</RouterLink>
        <RouterLink class="text-[#6C7275]" active-class="text-[#141718]" to="/contact">Contact Us</RouterLink>
      </nav>
      <div class="flex gap-4 items-center">
        <Button aria-label="Search" variant="link" size="icon">
          <Search />
        </Button>
        <Button aria-label="Account" variant="link" size="icon">
          <CircleUser />
        </Button>
        <Button aria-label="Shopping bag" variant="link" size="icon">
          <ShoppingBag />
        </Button>
        <span class="cart-item-count">{{ cartItemsCount }}</span>
      </div>
    </div>
  </header>
</template>

<style scoped>
.logo {
  color: #000000;
  font-weight: 500;
  font-family: 'Poppins', sans-serif;
  line-height: 24px;
}

.cart-item-count {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 20px;
  height: 20px;
  border-radius: 50%;
  background-color: #F178B6;
  text-align: center;
  font-family: 'Inter', sans-serif;
  font-weight: 700;
  font-size: 12px;
  color: #ffffff;
}
</style>
