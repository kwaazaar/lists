FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM microsoft/dotnet:2.2-sdk AS build

# Install keys required for node and set up node (the old aspnetcore-build images came with node, the dotnet-sdk do not)
ENV NODE_VERSION 10.15.0
ENV NODE_DOWNLOAD_SHA f0b4ff9a74cbc0106bbf3ee7715f970101ac5b1bbe814404d7a0673d1da9f674
ENV NODE_DOWNLOAD_URL https://nodejs.org/dist/v$NODE_VERSION/node-v$NODE_VERSION-linux-x64.tar.gz

RUN curl -SL "$NODE_DOWNLOAD_URL" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs

WORKDIR /src
COPY ["list.csproj", "./"]
RUN dotnet restore "list.csproj"
COPY . .


FROM build AS publish
RUN dotnet publish "list.csproj" -c Release -o /app


FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "list.dll"]