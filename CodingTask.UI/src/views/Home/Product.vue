<script setup lang="ts">
import { computed, onMounted } from 'vue'
import { useStore } from 'vuex'
import Vue3StarRatings from 'vue3-star-ratings'
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb'
import {
  Carousel,
  CarouselContent,
  CarouselItem,
} from '@/components/ui/carousel'
import { Button } from '@/components/ui/button'
import { RootState } from '@/store'
import { BASE_API_URL } from '@/api/http'
import { Product } from '@/api/models'
import { handleError } from '@/utils/errorHandler';

const store = useStore<RootState>()
const products = computed(() => store.state.products.all)

onMounted(() => {
  store.dispatch('products/getAllProducts')
})

async function addToCart(product: Product) {
  try {
    await store.dispatch('cart/addProductToCart', { product, quantity: 1 })
  } catch (error) {
    handleError(error)
  }
}

</script>

<template>
  <div class="mb-7">
    <Breadcrumb>
      <BreadcrumbList>
        <BreadcrumbItem>
          <BreadcrumbLink href="/"> Home </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator />
        <BreadcrumbItem>
          <BreadcrumbLink href="/shop"> Shop </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator />
        <BreadcrumbItem>
          <BreadcrumbLink href="/fashion">Fashion</BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator />
        <BreadcrumbItem>
          <BreadcrumbPage>Product</BreadcrumbPage>
        </BreadcrumbItem>
      </BreadcrumbList>
    </Breadcrumb>
  </div>
  <div v-for="product in products" class="border-b py-4">
    <div class="flex gap-2 items-center mb-4">
      <Vue3StarRatings style="margin-top: -3px" :star-size="16" :model-value="3.5" star-color="#343839"
        inactive-color="#999EA0" />
      <span class="text-xs leading-4">11 Reviews</span>
    </div>
    <h1 class="text-[#141718] text-4xl font-medium mb-4 font-['Roboto']">
      {{ product.name }}
    </h1>
    <p class="text-[#6C7275] text-base mb-4 font-['Inter']">
      {{ product.description }}
    </p>
    <div class="flex items-center gap-3 font-['Poppins']">
      <span class="inline-block text-[#121212] font-semibold text-3xl">
        ${{ product.price }}
      </span>
      <span class="inline-block text-[#6C7275] font-light text-xl leading-9 line-through">
        ${{ product.price * 2 }}
      </span>
    </div>
    <div class="my-8">
      <Carousel>
        <CarouselContent>
          <CarouselItem
            v-for="(imgPath, index) in product.images"
            :key="index"
            class="basis-1/3"
          >
            <img :src="`${BASE_API_URL}${imgPath}`" alt="carousel image" />
          </CarouselItem>
        </CarouselContent>
      </Carousel>
    </div>
    <div class="mb-8">
      <h2 class="text-[#6C7275] font-semibold text-base mb-4 font-['Inter'] mb-2">
        Description
      </h2>
      <p class="text-[#6C7275] text-base mb-4 font-['Inter']">
        {{ product.description }}
      </p>
    </div>
    <Button @click="addToCart(product)" class="w-full">Add to Cart</Button>
  </div>
</template>
