# Phase 4: Blazor WebAssembly Frontend Setup

## Goals
Initialize the client-side WebAssembly application, ensuring it meets the mobile-first requirement, supports multiple languages, and communicates securely with the APIs.

## Deliverables
1. **Blazor Application Initialization:**
   - Scaffold the `Sensorialab.Client` Blazor WebAssembly project.
   - Implement a mobile-first, responsive layout using the MudBlazor UI component library.
2. **Internationalization (i18n):**
   - Set up localization resources for `pt-BR` (Portuguese) and `en-US` (English) from day one.
   - Implement a language toggle accessible across the application.
3. **Authentication Integration:**
   - Configure the Blazor Authentication State Provider to handle JWTs and SSO login redirects.
   - Implement authorized views based on the user's role (Customer vs. Q Grader).
4. **API Consumption:**
   - Setup typed `HttpClient` services.
   - Implement API clients to fetch user profile data and active evaluation requests.

## Milestones
- The Blazor application runs locally and successfully authenticates against the Minimal API.
- The UI gracefully adapts to mobile viewports (e.g., simulating a tablet/phone on a cupping table).
- The user can seamlessly toggle between English and Portuguese, updating the UI text dynamically.
