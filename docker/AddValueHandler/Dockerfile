FROM microsoft/dotnet:sdk as builder

WORKDIR /src/Core
COPY /src/Core/Core.csproj .
RUN dotnet restore

WORKDIR /src/Messaging
COPY /src/Messaging/Messaging.csproj .
RUN dotnet restore

WORKDIR /src/Persistance
COPY /src/Persistance/Persistance.csproj .
RUN dotnet restore

WORKDIR /src/Logging
COPY /src/Logging/Logging.csproj .
RUN dotnet restore

WORKDIR /src/AddValueHandler
COPY /src/AddValueHandler/AddValueHandler.csproj .
RUN dotnet restore

COPY /src/Core/ /src/Core/ 
COPY /src/Messaging/ /src/Messaging/ 
COPY /src/Persistance/ /src/Persistance/ 
COPY /src/Logging/ /src/Logging/ 
COPY /src/AddValueHandler/ /src/AddValueHandler/ 

RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet

ENV MESSAGE_QUEUE_URL="nats://template-mq:4222" \
    CONNECTION_STRING="Server=template-database;Initial Catalog=TemplateDatabase;User Id=sa;Password=Pword!!!;"

WORKDIR /app

COPY --from=builder /app .

CMD dotnet AddValueHandler.dll




