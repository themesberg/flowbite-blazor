# Workflow: Dedicated AI Chat Page for DemoApp

## 1. Goals & Success Criteria
- Deliver a dedicated AI chat page in the DemoApp that uses Flowbite Blazor chat primitives end to end.
- Allow users to pick one of **OpenAI**, **Anthropic**, **Google**, or **OpenRouter** as the active provider and supply a protected API key in the UI.
- Drive LlmTornado calls with the user-selected provider/key and surface streamed assistant responses, reasoning drawers, and citation links.
- Ship supporting documentation, navigation updates, and Playwright verification evidence.

### Definition of Done
1. New AI chat page renders in DemoApp with conversation stream, prompt toolbar, provider dropdown, and password-style API key input.
2. Provider/key selection is validated client-side and used to configure LlmTornado before each submission.
3. `IAiChatService` (or equivalent) wraps LlmTornado for DemoApp, handling provider mapping, errors, and streaming fallbacks.
4. Documentation and sidebar navigation reference the new page, and a Playwright MCP run captures the working UI.

---

## 2. References & Inputs
| Topic | Source |
| --- | --- |
| Vercel AI template inspiration | `/home/tschavey/themesberg/flowbite-blazor/docs/feature/chat/vercel-ai-template-chat-example.png` |
| Tutorial guidance | `/home/tschavey/themesberg/flowbite-blazor/docs/feature/chat/ai-elements-nextjs-example-chatbot.mdx` |
| Existing Flowbite chat demo | `/home/tschavey/themesberg/flowbite-blazor/src/DemoApp/Pages/Docs/components/ChatbotPage.razor` |
| Flowbite chat components | `/home/tschavey/themesberg/flowbite-blazor/src/Flowbite/Components/Chat` |
| LlmTornado Getting Started | `/home/tschavey/github/LlmTornado/src/LlmTornado.Docs/website/docs/getting-started.md` |
| LlmTornado Chat Basics | `/home/tschavey/github/LlmTornado/src/LlmTornado.Docs/website/docs/1. LlmTornado/Chat/1. basics.md` |
| LlmTornado Provider Models | `/home/tschavey/github/LlmTornado/src/LlmTornado.Docs/website/docs/1. LlmTornado/Chat/5. models.md` |

---

## 3. Architecture Decisions

### 3.1 Page Placement & Structure
- Create `ChatAiPage.razor` under `/home/tschavey/themesberg/flowbite-blazor/src/DemoApp/Pages/Docs/ai/` (match existing docs routing pattern).
- Follow Flowbite’s component split: `.razor` for markup, `.razor.cs` partial for logic/state.
- Mirror the layout of `ChatbotPage.razor` but replace mock data with live state driven by LlmTornado.

### 3.2 Provider & Credential Handling
- Provider dropdown limited to four options: **OpenAI**, **Anthropic**, **Google**, **OpenRouter**.
- Store provider selection as enum or strongly typed record with default model mapping:
  - OpenAI → `ChatModel.OpenAi.Gpt4.O`
  - Anthropic → `ChatModel.Anthropic.Claude35Sonnet`
  - Google → `ChatModel.Google.Gemini.Gemini15Pro`
  - OpenRouter → default to `ChatModel.OpenRouter.All.Llama38bInstruct` (or configurable)
- API key field should use a password input (masked) with optional reveal toggle. Never log the key.

### 3.3 Service Layer
- Implement `IAiChatService` in `/home/tschavey/themesberg/flowbite-blazor/src/DemoApp/Services/`:
  - Accept provider enum, api key, conversation history, and user flags (web search, attachments metadata).
  - Instantiate/configure `TornadoApi` with `ProviderAuthentication` per submission or cached per session.
  - Call `CreateConversation` / `CreateChatCompletion` to fetch results.
  - Normalize assistant output into DTO containing text, reasoning (if available), sources, and attachment echoes.
- Add dependency injection registration in `/home/tschavey/themesberg/flowbite-blazor/src/DemoApp/Program.cs`.

### 3.4 Component Behavior
- Maintain conversation state in the page component: list of messages (user + assistant), attachments, streaming status.
- On submit:
  1. Validate provider selection and API key presence.
  2. Append user message to local state, clear input, set status to submitting.
  3. Invoke `IAiChatService`.
  4. Stream or append assistant response, updating reasoning and sources sections.
  5. Handle errors with Flowbite toast/banner and reset state gracefully.
- Provide toggles for microphone (stub) and web search; wire web search flag to service for future use.

### 3.5 Security & UX Notes
- Mask API key in UI and avoid persisting it server-side. For demos, consider storing in-memory for the session only.
- Add copy-friendly info box explaining that keys stay in the browser session.
- Ensure dark mode responsive styles.

### 3.6 Documentation & Navigation
- Add entry under `/home/tschavey/themesberg/flowbite-blazor/src/DemoApp/Layout/DocLayoutSidebarData.cs` grouping (e.g., “AI Chat” under AI or Components).
- Document workflow in LLMS sections under `/home/tschavey/themesberg/flowbite-blazor/src/DemoApp/wwwroot/llms-docs/sections/`.
- Update AI docs generator if needed (`/home/tschavey/themesberg/flowbite-blazor/src/DemoApp/Build-LlmsContext.ps1`).

---

## 4. Execution Phases & Tasks

### Phase A – Foundation
1. Scaffold new page and partial class.
2. Define message DTOs (role, text, reasoning, sources, attachments).
3. Introduce provider enum/config mapping.
4. **Verification:** Perform a Playwright MCP capture for Phase A setup and store screenshots/logs in `/home/tschavey/themesberg/flowbite-blazor/docs/feature/chat/tmp/verification/phase-a/`.

### Phase B – UI Assembly
1. Build conversation stream with `Conversation` + `ChatMessage` + `ChatResponse`.
2. Add reasoning (`<Reasoning>`), sources (`<Sources>` + `ChatSource`), and action buttons (`ChatActions`).
3. Configure `PromptInput` with attachments, web search toggle, microphone stub.
4. Add provider dropdown (`PromptInputModelSelect`) limited to the four providers.
5. Add masked API key input (Flowbite `TextInput` or custom slot), plus validation feedback.
6. **Verification:** Capture the assembled UI via Playwright MCP and archive outputs in `/home/tschavey/themesberg/flowbite-blazor/docs/feature/chat/tmp/verification/phase-b/`.

### Phase C – LlmTornado Integration
1. Add LlmTornado NuGet package references to DemoApp if not already present.
2. Implement `IAiChatService` + `AiChatService`.
3. Wire DI and inject into page.
4. Implement submit handler: translate conversation history to `ChatRequest`, include provider-specific model, pass API key.
5. Handle streaming (preferred) or fallback to full response.
6. **Verification:** Run Playwright MCP against the wired backend flow and place logs/screenshots in `/home/tschavey/themesberg/flowbite-blazor/docs/feature/chat/tmp/verification/phase-c/`.

### Phase D – Documentation & System Updates
1. Finish this workflow doc (`/home/tschavey/themesberg/flowbite-blazor/.clinerules/workflows/demoapp-ai-chat-page.md`).
2. Create AI docs section snippet describing usage and key entry requirements.
3. Update sidebar navigation.
4. Run `Build-LlmsContext.ps1` if doc context requires regeneration.
5. **Verification:** Record Playwright MCP evidence of documentation/navigation updates under `/home/tschavey/themesberg/flowbite-blazor/docs/feature/chat/tmp/verification/phase-d/`.

### Phase E – Testing & Verification
1. `dotnet build` (Debug/Release) to ensure solution compiles.
2. Manual smoke test via `dotnet watch --project src/DemoApp/DemoApp.csproj run`.
3. Execute Playwright MCP flow:
   - Launch browser to DemoApp AI chat page.
   - Enter placeholder provider and masked key.
   - Capture screenshot demonstrating dropdown and key input.
   - Document result (screenshot + log snippet).
4. Record findings in project notes.
5. **Verification:** Consolidate final Playwright MCP artifacts for Phase E into `/home/tschavey/themesberg/flowbite-blazor/docs/feature/chat/tmp/verification/phase-e/`.

---

## 5. Risks & Mitigations
| Risk | Impact | Mitigation |
| --- | --- | --- |
| API key leakage via logs/UI | High | Mask inputs, avoid logging, sanitize telemetry. |
| Provider model drift | Medium | Centralize provider→model mapping; document update path; expose fallback config. |
| Streaming unsupported for selected provider/model | Medium | Detect and fallback to non-streamed response; surface status indicator. |
| Invalid keys or rate limits | Medium | Catch LlmTornado exceptions, provide user-friendly alerts and retry hints. |
| Demo without real keys feels broken | Low | Supply explanatory message and allow dry-run mode with canned responses if key missing. |

---

## 6. Deliverables Checklist
- [ ] `/home/tschavey/themesberg/flowbite-blazor/src/DemoApp/Pages/Docs/ai/ChatAiPage.razor` & `/home/tschavey/themesberg/flowbite-blazor/src/DemoApp/Pages/Docs/ai/ChatAiPage.razor.cs` with Flowbite chat UI wired to state.
- [ ] Provider dropdown limited to OpenAI, Anthropic, Google, OpenRouter.
- [ ] Masked API key input with validation and optional reveal.
- [ ] `/home/tschavey/themesberg/flowbite-blazor/src/DemoApp/Services/IAiChatService` leveraging LlmTornado with provider-aware configuration.
- [ ] Navigation update and LLMS documentation snippet.
- [ ] Playwright MCP verification artifact (screenshot/log).
- [ ] `/home/tschavey/themesberg/flowbite-blazor/.clinerules/workflows/demoapp-ai-chat-page.md` (this document) committed with implementation notes.

---

## 7. Notes & Follow-ups
- Consider adding local storage persistence for provider choice, but require explicit opt-in for API key storage.
- Evaluate hooking Flowbite `PromptInput` streaming events to mimic Vercel AI template real-time UX.
- When implementing, revisit Tailwind classes to ensure responsive layout and dark mode parity with screenshot reference.
- Future enhancement: provide multi-provider history and benchmarking UI leveraging LlmTornado’s multi-provider support.
