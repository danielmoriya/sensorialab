# Phase 2: Identity, Authentication & Multi-Tenancy

## Goals
Secure the platform by implementing User profiles, SSO integration, Multi-tenancy isolation for B2B customers, and Role-Based Access Control (RBAC).

## Deliverables
1. **Identity & Authentication:**
   - Implement robust authentication using ASP.NET Core Identity with OAuth2/OpenID Connect.
   - Configure Single Sign-On (SSO) integration with Google and Microsoft accounts.
   - Setup JWT Bearer token validation for the Minimal APIs.
2. **Multi-Tenancy & Authorization:**
   - Implement `CustomerCompany` and `User` domain models.
   - Configure EF Core Global Query Filters to enforce multi-tenancy (scoping data to the active user's `CustomerCompanyId`).
   - Establish RBAC definitions: Admin, QGrader, Customer, API-Only User.
3. **B2B API Management:**
   - Implement domain models for API Key management (Generation, Hashing, Expiration).
   - Create custom middleware or endpoint filters in .NET 10 to validate incoming API Keys for programmatic access.

## Milestones
- A user can authenticate via Google/Microsoft and receive a valid JWT.
- API endpoints correctly authorize requests based on the JWT or B2B API Key.
- Data queries automatically filter out records belonging to other tenants.
