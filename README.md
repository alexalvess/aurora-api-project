![Aurora Project](./img/logo.png)

# What is Aurora project?
It's an open source project, written in .NET, currently in version 6.

The project's goal is to show how can we use the Hexagonal Architecture concepts and using some concepts like DDD to create an API.

## Business proposal:
This project is a simple PPE Management. The principle idea is to register workers and PPE and, with this data, allow to transfer PPE to a worker.
Besides that, this system allows that you see all the PPE and who has a PPE and notify if the PPE is near to expire.

### Abbreviations:
* NIN: National Insurance Number (as CPF in Brazil)
* PPE: Personal Protective Equipment
* DDD: Domain Driven Design

## How to use:
1. Clone this project to into your machine
2. Run MongoDB container (like on Docker)
    2.1. Inform the right connection string in project
3. Finally, build and run the application

## Technologies:
* .NET 6
* C# 10
* MongoDB
* FluentValidation
* Swagger
* .NET Core Native DI

## Architecture:
* Hexagonal Architecture
* S.O.L.I.D. principles
* Clean Code
* Domain Validations
* Domain Notifications
* Domain Driven Design
* Repository Pattern
* Notification Pattern
* Value Types

## News:
**v1.5 --- 2021-11-30**
* Migrate to Hexagonal Architecture
* Include MongoDB
* Refactors to improve the code

**v1.4 --- 2020-09-28**
* CI/CD by GitHub Actions
* Include integration tests using BDD with SpecFlow
    * scenario of register a worker
    * scenario of update a worker
* Bug corrections

**v1.3 --- 2020-07-30**
* Changed some Primitive Types to Value Types
* Changed the business idea principle

**v1.2 --- 2020-06-30**
* Implemented Notification Pattern
* Implemented Domain Validations and Notifications
* Using some concepts of Clean Architecture
    * Entities
    * Interface Adapters
* Changed the framework validations to Flunt
* Using mapper by extension methods

**v1.1 --- 2020-06-24**
* Updated the project name
* Updated the project's SDK to .NET Core 3.1 version
* Added the Swagger framework to document the API
* Corrections to end-points
* Published in Azure

**v1.0 --- 2018-06-09**
* Create the project in .NET Core 2.0 version
* Structured the project on layer architecture 
* Used the Service layer to business rules
* Used the FluentValidation library
* Configured the connection to MySql database
* Used EntityFramework

---

## Hexagonal Architecture
![Design Architectural](./img/aurora_architecture.png)

### Why did I choose this design?
* It's very easy to include, or exclude, a framework or external library in a separate DLL.
* The focus is in Core layer. So the business rules/your domain stay very uncoupled of external things.
* I'm an enthusiast for this Design 😁🤓


---

## Why Aurora?
The name Aurora came from the natural event called Aurora Borealis. It is a scientific event described by the interaction between the earth's magnetic layer and energized particles from the solar wind.

A curiosity about such an event is that what we see in photographs is not always the same image that is seen live.

For more information, look this [link](https://www.hipercultura.com/fenomenos-naturais/).

## About:
The Aurora project was developed by [Alex Alves](https://www.linkedin.com/in/alexalvess/).

---

# References:
* https://medium.com/@alexalves_85598/criando-uma-api-em-net-core-baseado-na-arquitetura-ddd-2c6a409c686
* https://alexalvess.medium.com/organizando-seu-projeto-net-com-arquitetura-hexagonal-parte-01-a598662a3818
* https://alexalvess.medium.com/organizando-seu-projeto-net-com-arquitetura-hexagonal-parte-02-fe9a8ed6ab02