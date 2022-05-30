FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /src

COPY ./src/Parfete.Api/Parfete.Api.csproj ./Parfete.Api/Parfete.Api.csproj
COPY ./src/Parfete.Users.Service/Parfete.Users.Service.csproj ./Parfete.Users.Service/Parfete.Users.Service.csproj
COPY ./src/Parfete.Users/Parfete.Users.csproj ./Parfete.Users/Parfete.Users.csproj

RUN dotnet new sln
RUN dotnet sln add Parfete.Api/ Parfete.Users.Service/ Parfete.Users/
RUN dotnet restore

COPY ./src/Parfete.Api/ ./Parfete.Api/
COPY ./src/Parfete.Users.Service/ ./Parfete.Users.Service/
COPY ./src/Parfete.Users/ ./Parfete.Users/

RUN dotnet build -c Release -o /build/

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as final

ARG ASPNETCORE_Kestrel__Certificates__Default__Password

ENV ASPNETCORE_URLS="https://+;http://+"
ENV ASPNETCORE_HTTPS_PORT=443
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=${ASPNETCORE_Kestrel__Certificates__Default__Password}

EXPOSE 80
EXPOSE 443

WORKDIR /app

COPY --from=build /build/ .

ENTRYPOINT [ "dotnet", "Parfete.Api.dll" ]