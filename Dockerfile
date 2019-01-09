FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["list.csproj", "./"]
RUN dotnet restore "list.csproj"
COPY . .
WORKDIR "/src/WebApplication1"
RUN dotnet build "list.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "list.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "list.dll"]