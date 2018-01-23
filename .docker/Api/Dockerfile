FROM microsoft/aspnetcore-build as builder

WORKDIR /
COPY Template.sln .

WORKDIR /Api
COPY Api/Api.csproj .

WORKDIR /Core
COPY Core/Core.csproj .

WORKDIR /Tests/UnitTests
COPY Tests/UnitTests/UnitTests.csproj .

WORKDIR /Tests/AcceptanceTests
COPY Tests/AcceptanceTests/AcceptanceTests.csproj .

WORKDIR /
RUN dotnet restore

COPY . .

WORKDIR /Api
RUN dotnet publish -c Release -o C:\build --no-restore

FROM microsoft/aspnetcore:2.0

WORKDIR /app

COPY --from=builder /build .

ENTRYPOINT [ "dotnet", "Api.dll" ]