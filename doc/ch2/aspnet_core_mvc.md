#aspnet_core_mvc

> the asp.net core mvc framework is a lightweight , open source, highly testable presentation framework optimized for use with ASP.NET Core.

- Routing
- Web APIs
- Model binding
- Model validation
- Dependency injection
- Filters
- Areas
- Testability
- Razor view engine
- Strongly typed views
- View Components
- Tag Helpers


### Empty template:

an empty project template for creating ASP.NET Core application. this template does not have any content in it.

### API:

a project template for creating ASP.NET Core application with an example controller for a RESTful HTTP service. this template can also be used for ASP.NET Core MVC view and controllers.

### Web Application:

a project template for creating ASP.NET Core application with an example ASP.NET Core Razor Pages content

### Web Application(Model-View-Controller):

a project template for creating ASP.NET Core application with example ASP.NET Core MVC views and controllers. this template can also be used for RESTful HTTP services.

### Angular,React.js and React.js with Redux:

a project template for creating ASP.NET Core application with javascript-framework based ASP.NET Core web applications.

### command line
```dotnetcli
dotnet new --list or dotnet new -l
```
[相關連結](https://docs.microsoft.com/zh-tw/dotnet/core/tools/dotnet-new)

## selecting authentication type

- no authentication:this option will not add any authentication packages to the application

- individual user accounts: individual user account template design for connecting to an azure active directory b2c application will provide us with all the authentication and authorization data.

- work or school accounts: this is for the enterprises, organizations and schools that authenticate users with office 365, active directory,or azure directory services can also use this option.

- windows authentication: windows authentication mostly be used applications within the internet environment