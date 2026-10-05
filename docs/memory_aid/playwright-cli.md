# Playwright CLI: UI Verification

The `playwright-cli` skill (Skill tool: `playwright-cli`) is the preferred way to verify DemoApp UI. Order of preference: `playwright-cli` skill, then Playwright MCP, then the built-in browser pane.

## Is it available?

**Check first:** `which playwright-cli`. Don't conclude Playwright is unavailable just because the Playwright MCP tools are missing. The CLI is a separate global install (`npm install -g @playwright/cli@latest`) and the skill lives at `~/.claude/skills/playwright-cli` (user-level, not in this repo).

## Where output goes

Everything stays under `.playwright-cli/` (gitignored). Never write to `/tmp` or the repo root.

- CLI scratch (snapshots, console logs): `.playwright-cli/` (created by the tool in the working directory)
- Screenshots: `.playwright-cli/screenshots/<branch-slug>/<page>-<width>-<scheme>.png`, with `/` in the branch name replaced by `-`. Example: `.playwright-cli/screenshots/fix-landing-page-broken-images/home-375-light.png`

Attach screenshots to a PR with `gh` (2.100+), alt text after `#`:

```bash
gh pr create --attach './.playwright-cli/screenshots/<slug>/home-375-light.png#Home, light, 375px'
gh pr comment <n> --attach ./before.png --attach ./after.png
```

## Reusable image check

`scripts/playwright/verify-images.js` checks every visible `<img>` has `naturalWidth > 0` in light and dark mode and saves screenshots. Run it from the repo root:

```bash
python build.py start
playwright-cli open http://localhost:5290/
playwright-cli resize 375 812
playwright-cli --raw run-code --filename=scripts/playwright/verify-images.js
playwright-cli close
python build.py stop
```

It reports `broken` and `externalHosts` per page/theme. Both should be empty.

## Gotchas

- **Startup race:** `build.py start` can return before the app accepts connections, so the first `open`/`goto` fails with `ERR_CONNECTION_REFUSED`. Retry, or poll with `curl` first.
- **Theme toggle:** the app applies the theme after the WASM boots. Wait about 4 seconds after `goto` before toggling the `dark` class, or the toggle is overridden. The theme persists in localStorage (`color-theme`), so run `playwright-cli localstorage-clear` between runs.
- **Lazy images:** `loading="lazy"` images never load while offscreen. Set `img.loading = 'eager'` and wait before checking `naturalWidth`.
- **Light/dark variants:** both `x.svg` and `x-dark.svg` are in the DOM and the parent's `display` hides one. Count only images with `getBoundingClientRect().width > 0`, and use `locator('visible=true')` when targeting one.
- **Phone viewport:** `playwright-cli resize 375 812`.
- **Multi-step checks:** use `run-code --filename=<script>` with the global `--raw` flag instead of many separate commands.
- **Banner noise:** the "update available" box is printed on every command. Filter with `grep -v -E '^[╔║╚]'` or update the CLI.
- **Cleanup:** `playwright-cli close` and `python build.py stop` when done.
