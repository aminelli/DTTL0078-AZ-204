# Integrazione REDIS


## Preparazione app

```powershell

# Creazione applicazione dotnet web
dotnet new web

# Installazione sdk per interfacciamento con redis e gestione del caching
dotnet add package Microsoft.AspNetCore.OutputCaching.StackExchangeRedis



# build progetto
dotnet build

```