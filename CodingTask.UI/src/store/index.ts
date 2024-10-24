import { createStore, createLogger } from 'vuex'
import cart, { CartState } from './modules/cart'
import products, { ProductsState } from './modules/products'

export type RootState = {
  cart: CartState
  products: ProductsState
}

const debug = import.meta.env.MODE === 'development'

export default createStore<RootState>({
  modules: {
    cart,
    products,
  },
  strict: debug,
  plugins: debug ? [createLogger()] : [],
})
