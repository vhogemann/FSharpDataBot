FROM ubuntu:20.04 as base
RUN apt-get update
RUN apt-get install -y wget
RUN wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb
RUN rm packages-microsoft-prod.deb
RUN apt-get update
RUN apt-get install -y apt-transport-https
RUN apt-get install -y dotnet-sdk-3.1
RUN apt-get install -y libgdiplus
WORKDIR /app

FROM base as restore
WORKDIR /src
COPY ["DataBot/DataBot.fsproj", "DataBot/"]
RUN dotnet restore "DataBot/DataBot.fsproj"

FROM restore as build
WORKDIR /src
COPY . .
RUN dotnet build "DataBot/DataBot.fsproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "DataBot/DataBot.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ARG TWITTER_CONSUMER_SECRET
ENV TWITTER_CONSUMER_SECRET=$TWITTER_CONSUMER_SECRET

ARG TWITTER_CONSUMER_KEY
ENV TWITTER_CONSUMER_KEY=$TWITTER_CONSUMER_KEY

ARG TWITTER_TOKEN_SECRET
ENV TWITTER_TOKEN_SECRET=$TWITTER_TOKEN_SECRET

ARG TWITTER_TOKEN
ENV TWITTER_TOKEN=$TWITTER_TOKEN

ARG USE_SQLITE
ENV USE_SQLITE=$USE_SQLITE

ARG COSMOS_DB_ACCOUNT_ENDPOINT
ENV COSMOS_DB_ACCOUNT_ENDPOINT=$COSMOS_DB_ACCOUNT_ENDPOINT

ARG COSMOS_DB_ACCOUNT_KEY
ENV COSMOS_DB_ACCOUNT_KEY=$COSMOS_DB_ACCOUNT_KEY

ARG COSMOS_DB_DATABASE_NAME
ENV COSMOS_DB_DATABASE_NAME=$COSMOS_DB_DATABASE_NAME

ENTRYPOINT ["dotnet", "DataBot.dll"]
