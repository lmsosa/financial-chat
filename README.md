 <img align="left" width="116" height="116" src="https://www.jobsity.com/themes/custom/jobsity/images/icons/logo2.svg" />
 
 # Financial Chat Room

<br/>

This is an exercise that implements a thematic chat room (financial in this case), using .NET Core technology.


## Technologies
* .NET Core 2.2
* ASP .NET Core 2.2.0
* Entity Framework Core 2.2.6
* SignalR


## Prerequisites

- MS SQL Local DB 
- [Rabbit MQ Server](https://www.rabbitmq.com/download.html)
- [.NET Core SDK 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)

## Getting Started

1. Check SQL Local DB and Rabbit MQ configuration on appsettings.json
2. Navigate to `src/Web` and run `dotnet run` to launch the project
3. There are 2 predefined users: 
   lmsosa@gmail.com (pass: Chat123$)
   jeancarlos@gmail.com (pass: Chat123$)
   Anyways you can register a new one and use it.

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.


### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.


### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Web

This layer "Razor Pages" application based on ASP.NET Core 2.2. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

## Support

If you are having problems, please let me know by [sending me an email](mailto:lmsosa@gmail.com).

## License

This project is licensed with the [MIT license](LICENSE).
