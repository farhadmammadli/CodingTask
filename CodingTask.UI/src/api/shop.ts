import http from './http'
import { CartItem, Product } from './models'

export function getProducts() {
  return http.get<Product[]>('/products').then((response) => response.data)
}

export function getProductById(id: number) {
  return http.get<Product>(`/products/${id}`).then((response) => response.data)
}

export function getCartItems() {
  return http.get<CartItem[]>('/cart').then((response) => response.data)
}

export function addToCart(productId: number, quantity: number) {
  const formdata = new FormData()
  formdata.append('productId', productId.toString())
  formdata.append('quantity', quantity.toString())
  return http.post<void>('/cart', formdata).then((response) => response.data)
}

export function removeFromCart(productId: number) {
  return http
    .delete<void>(`/cart/product/${productId}`)
    .then((response) => response.data)
}

export function updateCart(productId: number, quantity: number) {
  const formdata = new FormData()
  formdata.append('productId', productId.toString())
  formdata.append('quantity', quantity.toString())
  return http.put<void>('/cart', formdata).then((response) => response.data)
}

export function checkout() {
  return http.post<void>('/orders/checkout').then((response) => response.data)
}
