import { CartItem, Product } from '@/api/models'
import * as shop from '@/api/shop'

export type CheckoutStatus = 'successful' | 'failed' | null

export type CartState = {
  items: CartItem[]
  checkoutStatus: CheckoutStatus
}

const state = (): CartState => ({
  items: [],
  checkoutStatus: null,
})

const getters = {}

const actions = {
  async checkout({ commit, state }) {
    const savedCartItems = [...state.items]
    commit('setCheckoutStatus', null)
    commit('setCartItems', { items: [] })
    try {
      await shop.checkout()
      commit('setCheckoutStatus', 'successful')
    } catch (_) {
      commit('setCheckoutStatus', 'failed')
      commit('setCartItems', { items: savedCartItems })
    }
  },

  async addProductToCart(
    { commit, dispatch },
    { product, quantity = 1 }: { product: Product; quantity: number }
  ) {
    commit('setCheckoutStatus', null)
    if (product.stock > 0) {
      await shop.addToCart(product.id, quantity)
      dispatch('syncCartItems')
    }
  },

  async syncCartItems({ commit }) {
    const items = await shop.getCartItems()
    commit('setCartItems', { items })
  },

  async removeFromCart({ dispatch }, product: Product) {
    await shop.removeFromCart(product.id)
    dispatch('syncCartItems')
  },
}

const mutations = {
  setCartItems(state: CartState, { items }: { items: CartItem[] }) {
    state.items = items
  },

  setCheckoutStatus(state: CartState, status: CheckoutStatus) {
    state.checkoutStatus = status
  },
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
}
