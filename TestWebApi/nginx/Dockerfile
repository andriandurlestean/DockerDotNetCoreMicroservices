# Stage 1 - the build process
FROM node:7.10 as build-deps
WORKDIR /usr/src/app
COPY frontapp/package.json frontapp/yarn.lock ./
RUN yarn
COPY frontapp/. ./
RUN yarn build

FROM nginx
COPY --from=build-deps /usr/src/app/build /usr/share/nginx/html
COPY nginx/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80