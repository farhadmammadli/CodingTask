import axios from 'axios'

export const BASE_API_URL = process.env.BASE_API_URL;

const instance = axios.create({
  baseURL: BASE_API_URL + '/api',
  timeout: 10000,
})

export default instance
