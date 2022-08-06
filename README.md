# Suitsupply tech assignment

## Comment:
I mainly focused on the architecture and tried to understand the problem to be solved that the user is facing.

This business process would be easy to do using Logic App and possibly Function app for data validation (but not necessarily because in logic app you can put some JavaScript code). It would be much cheaper (no or less code to maintanance) and easier to creat.

## Used Azure technologies:
- Active Directory (app registrations) with managed identity
- Application Insights
- App Service
- Table Storage
- SendGrid
- ServiceBus
- Key Vault

## CI:
- Gihub actions.
- webjob deployed manually because of Github actions restrictions.

## Potential improvements:
- LLD and HLD diagrams (low/high level design)
- Each employee logs in to the application himself using App registration instead of using pre-logged-in device as service principal.
- Docker support & integration with Azure Container Registry to more fluent deployment.
- Instead of app service WebJob; go with `BackgroundService` in `API` deployment (hosted service).
- Email templates to HTML; can use RazorViewEngine to produce fancy parametrized emails

## Local development:

Go to `Suitsupply.Alteration.Api` direcory and init user secrets:

```powershell
dotnet user-secrets init

dotnet user-secrets set "secret" "value"
```

Or setup Azure resources on your own subscription and login into Azure through cli:

```powershell
az login
```

`DefaultAzureCredential` used in solution will automatically find credentials. It will lookup for credentials in sequence mentioned here: https://docs.microsoft.com/en-us/dotnet/api/azure.identity.defaultazurecredential?view=azure-dotnet


## Architecture big picture:

![](https://github.com/michasacuer/suitsupply-tech-assignment/blob/main/resources/bigpicture.png)

## RG (with susu naming convention ;D)

![image](https://user-images.githubusercontent.com/37336963/182036143-b49c8707-da2b-432a-a3b3-9d95b627e9f1.png)

