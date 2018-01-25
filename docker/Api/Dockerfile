FROM microsoft/aspnetcore-build:2 as builder

WORKDIR /src
COPY /src/Template.sln .

WORKDIR /src/Api
COPY /src/Api/Api.csproj .

WORKDIR /src/Core
COPY /src/Core/Core.csproj .

WORKDIR /src/Messaging
COPY /src/Messaging/Messaging.csproj .

WORKDIR /src
RUN dotnet restore

COPY /src /src

WORKDIR /src/Api
RUN dotnet publish -c Release -o C:\build 

FROM microsoft/aspnetcore:2.0

WORKDIR /app

COPY --from=builder /build .

ENTRYPOINT [ "dotnet", "Api.dll" ]