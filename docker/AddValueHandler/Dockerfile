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

WORKDIR /src/AddValueHandler
COPY /src/AddValueHandler/AddValueHandler.csproj .
RUN dotnet restore

COPY /src /src

RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet

ENV MESSAGE_QUEUE_URL="nats://template-mq:4222"

WORKDIR /app

COPY --from=builder /app .

CMD dotnet AddValueHandler.dll




