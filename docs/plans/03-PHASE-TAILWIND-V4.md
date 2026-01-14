# Phase 3: Tailwind v4 Migration

**Status:** COMPLETED (All Tasks Complete)
**Prerequisites:** Phase 2 complete (Slot System, Floating UI)
**Priority:** P1 (High)
**Effort:** M (12-18 hours)

---

## Design Philosophy Note

This phase implements **limited customization** per Flowbite's design philosophy. We are NOT creating a full design token system. Customization is restricted to:

- Primary brand color (via `@theme`)
- Layout/structure adjustments (via slot classes - see [Phase 2](./02-PHASE-SLOTS-POSITIONING.md))
- Dark mode toggle (via Flowbite plugin)

> **Note:** The slot system for layout customization was implemented in Phase 2. This phase focuses exclusively on Tailwind v4 migration, primary color theming, and dark mode integration.

---

## Objectives

1. Migrate from Tailwind CSS v3 to v4
2. Implement `@theme` directive for primary color customization
3. Ensure dark mode works correctly with Tailwind v4 and custom primary colors

---

## Task 3.1: Tailwind v4 Migration

**Priority:** P1 (High) | **Effort:** Medium (8-12 hours)

### Objective

Upgrade from Tailwind CSS v3 to v4, updating configuration syntax and build process.

### Acceptance Criteria

- [x] `tailwindcss` package updated to v4.x (v4.1.18 standalone CLI)
- [x] `tailwind.config.js` converted to CSS-based configuration (@source, @theme directives)
- [x] `@import "tailwindcss"` replaces old `@tailwind` directives
- [x] `flowbite/plugin-v4` integrated (via @plugin "flowbite/plugin")
- [x] All existing components render correctly after migration
- [x] Build process updated for new CSS-first approach

### Implementation Notes

```css
/* Before (Tailwind v3 - tailwind.config.js) */
module.exports = {
  content: ['./**/*.razor'],
  theme: {
    extend: {
      colors: {
        primary: { /* ... */ }
      }
    }
  },
  plugins: [require('flowbite/plugin')]
}

/* After (Tailwind v4 - app.css) */
@import "tailwindcss";
@plugin "flowbite/plugin";

@theme {
  --color-primary-50: #eff6ff;
  --color-primary-100: #dbeafe;
  --color-primary-200: #bfdbfe;
  --color-primary-300: #93c5fd;
  --color-primary-400: #60a5fa;
  --color-primary-500: #3b82f6;
  --color-primary-600: #2563eb;
  --color-primary-700: #1d4ed8;
  --color-primary-800: #1e40af;
  --color-primary-900: #1e3a8a;
  --color-primary-950: #172554;
}
```

### Breaking Changes

| v3 Syntax | v4 Syntax | Notes |
|-----------|-----------|-------|
| `@tailwind base` | `@import "tailwindcss"` | Single import |
| `tailwind.config.js` | `@theme {}` in CSS | CSS-first config |
| `require('flowbite/plugin')` | `@plugin "flowbite/plugin"` | CSS directive |
| `theme.extend.colors` | `--color-*` in `@theme` | CSS variables |

---

## Task 3.2: Primary Color Customization

**Priority:** P1 (High) | **Effort:** Low (2-4 hours)

### Objective

Enable consumers to customize the primary brand color through Tailwind v4's `@theme` directive while preserving all other Flowbite colors.

### Acceptance Criteria

- [x] Default primary color defined in library CSS (flowbite.css @theme block)
- [x] Consumers can override via their own `@theme` block
- [x] Primary color scale (50-950) defined in library (consumers provide full scale)
- [x] All `primary-*` utility classes work correctly
- [x] Dark mode variants work with custom primary

### What Consumers CAN Customize

| Token | Example | Purpose |
|-------|---------|---------|
| `--color-primary-*` | `--color-primary-500: #8b5cf6` | Brand color |
| Layout classes | `Slots.Base = "rounded-xl"` | Structural changes |
| Spacing | `Class = "p-8"` | Component padding |

### What Consumers CANNOT Customize (By Design)

| Token | Reason |
|-------|--------|
| `--color-secondary-*` | Part of Flowbite design system |
| `--color-success-*` | Part of Flowbite design system |
| `--color-warning-*` | Part of Flowbite design system |
| `--color-danger-*` | Part of Flowbite design system |
| `--color-gray-*` | Part of Flowbite design system |

### Implementation Notes

```css
/* Library default (Flowbite.css) */
@theme {
  /* Primary - CAN be overridden by consumers */
  --color-primary-50: #eff6ff;
  --color-primary-500: #3b82f6;
  --color-primary-600: #2563eb;
  /* ... full scale */
}

/* Consumer override (their app.css) */
@theme {
  /* Purple brand instead of blue */
  --color-primary-50: #faf5ff;
  --color-primary-500: #8b5cf6;
  --color-primary-600: #7c3aed;
  /* ... */
}
```

---

## Task 3.3: Dark Mode Integration

**Priority:** P1 (High) | **Effort:** Low (2-3 hours)

### Objective

Ensure dark mode works correctly with Tailwind v4 and custom primary colors.

### Acceptance Criteria

- [x] Dark mode toggle continues to work
- [x] Custom primary colors render correctly in dark mode
- [x] `dark:` prefix utilities work as expected
- [x] Flowbite plugin dark mode colors preserved
- [x] No flash of wrong theme on page load

### Implementation Notes

**COMPLETED:** Dark mode now works correctly in Tailwind v4 using `@config` directive.

The key insight: `@custom-variant dark (&:where(.dark, .dark *))` generates selectors with zero specificity due to `:where()`, causing dark mode classes to be overridden. Using `@config "../tailwind.config.js"` with `darkMode: 'class'` generates proper `:is(.dark *)` selectors.

```css
/* flowbite.css - Working dark mode configuration */
@import "tailwindcss";

/*
 * DARK MODE CONFIGURATION
 * Uses @config to import v3 tailwind.config.js with darkMode: 'class'
 * This generates :is(.dark *) selectors with proper specificity
 */
@config "../tailwind.config.js";

@plugin "flowbite/plugin";
```

---

## Definition of Done

Phase 3 is complete when:

1. All components render correctly on Tailwind v4
2. Primary color customization works via `@theme`
3. Dark mode functions correctly
4. No regression in existing functionality
5. PR reviewed and merged

---

## Risks & Mitigations

| Risk | Likelihood | Impact | Mitigation |
|------|------------|--------|------------|
| Flowbite plugin v4 compatibility | Medium | High | Check flowbite/plugin releases; may need shim |
| Breaking changes in v4 syntax | High | Medium | Test all components; document migration steps |
| Consumer upgrade difficulty | Medium | Medium | Provide clear migration guide |
| Dark mode behavior changes | Low | Medium | Test both class-based and media-query modes |

---

## Migration Checklist for Consumers

When Flowbite Blazor upgrades to Tailwind v4, consumers must:

1. [ ] Update `tailwindcss` to v4.x in their project
2. [ ] Convert `tailwind.config.js` to CSS `@theme` block
3. [ ] Replace `@tailwind base/components/utilities` with `@import "tailwindcss"`
4. [ ] Update any custom color definitions to use `--color-*` syntax
5. [ ] Test dark mode functionality

---

## Next Phase

Upon completion, proceed to [Phase 4: Animation System](./04-PHASE-ANIMATION.md).
