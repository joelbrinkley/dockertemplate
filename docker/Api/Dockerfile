FROM microsoft/aspnetcore-build:2 as builder

WORKDIR /src
COPY /src/Template.sln .

WORKDIR /src/Api
COPY /src/Api/Api.csproj .
RUN dotnet restore

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

COPY /src/Api/ /src/Api/ 
COPY /src/Core/ /src/Core/ 
COPY /src/Persistance/ /src/Persistance/ 
COPY /src/Messaging/ /src/Messaging/ 
COPY /src/Logging/ /src/Logging

WORKDIR /src/Api
RUN dotnet publish -c Release -o C:\build 

FROM microsoft/aspnetcore:2.0

WORKDIR /app

COPY --from=builder /build .

ENTRYPOINT [ "dotnet", "Api.dll" ]