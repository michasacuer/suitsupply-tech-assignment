# Suitsupply tech assignment
![apisusu](https://github.com/michasacuer/suitsupply-tech-assignment/actions/workflows/apisusu.yml/badge.svg)

Postman collection in `resources` directory.

Swagger: https://app.swaggerhub.com/apis-docs/michasacuer/Suitsupply.alterations/1-oas3

(client credentials in postman collection)

The only way to post messages to queue/topic is doing it manually by SB Explorer or API.

## Comment:
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
- Each employee logs in to the application himself using App registration
- Instead of app service WebJob; go with `BackgroundService` in `API` deployment

## Big picture:

![](https://github.com/michasacuer/suitsupply-tech-assignment/blob/main/resources/bigpicture.png)

## RG (with susu naming convention ;D)

![image](https://user-images.githubusercontent.com/37336963/182036143-b49c8707-da2b-432a-a3b3-9d95b627e9f1.png)

