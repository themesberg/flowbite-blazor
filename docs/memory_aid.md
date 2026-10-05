# Memory Aid Index

> Lessons learned and gotchas discovered through development. Claude MUST read this before editing source files.

## Topic Files

| Topic | File | Description |
|-------|------|-------------|
| Assets | [assets.md](./memory_aid/assets.md) | Self-host DemoApp images in `wwwroot/images`, never hotlink |
| Build | [build.md](./memory_aid/build.md) | Local build gotchas (`build.py`, `nuget-local`, venv, `*.min.css`) |
| Cleanup | [cleanup.md](./memory_aid/cleanup.md) | Temp files and artifacts to clean up |
| Playwright CLI | [playwright-cli.md](./memory_aid/playwright-cli.md) | `playwright-cli` UI verification, screenshot locations, gotchas |
| TailwindMerge | [tailwindmerge.md](./memory_aid/tailwindmerge.md) | TailwindMerge.NET usage patterns |

## Quick Reminders

- **Always delete `tmpclaude-*-cwd` files** - These temp files accumulate and clutter the repo
- **Use `MergeClasses()` for class conflicts** - TailwindMerge.NET handles px-4 vs px-6 intelligently
- **Update CHANGELOG.md before committing features** - Don't wait until after merge
- **Self-host DemoApp images in `wwwroot/images`** - Never hotlink other sites (flowbite-react.com `/_next/image` URLs broke the landing page)
- **Verify UI with the `playwright-cli` skill** - Check `which playwright-cli` before assuming Playwright is unavailable; keep screenshots under `.playwright-cli/screenshots/`
- **Build through `build.py`** - Direct `dotnet build` fails on macOS (Tailwind `--postcss`)
