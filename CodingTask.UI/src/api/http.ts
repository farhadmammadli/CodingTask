import axios from 'axios'

export const BASE_URL = process.env.BASE_API_URL;

const instance = axios.create({
  baseURL: BASE_URL + '/api',
  timeout: 10000,
})

export default instance
