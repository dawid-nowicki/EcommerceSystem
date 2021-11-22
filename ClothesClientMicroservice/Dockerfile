FROM node:14.18.1 as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod
FROM nginx:alpine
COPY --from=node /app/dist/clothes-client-microservice /usr/share/nginx/html