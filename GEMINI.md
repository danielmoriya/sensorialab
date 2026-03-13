# Sensorialab: Project Guidelines & Tech Stack

## Overview
Sensorialab is a B2B SaaS platform for the coffee industry, facilitating Q Grader evaluations (SCA CVA and Legacy 2004) and connecting them with customers (Roasters, Importers).

## Tech Stack
- **Runtime:** .NET 10 (Minimal APIs)
- **Database:** PostgreSQL (EF Core)
- **UI:** Blazor WebAssembly (MudBlazor)
- **Background Tasks:** Hangfire
- **Reporting:** QuestPDF
- **Observability:** OpenTelemetry (OTEL)
- **Documentation:** OpenAPI/Swagger
- **Testing:** xUnit, NetArchTest, Coverlet

## Project Architecture (Clean Architecture / DDD)
- **Sensorialab.Domain:** Core entities, value objects, domain logic, and interfaces. No dependencies.
- **Sensorialab.Shared:** Data Transfer Objects (DTOs), constants, and common enums.
- **Sensorialab.Application:** Use cases, services, and business logic. Depends on Domain and Shared.
- **Sensorialab.Infrastructure:** External concerns (Database, SMTP, etc.). Depends on Application.
- **Sensorialab.Api:** Entry point, Minimal APIs, Middleware. Depends on Application and Infrastructure.
- **Sensorialab.Tests:** Unit, Integration, and Architecture tests.

## Coding Standards
- **Naming:** PascalCase for classes/methods, camelCase for local variables.
- **Formatting:** Use `dotnet format` for consistency.
- **Documentation:** Use XML comments for public members in Domain and Application.
- **Validation:** Minimum 70% code coverage.
- **Architecture Enforcement:** Domain must not depend on Infrastructure.

## Git & Workflow
- Use semantic commits (`feat:`, `fix:`, `chore:`, `test:`).
- All changes via Pull Requests (PRs).
- CI pipeline enforces builds, tests, and coverage.
