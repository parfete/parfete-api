FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /src

COPY ./src/Parfete.Api/Parfete.Api.csproj ./Parfete.Api/Parfete.Api.csproj
COPY ./src/Parfete.Parties/Parfete.Parties.csproj ./Parfete.Parties/Parfete.Parties.csproj
COPY ./src/Parfete.Parties.Service/Parfete.Parties.Service.csproj ./Parfete.Parties.Service/Parfete.Parties.Service.csproj

RUN dotnet restore ./Parfete.Api/Parfete.Api.csproj

COPY ./src/ ./

RUN dotnet build ./Parfete.Api/Parfete.Api.csproj --no-restore -c Release -o /build/

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as final

EXPOSE 80
EXPOSE 443

WORKDIR /app

COPY --from=build /build/ .

ENTRYPOINT [ "dotnet", "Parfete.Api.dll" ]