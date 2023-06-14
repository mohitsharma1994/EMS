# Test Project for Employee

# Technology
1. .NET 6 Core MVC
2. .NET 6 Web API
3. SQL Server
4. Entity Framework

# Steps to Run Project
1. Clone Project into local directory
2. Build Project in Visual Studio. Before making build, please make ensure you have .NET 6 SDK and VS 2022 in your system.
3. Change database connectionstring in `appsettings.json` of WebAPI project
4. Run `update-database` command in `Nuget Package Manager Console`.
5. Run solution using F5. Please make ensure both projects `API` and `Web` Project both have run.
6. Web Project will run on `7099` port on `localhost`.
7. For employee list page, please go to `https://localhost:7099/Employee/list`
