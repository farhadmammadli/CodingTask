# Stage 1: Build Vue app
FROM node:18 AS build
WORKDIR /app
COPY ./CodingTask.UI/package*.json ./
RUN npm install
COPY ./CodingTask.UI ./
RUN npm run build

# Stage 2: Serve with Nginx
FROM nginx:alpine
COPY ./CodingTask.UI/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]