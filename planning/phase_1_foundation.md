# Phase 1: Project Foundation & Architecture Setup

## Goals
Establish the core .NET 10 Solution, enforce Domain-Driven Design (DDD) principles, configure the database infrastructure, set up continuous integration, and ensure observability.

## Deliverables
1. **Solution Initialization:**
   - Create a clean architecture structure: `Sensorialab.Domain`, `Sensorialab.Shared` (for DTOs/Contracts), `Sensorialab.Application`, `Sensorialab.Infrastructure`, `Sensorialab.Api`, `Sensorialab.Tests`.
2. **API Setup:**
   - Initialize .NET 10 Minimal APIs in the `Sensorialab.Api` project.
   - Configure **OpenAPI and Swagger** for API documentation and automated UI testing.
3. **Database Infrastructure:**
   - Configure PostgreSQL integration with Entity Framework Core (EF Core) in the `Sensorialab.Infrastructure` project.
   - Set up the initial `AppDbContext` (and an `IAppDbContext` interface for testability) to be used directly by services, eschewing the Repository pattern for simplicity. Implement EF Core Migrations.
4. **Observability Setup:**
   - Integrate OpenTelemetry (OTEL) for logging, distributed tracing, and metrics gathering.
   - Export telemetry to an open-source backend (e.g., Jaeger, Prometheus, or Seq).
5. **CI/CD & Development Standards:**
   - Implement the Architecture Tests using `NetArchTest` to ensure Domain entities do not depend on Infrastructure.
   - Setup CI pipeline (GitHub Actions) to enforce linting (`dotnet format`), run all unit tests, and validate the 70% minimum code coverage.

## Milestones
- Development environment is fully containerized (e.g., docker-compose with PostgreSQL and OTEL backend).
- The `dotnet run` command successfully launches the Swagger UI.
- All initial architecture tests pass in the CI pipeline.
