export type Product = {
  id: number
  name: string
  description: string
  price: number
  stock: number
  images: string[]
}

export type CartItem = {
  id: number
  productId: number
  product: Product
  userId: number
  user: null
  quantity: number
}
