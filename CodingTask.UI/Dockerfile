# Step 1: Build the Vue app
# Use an official Node image as the build stage
FROM node:18 AS build

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json to install dependencies
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the Vue app
RUN npm run build

# Step 2: Serve the built files with nginx
# Use an official nginx image as the serve stage
FROM nginx:stable-alpine

# Copy the built files from the previous stage to the nginx html directory
COPY --from=build /app/dist /usr/share/nginx/html

# Copy custom nginx configuration file if needed (optional)
# COPY nginx.conf /etc/nginx/nginx.conf

# Expose port 80
EXPOSE 80

# Start nginx server
CMD ["nginx", "-g", "daemon off;"]