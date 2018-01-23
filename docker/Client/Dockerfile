FROM joelvbrinkley/angular-build:latest as builder

RUN mkdir ng-app

WORKDIR /ng-app

COPY /src/Client/template-app/package.json .

RUN npm update && npm set progress=false && npm install

COPY /src/Client/template-app/ .

RUN ng build --prod

EXPOSE 3000

ENTRYPOINT node server.js