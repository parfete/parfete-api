FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /src

COPY ./.config/ ./.config/

RUN dotnet tool restore

COPY ./paket.dependencies ./paket.dependencies
COPY ./paket.lock ./paket.lock

RUN dotnet paket restore

COPY ./src/Parfete.Api/ ./src/Parfete.Api/

RUN dotnet build ./src/Parfete.Api/Parfete.Api.fsproj -c Release -o /build/

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as final

EXPOSE 80

WORKDIR /app

COPY --from=build /build/ .

ENTRYPOINT [ "dotnet", "Parfete.Api.dll" ]