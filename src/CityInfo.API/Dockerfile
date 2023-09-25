FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app 

EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /build
COPY ./src/CityInfo.API/CityInfo.API.csproj ./src/CityInfo.API/
COPY ./src/tests/CityInfo.API.Test/CityInfo.API.Test.csproj ./src/tests/CityInfo.API.Test/
COPY ./CityInfo.API.sln .
RUN dotnet restore 

COPY . .
RUN dotnet publish ./src/CityInfo.API/CityInfo.API.csproj -c Release -o /out

FROM build as test
LABEL test=true
RUN dotnet test -c Release --results-directory /results --logger "trx;LogFileName=test_results.trx" ./src/tests/CityInfo.API.Test/CityInfo.API.Test.csproj || true


FROM base AS final
LABEL type=webapp
WORKDIR /app
COPY --from=build /out .
COPY --from=test /results /test-results/
ENTRYPOINT ["dotnet", "CityInfo.API.dll"]
