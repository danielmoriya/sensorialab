# Phase 3: Core Domain Models & Logistics

## Goals
Translate the coffee industry's physical logistics and certification rules into strictly validated Domain Models, leveraging EF Core's advanced mapping capabilities.

## Deliverables
1. **Q Grader Profiling:**
   - Map the `QGraderProfile` entity.
   - Implement dual certification tracking (Legacy CQI Certification vs. Evolved SCA CVA Certification) with independent expiry dates and domain events for near-expiration alerts.
2. **Logistics & Request Mapping:**
   - Map `EvaluationRequest` and include properties for physical logistics (Sample Weight, Courier Tracking).
   - Map B2B routing data (Customer Reference Code, External Client Name, Delivery Emails/WhatsApp).
   - Implement a Domain-driven State Machine for `EvaluationRequest` (Pending -> Accepted -> Sample Received -> Evaluating -> Completed).
3. **Protocol Abstraction (CVA & Legacy):**
   - Create the base `Evaluation` entity.
   - Map the complex, multi-tiered CVA (4-pillars) data structure and the flat 10-attribute Legacy 2004 data structure.
   - Configure EF Core mapping: Decide between Table-Per-Hierarchy (TPH) or using a PostgreSQL JSONB column for the varying evaluation metrics payload.

## Milestones
- Domain unit tests successfully validate the business rules (e.g., preventing a legacy evaluation from being saved without all 10 attributes).
- Entity Framework successfully creates and queries the dual-protocol tables without schema conflicts.
