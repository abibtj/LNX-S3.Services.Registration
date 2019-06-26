FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim
WORKDIR /app
COPY ./bin/Release/netcoreapp2.2 .
ENV ASPNETCORE_URLS http://*:80
#ENV ASPNETCORE_URLS https://*:443
#ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 80
ENTRYPOINT dotnet S3.Services.Registration.dll


#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
#WORKDIR /src
#COPY ["src/S3.Services.Registration/S3.Services.Registration.csproj", "src/S3.Services.Registration/"]
#RUN dotnet restore "src/S3.Services.Registration/S3.Services.Registration.csproj"
#COPY . .
#WORKDIR "/src/src/S3.Services.Registration"
#RUN dotnet build "S3.Services.Registration.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "S3.Services.Registration.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "S3.Services.Registration.dll"]