FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /src

COPY ./src/Parfete.Api/ ./Parfete.Api/

RUN dotnet new sln

RUN dotnet sln add Parfete.Api/

RUN dotnet restore

RUN dotnet build --no-restore -c Release -o /build/

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as final

WORKDIR /app

COPY --from=build /build/ .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Parfete.Api.dll
