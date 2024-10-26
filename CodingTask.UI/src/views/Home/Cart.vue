<script setup lang="ts">
import { Button } from '@/components/ui/button'
import CartItem from './CartItem.vue'
import { useStore } from 'vuex'
import { RootState } from '@/store'
import { computed } from 'vue'
import { handleError } from '@/utils/errorHandler';

const store = useStore<RootState>()

const cartItems = computed(() => store.state.cart.items);
const cartTotal = computed(() => 
  Math.round(
    store.state.cart.items.reduce((acc, item) => {
      return acc + item.quantity * item.product.price * 100;
    }, 0)
  ) / 100
);

async function checkout() {
  try {
    await store.dispatch('cart/checkout')
  } catch (error) {
    handleError(error)
  }
}

</script>

<template>
  <div class="max-w-sm ms-auto">
    <h1 class="font-medium text-lg font-['Poppins']">Your Shopping Cart</h1>

    <template v-for="item in cartItems">
      <CartItem :product="item.product" :quantity="item.quantity" />
    </template>

    <div class="py-4">
      <h3 class="text-md font-medium">Shipping</h3>
      <p class="text-sm text-[#6C7275]">
        Experience seamless shopping with a bonus: Spend over $150 and unlock
        free global shipping, saving you the standard $30 shipping fee!
      </p>
      <p class="mt-2 flex justify-between items-center">
        <strong>Shipping Cost:</strong> ${{ cartTotal > 150 ? 0 : 30.0 }}
      </p>
    </div>

    <div class="py-4">
      <h3 class="text-md font-medium">ShoppingApp Peace of Mind</h3>
      <p class="text-sm text-[#6C7275]">
        Shop confidently on ShoppingApp knowing if something goes wrong with an
        order, we've got your back for all eligible purchases.
      </p>
    </div>

    <div class="py-4 text-right flex justify-between items-center">
      <h2 class="text-xl font-medium">Total</h2>
      <span class="text-2xl font-bold">${{ cartTotal }}</span>
    </div>

    <Button @click="checkout()" class="w-full mb-2">Checkout</Button>
    <Button variant="outline" class="w-full">Empty cart</Button>
  </div>
</template>
