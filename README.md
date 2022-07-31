# Suitsupply tech assignment
Postman collection in `resources` directory.

Swagger: https://app.swaggerhub.com/apis-docs/michasacuer/Suitsupply.alterations/1-oas3

## Comment:
This business process would be easy to do using Logic App and possibly Function app for data validation. It would certainly be a cheaper (no or less code to maintanance).

## Used Azure technologies:
- Active Directory (app registrations)
- Application Insights
- App Service
- Table Storage
- SendGrid
- ServiceBus
- Key Vault

![image](https://user-images.githubusercontent.com/37336963/182036143-b49c8707-da2b-432a-a3b3-9d95b627e9f1.png)


## Big picture:

PC in store is logged in the name of Azure App Registration that have access to Suitsupply Alteration API. 

![](https://github.com/michasacuer/suitsupply-tech-assignment/blob/main/resources/bigpicture.png)

## LLD (low level design)
