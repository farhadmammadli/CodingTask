import axios from 'axios'

export const BASE_URL = 'https://coding-task-backend.farhadmammadli.com'

const instance = axios.create({
  baseURL: BASE_URL + '/api',
  timeout: 10000,
})

export default instance
