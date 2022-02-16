FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ./*.sln ./
COPY ./Recodo.API/*.csproj ./Recodo.API/Recodo.API.csproj
COPY ./Recodo.BLL/*.csproj ./Recodo.BLL/Recodo.BLL.csproj
COPY ./Recodo.DAL/*.csproj ./Recodo.DAL/Recodo.DAL.csproj
COPY ./Recodo.Common/*.csproj ./Recodo.Common/Recodo.Common.csproj
RUN dotnet restore

COPY ./ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /src/out .
EXPOSE 5050
ENTRYPOINT ["dotnet", "Recodo.API.dll"]