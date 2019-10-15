FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
COPY . .
EXPOSE 5000
EXPOSE 443
ENTRYPOINT ["dotnet", "OrderApi.dll"]
