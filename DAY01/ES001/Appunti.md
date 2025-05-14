
# Esempio pubblicazoine app dotnet in appsevice in azure


```powershell

azd init --template https://github.com/Azure-Samples/quickstart-deploy-aspnet-core-app-service.git

az account list-locations

az account list-locations -o table

azd auth login

# Deploy
azd up -e ES001-dev

# Distruzione
azd down -e ES001-dev
```