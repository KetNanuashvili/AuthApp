# base image - .NET 8 runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# build image - .NET 8 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# მხოლოდ csproj-ის კოპირება
COPY Submit.csproj ./
RUN dotnet restore "Submit.csproj"

# პროექტის კოპირება
COPY . ./
RUN dotnet publish "Submit.csproj" -c Release -o /app/publish

# final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Submit.dll"]
