# Phase 1: Project Foundation & Architecture Setup - Task List

This document breaks down Phase 1 into granular, actionable tasks for implementation.

## 1. Solution & Clean Architecture Initialization
- [ ] Create `GEMINI.md` at the root of the workspace to codify overarching project rules, tech stack details, and coding standards.
- [ ] Initialize the Git repository with a standard `.gitignore` for .NET.
- [ ] Create the main `.sln` (Solution) file named `Sensorialab.sln`.
- [ ] Create the **Domain** Project (`Sensorialab.Domain`):
  - Use `classlib` template.
  - Remove default `Class1.cs`.
  - Add standard folders: `Entities`, `Enums`, `ValueObjects`, `Exceptions`, `Interfaces`.
- [ ] Create the **Shared** Project (`Sensorialab.Shared`):
  - Use `classlib` template.
  - Add standard folders: `DTOs`, `Enums`, `Constants`.
- [ ] Create the **Application** Project (`Sensorialab.Application`):
  - Use `classlib` template.
  - Reference `Sensorialab.Domain` and `Sensorialab.Shared`.
  - Add standard folders: `Interfaces`, `Services`, `Validators`.
- [ ] Create the **Infrastructure** Project (`Sensorialab.Infrastructure`):
  - Use `classlib` template.
  - Reference `Sensorialab.Application`.
  - Add standard folders: `Data`, `Services` (for external integrations like SMTP).
- [ ] Create the **API** Project (`Sensorialab.Api`):
  - Use `webapi` or `web` template targeting .NET 10 Minimal APIs.
  - Reference `Sensorialab.Application`, `Sensorialab.Infrastructure`, and `Sensorialab.Shared`.
  - Add standard folders: `Endpoints`, `Middleware`, `Extensions`.
- [ ] Create the **Tests** Project (`Sensorialab.Tests`):
  - Use `xunit` or `nunit` template (recommend `xunit` for .NET).
  - Reference all other projects.
  - Add folders: `Domain`, `Application`, `Infrastructure`, `Architecture`.
- [ ] Add all projects to the `Sensorialab.sln`.

## 2. API Setup & Documentation
- [ ] In `Sensorialab.Api`, clean up default weather forecast endpoints.
- [ ] Configure OpenAPI / Swagger generation in `Program.cs`.
- [ ] Add Swagger UI middleware to serve the documentation.
- [ ] Ensure the API can run successfully (`dotnet run`) and navigate to `/swagger` to view the empty API surface.

## 3. Database & Entity Framework Core Integration
- [ ] Add EF Core NuGet packages to `Sensorialab.Infrastructure`:
  - `Microsoft.EntityFrameworkCore`
  - `Npgsql.EntityFrameworkCore.PostgreSQL` (for PostgreSQL)
- [ ] Add EF Core Design packages to `Sensorialab.Api` (for running migrations):
  - `Microsoft.EntityFrameworkCore.Design`
- [ ] Create the initial `AppDbContext` inside `Sensorialab.Infrastructure/Data`.
- [ ] Add a `docker-compose.yml` at the solution root to spin up a local PostgreSQL instance.
- [ ] Define the default PostgreSQL connection string in `appsettings.Development.json` of the API project.
- [ ] Register `AppDbContext` in the API `Program.cs` DI container using the connection string.
- [ ] (Optional at this stage but recommended) Create an initial, empty EF Core migration to verify connectivity (`dotnet ef migrations add InitialCreate`).

## 4. Observability Setup (OpenTelemetry)
- [ ] Add OpenTelemetry NuGet packages to `Sensorialab.Api`:
  - `OpenTelemetry.Extensions.Hosting`
  - `OpenTelemetry.Instrumentation.AspNetCore`
  - `OpenTelemetry.Instrumentation.Http`
  - `OpenTelemetry.Instrumentation.EntityFrameworkCore`
  - `OpenTelemetry.Exporter.OpenTelemetryProtocol` (OTLP) or `OpenTelemetry.Exporter.Console` (for initial testing).
- [ ] Configure OpenTelemetry in `Program.cs` (Tracing, Metrics, Logging).
- [ ] Add a local observability backend (e.g., Jaeger or Prometheus) to the `docker-compose.yml` alongside PostgreSQL.
- [ ] Configure the OTLP exporter in `appsettings.json` to point to the local backend.
- [ ] Test tracing by making a request to the API and checking the backend UI.

## 5. Architecture Tests & CI/CD Pipeline
- [ ] Add the `NetArchTest.Rules` NuGet package to `Sensorialab.Tests`.
- [ ] Write the first Architecture Test: `Domain_Should_Not_Have_Dependency_On_Infrastructure()`.
- [ ] Create a GitHub Actions workflow file: `.github/workflows/ci.yml`.
- [ ] Configure the CI workflow to trigger on `push` and `pull_request` to `main`.
- [ ] Add steps to the CI workflow:
  - Setup .NET 10.
  - Run `dotnet format --verify-no-changes` (Linting check).
  - Run `dotnet build`.
  - Run `dotnet test` with coverage collection (e.g., using `coverlet.collector`).
  - Add a step to enforce the >70% line coverage requirement (using tools like `ReportGenerator` or a custom script to parse the coverage output and fail the build if below 70%).

## Checkpoint & Review
- Run `docker-compose up -d`.
- Run `dotnet build`.
- Run `dotnet test`.
- Manually run `dotnet format` to ensure everything is compliant.
- Ensure the API runs, Swagger loads, and no errors occur.
