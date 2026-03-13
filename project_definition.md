# Sensorialab: Project Definition & Architecture Draft
**Date:** March 10, 2026
**Tech Stack:** ASP.NET Core Minimal APIs (.NET 10), Entity Framework Core, PostgreSQL, Blazor WebAssembly, MudBlazor (UI Components), Hangfire (Background Tasks), QuestPDF (Reporting), OpenTelemetry (Observability), OpenAPI/Swagger (API Testing)

## 1. Intent & Vision
Sensorialab is a modern, two-sided B2B SaaS platform designed specifically for the global coffee industry. Its primary goal is to provide a comprehensive, digital workflow for **Evolved Q Graders** to evaluate coffee samples. While heavily optimized for the new **Specialty Coffee Association (SCA) Coffee Value Assessment (CVA)** methodology, the platform also provides full support for the **Legacy 2004 SCA Cupping Protocol** to accommodate the ongoing industry demand during this transitional period.

Simultaneously, Sensorialab serves as a marketplace and integration hub where **Customers** (coffee roasters, importers, farmers) can discover certified Q Graders, request evaluations under their preferred protocol, and receive structured, high-resolution sensory data seamlessly into their own internal systems via APIs and Webhooks.

## 2. Core Scope
*   **Target Audience (Primary):** Certified Evolved Q Graders (Arabica & Robusta) operating as independent consultants or within labs.
*   **Target Audience (Secondary):** Coffee Roasters, Importers, and Producers who need objective evaluations of their green coffee inventory.
*   **Platform Features:**
    *   **Mobile-First Responsive UI:** The application's interface must be fully responsive, prioritizing mobile usability for Q Graders working in field or lab environments.
    *   Dual Digital Cupping Workflows supporting both the modern CVA (4 Pillars) and the Legacy 2004 100-point scoring system.
    *   Public Directory of verified Q Graders (with filters for supported protocols and pricing).
    *   B2B Request & Logistics Tracking (Sample shipments, weights, and courier tracking).
    *   Automated PDF Report Generation and Interactive Web Reports.
    *   Enterprise API & Webhook Engine for automated third-party integrations (e.g., connecting a Roaster's ERP system directly to a Q Grader's queue).
    *   Single Sign-On (SSO) Support enabling users to log in securely via Google or Microsoft accounts using ASP.NET Core Identity.
*   **Internationalization (i18n):** The platform must be multi-language from Day 1, launching with **Portuguese** and **English**, with an architecture that easily supports Spanish, Japanese, and other languages in the future.

## 3. Defined Use Cases & Additional Requirements

### Q Grader (Evaluator) Workflows
1.  **Profile Creation & Verification:** Q Graders can create a public profile including their bio, region of operation, supported protocols (CVA and/or Legacy 2004), and pricing. They can register two distinct certifications: their **Legacy CQI Certification Number** (for the 2004 protocol) and their new **SCA Evolved Q Grader Certification Number** (for the CVA protocol), each with its own independent **Expiry Date**. The system will track and alert Q Graders when either of their certifications is nearing expiration. 
2.  **Sample Registration:** Q Graders (or Customers via API) can register a new green coffee sample. This captures essential traceability data (Origin, Farm, Varietal), a **Reference Code** (for the customer's internal system mapping), and physical logistics (Sample Weight). During registration, the user must explicitly select the requested **Evaluation Protocol** (CVA or Legacy 2004). If the Q Grader registers the sample manually on behalf of an offline client, they can input external contact details (Client Name, Emails, WhatsApp numbers) to automatically route the final report.
3.  **Roast Profile Management:** Q Graders can pre-configure and save specific "Sample Roast" profiles (Agtron score, Roast Time) to associate with evaluations.
4.  **Evaluation Execution (Dual Protocol):** 
    *   **CVA Mode:** Systematic data entry across the four CVA pillars (Physical, Descriptive, Affective, Extrinsic).
    *   **Legacy 2004 Mode:** Traditional 100-point scoring entry (Fragrance/Aroma, Flavor, Aftertaste, Acidity, Body, Balance, Uniformity, Clean Cup, Sweetness, Defects, Overall).
5.  **Report Generation & Sharing:** View, print, and share the final report via password-protected Web Link, PDF, Email, or WhatsApp. Users can toggle data privacy settings (Confidential vs. Public).

### Customer (Roaster/Importer) Workflows
6.  **Account Creation & Team Management:** Customers can create an account containing multiple individual users, utilizing SSO. Companies can configure **Default Delivery Preferences** (lists of emails and WhatsApp numbers) specifying where completed reports should be automatically sent.
7.  **Marketplace Discovery & Requests:** Search the global directory of Q Graders (filtering by country, protocol support, price) and submit a formal request. The request automatically inherits the company's default delivery preferences, but the customer can add or override them for specific samples.
8.  **Logistics & Status Tracking:** Customers can input Courier Tracking Numbers and monitor the evaluation request through a defined state machine: `Pending` -> `Accepted` -> `Sample Received` -> `Evaluating` -> `Completed`.
9.  **Notifications:** Real-time notifications via In-App alerts and Email when status changes occur.

### B2B API & Integration Workflows (The Enterprise Tier)
10. **Trusted Connections:** Establish a `QGraderConnectionId` between a Customer and a Q Grader.
11. **API Key Management:** Customers can manage API keys with custom expirations. Keys are hashed and shown only once.
12. **Automated Sample Submission (API):** Programmatically submit coffee evaluation requests, including the `ReferenceCode` and the requested `Protocol`, directly from internal ERPs.
13. **Webhook Callbacks:** Automatic `POST` requests delivering the full JSON payload of either the CVA or Legacy results back to the Customer's `CallbackUrl` upon completion.

## 4. Key Architectural Considerations for Next Steps
*   **Protocol Abstraction:** The `Evaluation` entity must flexibly accommodate either the complex CVA 4-pillar data or the flat 10-attribute Legacy data without enforcing null-conflicts in the database schema.
*   **Domain-Driven Design (DDD):** Ensuring bad data cannot enter the system by strictly encapsulating the distinct validation rules of the CVA vs. the 2004 protocol.
*   **Database Schema (PostgreSQL):** Using JSONB or Table-Per-Hierarchy (TPH) in EF Core to handle the differing shapes of CVA and Legacy evaluation data linked to the core `Evaluation` record.
*   **Authentication & Multi-tenancy:** OAuth2/OpenID Connect for SSO, scoping data strictly to the `CustomerCompany` tenant.
*   **Background Processing:** Hangfire for reliable webhook delivery, email notifications (via standard SMTP), and WhatsApp notifications (via official Meta WhatsApp Cloud API).
*   **API Design (Minimal APIs):** Utilize .NET 10 Minimal APIs instead of controller-based architecture to reduce overhead, simplify endpoints, and improve performance. Implement OpenAPI and Swagger to document and test the API.
*   **Observability:** Implement OpenTelemetry (OTEL) for distributed tracing, metrics, and logging to provide deep visibility into the system's performance and behavior.
*   **Open Source First:** Strongly prefer open-source libraries and tools over commercial ones. Avoid dependencies that require paid commercial licenses to keep operational costs low and avoid vendor lock-in.

## 5. Development & CI/CD Practices
*   **Unit Testing & Coverage:** Every new feature, domain model, or logic implementation must be accompanied by unit tests. The project must maintain a strict minimum of **70% test coverage**. 
*   **Test-Driven Culture:** If a test fails, fixing it is the immediate priority before any new code is written or merged.
*   **Iteration Checklists:** At the end of every development iteration (or before pushing code), developers must run all test suites and execute code linting/formatting checks (e.g., `dotnet format`).
*   **Pull Request Workflow:** All changes must be submitted via Pull Requests (PRs). A PR should only be created after tests pass locally and linting is verified.
*   **Continuous Integration (CI):** The CI pipeline (GitHub Actions) will enforce the 70% coverage rule, run all tests, and verify code formatting on every PR.
*   **Semantic Commits:** Use conventional commits (e.g., `feat:`, `fix:`, `chore:`) to maintain a readable history and automate changelogs.
*   **Architecture Enforcement:** Utilize tools like `NetArchTest` to write tests that enforce Domain-Driven Design (DDD) boundaries, ensuring that domain models do not depend on infrastructure or external frameworks.
*   **Security Scanning:** Integrate tools like Dependabot or Snyk in the CI pipeline to automatically scan for vulnerable dependencies.