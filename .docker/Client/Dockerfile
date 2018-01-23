FROM nodejs as builder

RUN mkdir ng-app

WORKDIR /ng-app

COPY Client/template-app/package.json .

RUN npm update && npm set progress=false && npm install

COPY Client/template-app/ .

RUN npm build --prod

EXPOSE 3000

ENTRYPOINT node server.js