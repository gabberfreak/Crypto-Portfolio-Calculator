# Crypto-Portfolio-Calculator
ASP.NET Core MVC  - Crypto Portfolio Calculator Web project

## Multi-tier based architecture
***
- ASP.NET Core Web API - Middleware for extracting data from CoinLore API
- ASP.NET Coinlore - Library Service project
*****
- ASP.NET Web MVC project - client-side
- Domain project - storing models
- Service project - main functionality of web api for calculating and extracting data

## Used Technologies:

- HTTP Client Factory for communicating between APIs
- ASP.NET CORE MVC 
- SignalR (for updating Initial Portfolio Values each 5 minutes)
- Logger for logging basic operations


