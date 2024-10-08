# Clean Architecture - CQRS - DDD
Repository containing a representation of Clean Architecture with some of known patterns like CQRS and Domain Driven Design in a .Net project.

It consists of one ASP.NET Web API, the incoming requests go through the Application Core Pipeline for data validation using FluentValidation and Business Rule validation.

EntityFramework is used as ORM with SqlLite in the Persistence Layer. 

Logging, tracing and metrics are collected using OpenTelemetry auto instrumentation for .Net.

Caching with Redis Cache and Idempotency support

Aspire dashboard to visualize OpenTelemetry data in the local environement.

Unit test for the Domain layer and Intregation tests for the Persistence Layer. 

The .NET application is containerized with Docker.

### Author - Francisco Sanches - [LinkedIn](https://fr.linkedin.com/in/francisco-sanches-319a7367)
