# Phase 6: Public Directory & B2B Integration

## Goals
Launch the marketplace discovery features, finalize B2B programmatic access via Webhooks, and prepare the platform for production.

## Deliverables
1. **Public Q Grader Directory:**
   - Build a searchable, filterable Blazor UI for Customers to discover verified Q Graders.
   - Implement filters by country, supported protocols, and pricing.
2. **B2B Webhooks:**
   - Implement a background webhook dispatch engine using Hangfire.
   - Configure automatic `POST` requests sending the full JSON payload of completed evaluations back to the Customer's registered `CallbackUrl`.
   - Implement webhook retry policies (e.g., exponential backoff) for failed deliveries.
3. **B2B Enterprise API Integration:**
   - Finalize the automated sample submission endpoints for ERP integration.
   - Allow programmatic assignment of the `CustomerReferenceCode` and routing information.
4. **Final Review & Optimization:**
   - Execute end-to-end integration tests.
   - Ensure all telemetry (OTEL) traces correctly correlate across API requests, database queries, and Hangfire background jobs.

## Milestones
- An external script (simulating an ERP) can successfully submit an evaluation request via API Key.
- Upon completion of that evaluation, a webhook payload is successfully dispatched and caught by a mock external listener (e.g., webhook.site).
- The solution meets the strict >70% test coverage requirement across all modules.
