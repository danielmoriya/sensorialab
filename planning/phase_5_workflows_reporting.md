# Phase 5: Evaluation Workflows & Reporting

## Goals
Build the core interactive tools for Q Graders to score coffee, generate high-quality PDF reports, and automatically dispatch results via background workers.

## Deliverables
1. **Interactive Evaluation UI:**
   - Build the complex data entry forms in Blazor for the modern CVA 4-Pillar methodology.
   - Build the traditional 100-point scoring form for the Legacy 2004 protocol.
2. **PDF Report Generation (QuestPDF):**
   - Design the visual PDF layout using QuestPDF in the backend.
   - Create distinct visual templates tailored to the CVA protocol and the Legacy protocol.
3. **Background Processing (Hangfire):**
   - Integrate Hangfire into the API to handle out-of-band processing.
   - Implement asynchronous tasks to generate the PDF report once an evaluation state transitions to `Completed`.
4. **Automated Delivery:**
   - Implement an Email delivery service using standard SMTP.
   - Implement a WhatsApp delivery service using the official Meta WhatsApp Cloud API.
   - Configure a Hangfire background job to dispatch the evaluation results/PDF links to the configured default delivery lists associated with the `EvaluationRequest`.

## Milestones
- A Q Grader can fully complete a mock evaluation (either CVA or Legacy) via the Blazor UI.
- QuestPDF successfully generates a stylized, complete PDF document containing the sensory data.
- The system reliably enqueues and processes background delivery tasks upon completion.
