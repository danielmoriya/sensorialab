# Phase 1: Project Foundation & Architecture Setup - Task List

This document breaks down Phase 1 into granular, actionable tasks for implementation.

## 1. Solution & Clean Architecture Initialization
- [x] Create `GEMINI.md` at the root of the workspace to codify overarching project rules, tech stack details, and coding standards.
- [x] Initialize the Git repository with a standard `.gitignore` for .NET.
- [x] Create the main `.sln` (Solution) file named `Sensorialab.sln`. (Note: Created as `Sensorialab.slnx` per .NET 10 defaults).
- [x] Create the **Domain** Project (`Sensorialab.Domain`):
  - [x] Use `classlib` template.
  - [x] Remove default `Class1.cs`.
  - [x] Add standard folders: `Entities`, `Enums`, `ValueObjects`, `Exceptions`, `Interfaces`.
- [x] Create the **Shared** Project (`Sensorialab.Shared`):
  - [x] Use `classlib` template.
  - [x] Add standard folders: `DTOs`, `Enums`, `Constants`.
- [x] Create the **Application** Project (`Sensorialab.Application`):
  - [x] Use `classlib` template.
  - [x] Reference `Sensorialab.Domain` and `Sensorialab.Shared`.
  - [x] Add standard folders: `Interfaces`, `Services`, `Validators`.
- [x] Create the **Infrastructure** Project (`Sensorialab.Infrastructure`):
  - [x] Use `classlib` template.
  - [x] Reference `Sensorialab.Application`.
  - [x] Add standard folders: `Data`, `Services` (for external integrations like SMTP).
- [x] Create the **API** Project (`Sensorialab.Api`):
  - [x] Use `webapi` or `web` template targeting .NET 10 Minimal APIs.
  - [x] Reference `Sensorialab.Application`, `Sensorialab.Infrastructure`, and `Sensorialab.Shared`.
  - [x] Add standard folders: `Endpoints`, `Middleware`, `Extensions`.
- [x] Create the **Tests** Project (`Sensorialab.Tests`):
  - [x] Use `xunit` or `nunit` template (recommend `xunit` for .NET).
  - [x] Reference all other projects.
  - [x] Add folders: `Domain`, `Application`, `Infrastructure`, `Architecture`.
- [x] Add all projects to the `Sensorialab.sln`.

## 2. API Setup & Documentation
- [x] In `Sensorialab.Api`, clean up default weather forecast endpoints.
- [x] Configure OpenAPI / Swagger generation in `Program.cs`.
- [x] Add Swagger UI middleware to serve the documentation.
- [x] Ensure the API can run successfully (`dotnet run`) and navigate to `/swagger` to view the empty API surface.

## 3. Database & Entity Framework Core Integration
- [x] Add EF Core NuGet packages to `Sensorialab.Infrastructure`:
  - [x] `Microsoft.EntityFrameworkCore`
  - [x] `Npgsql.EntityFrameworkCore.PostgreSQL` (for PostgreSQL)
- [x] Add EF Core Design packages to `Sensorialab.Api` (for running migrations):
  - [x] `Microsoft.EntityFrameworkCore.Design`
- [x] Create the initial `AppDbContext` inside `Sensorialab.Infrastructure/Data`.
- [x] Add a `docker-compose.yml` at the solution root to spin up a local PostgreSQL instance.
- [x] Define the default PostgreSQL connection string in `appsettings.Development.json` of the API project.
- [x] Register `AppDbContext` in the API `Program.cs` DI container using the connection string.
- [x] (Optional at this stage but recommended) Create an initial, empty EF Core migration to verify connectivity (`dotnet ef migrations add InitialCreate`).

## 4. Observability Setup (OpenTelemetry)
- [x] Add OpenTelemetry NuGet packages to `Sensorialab.Api`:
  - [x] `OpenTelemetry.Extensions.Hosting`
  - [x] `OpenTelemetry.Instrumentation.AspNetCore`
  - [x] `OpenTelemetry.Instrumentation.Http`
  - [x] `OpenTelemetry.Instrumentation.EntityFrameworkCore`
  - [x] `OpenTelemetry.Exporter.OpenTelemetryProtocol` (OTLP) or `OpenTelemetry.Exporter.Console` (for initial testing).
- [x] Configure OpenTelemetry in `Program.cs` (Tracing, Metrics, Logging).
- [x] Add a local observability backend (e.g., Jaeger or Prometheus) to the `docker-compose.yml` alongside PostgreSQL.
- [x] Configure the OTLP exporter in `appsettings.json` to point to the local backend.
- [x] Test tracing by making a request to the API and checking the backend UI.

## 5. Architecture Tests & CI/CD Pipeline
- [x] Add the `NetArchTest.Rules` NuGet package to `Sensorialab.Tests`.
- [x] Write the first Architecture Test: `Domain_Should_Not_Have_Dependency_On_Infrastructure()`.
- [x] Create a GitHub Actions workflow file: `.github/workflows/ci.yml`.
- [x] Configure the CI workflow to trigger on `push` and `pull_request` to `main`.
- [x] Add steps to the CI workflow:
  - [x] Setup .NET 10.
  - [x] Run `dotnet format --verify-no-changes` (Linting check).
  - [x] Run `dotnet build`.
  - [x] Run `dotnet test` with coverage collection (e.g., using `coverlet.collector`).
  - [x] Add a step to enforce the >70% line coverage requirement (using `irongut/CodeCoverageSummary`).

## Checkpoint & Review
- [x] Run `docker-compose up -d`.
- [x] Run `dotnet build`.
- [x] Run `dotnet test`.
- [x] Manually run `dotnet format` to ensure everything is compliant.
- [x] Ensure the API runs, Swagger loads, and no errors occur.
