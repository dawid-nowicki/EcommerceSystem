#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ClothesDataMicroservice.Rest/ClothesDataMicroservice.Rest.csproj", "ClothesDataMicroservice.Rest/"]
COPY ["ClothesDataMicroservice.Logic/ClothesDataMicroservice.Logic.csproj", "ClothesDataMicroservice.Logic/"]
COPY ["ClothesDataMicroservice.Model/ClothesDataMicroservice.Model.csproj", "ClothesDataMicroservice.Model/"]
RUN dotnet restore "ClothesDataMicroservice.Rest/ClothesDataMicroservice.Rest.csproj"
COPY . .
WORKDIR "/src/ClothesDataMicroservice.Rest"
RUN dotnet build "ClothesDataMicroservice.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClothesDataMicroservice.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClothesDataMicroservice.Rest.dll"]