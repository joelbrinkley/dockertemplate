FROM microsoft/aspnetcore-build:2 as builder

WORKDIR /src
COPY /src/Template.sln .

WORKDIR /src/Api
COPY /src/Api/Api.csproj .

WORKDIR /src/Core
COPY /src/Core/Core.csproj .

WORKDIR /src/Tests/UnitTests
COPY /src/Tests/UnitTests/UnitTests.csproj .

WORKDIR /src/Tests/AcceptanceTests
COPY /src/Tests/AcceptanceTests/AcceptanceTests.csproj .

WORKDIR /src
RUN dotnet restore

COPY /src /src

WORKDIR /src/Api
RUN dotnet publish -c Release -o C:\build --no-restore

FROM microsoft/aspnetcore:2.0

WORKDIR /app

COPY --from=builder /build .

ENTRYPOINT [ "dotnet", "Api.dll" ]