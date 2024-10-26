<script setup lang="ts">
import { useStore } from 'vuex';
import { Trash2, Plus, Minus } from 'lucide-vue-next'
import { Button } from '@/components/ui/button'
import { Product } from '@/api/models'
import { BASE_API_URL } from '@/api/http';
import { RootState } from '@/store';
import { handleError } from '@/utils/errorHandler';

const store = useStore<RootState>();
const props = defineProps<{ product: Product; quantity: number }>()

async function removeFromCart(product: Product) {
  try {
    await store.dispatch('cart/removeFromCart', product);
  } catch (error) {
    handleError(error);
  }
}

async function increment(product: Product) {
  try {
    await store.dispatch('cart/addProductToCart', { product, quantity: 1 });
  } catch (error) {
    handleError(error);
  }
}

async function decrement(product: Product) {
  try {
    await store.dispatch('cart/addProductToCart', { product, quantity: -1 });
  } catch (error) {
    handleError(error);
  }
}

</script>

<template>
  <div class="border-b py-4 flex items-start justify-between">
    <div class="flex items-start space-x-4">
      <img :src="BASE_API_URL + props.product.images[0]" width="80px" height="96px" alt="product image" />
      <div>
        <h2 class="text-lg font-medium">{{ props.product.name }}</h2>
        <p class="text-sm text-[#6C7275]">
          {{ props.product.description }}
        </p>
        <div class="mt-2 inline-flex items-center space-x-2 border rounded border-[#6C7275] px-2">
          <Button @click="decrement(props.product)" variant="ghost" size="icon">
            <Minus clip="w-4 h-4" />
          </Button>
          <span>{{ props.quantity }}</span>
          <Button @click="increment(props.product)" variant="ghost" size="icon">
            <Plus clip="w-4 h-4" />
          </Button>
        </div>
      </div>
    </div>
    <div class="text-right">
      <span class="block text-lg font-medium">${{ props.product.price }}</span>
      <Button @click="removeFromCart(props.product)" variant="ghost" size="icon" class="mt-2">
        <Trash2 />
      </Button>
    </div>
  </div>
</template>
