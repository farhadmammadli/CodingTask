import { Product } from '@/api/models'
import * as shop from '@/api/shop'

export type ProductsState = {
  all: Product[]
}

const state = (): ProductsState => ({
  all: [],
})

const getters = {}

const actions = {
  async getAllProducts({ commit }) {
    const products = await shop.getProducts()
    commit('setProducts', products)
  },
}

const mutations = {
  setProducts(state: ProductsState, products: Product[]) {
    state.all = products
  },
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
}
