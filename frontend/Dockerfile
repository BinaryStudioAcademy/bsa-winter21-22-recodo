FROM node:lts as build
COPY package.json package-lock.json ./
RUN npm install && mkdir /build && mv ./node_modules ./build
WORKDIR /build
COPY . .
RUN npm run build

FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /build/dist/* /usr/share/nginx/html
EXPOSE 80 443
ENTRYPOINT ["nginx", "-g", "daemon off;"]
