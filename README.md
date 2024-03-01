# Patient Form with WEB API
In this project front-end is developed using MVC Core 8.0, back-end is developed using Web API 8.0, Entity Framework Core is used to insert, update, delete and display patirnt information and MS SQL Server as database. Also the repository pattern is applied here.

# Project Requirements
- .Net Core 8.0
- Microsoft SQL Server 2019 Express Edition
- Microsoft SQL Server Management Studio
  
# Setup Database
- Install the project requirements
- Open project in IDE and run
- Change the ConnectionStrings (DB) in appsettings.json of PatientForm.WebApi
- From the top menu select - Tool > NuGet Package Manager > Package Manager Console
- Select PatientForm.WebApi as startup project
- Select PatientForm.EntityModel as DefaultProject in Package Manager Console
- Write below Command one by one
- ADD-MIGRATION INITIAL
- UPDATE-DATABASE

# Quickstart
- To run project Right click on the solution and select properties (Alt + Enter)
- Select Multiple startup projects
- Start PatientForm.WebApi and PatientForm.WebCore from the Action dropdown with Debug Target Https
- ![image](https://github.com/raseldotnetcoder/PatientForm/assets/115859775/d68219b7-4b45-49b6-b31a-db4658ff15fc)

