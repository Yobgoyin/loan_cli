FROM node:16.17.0

RUN npm install --production=false -g http-server

RUN mkdir /app

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

EXPOSE 8080

CMD ["http-server","dist"]