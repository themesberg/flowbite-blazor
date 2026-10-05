# Memory Aid Index

> Lessons learned and gotchas discovered through development. Claude MUST read this before editing source files.

## Topic Files

| Topic | File | Description |
|-------|------|-------------|
| Assets | [assets.md](./memory_aid/assets.md) | Self-host DemoApp images in `wwwroot/images`, never hotlink |
| Cleanup | [cleanup.md](./memory_aid/cleanup.md) | Temp files and artifacts to clean up |
| TailwindMerge | [tailwindmerge.md](./memory_aid/tailwindmerge.md) | TailwindMerge.NET usage patterns |

## Quick Reminders

- **Always delete `tmpclaude-*-cwd` files** - These temp files accumulate and clutter the repo
- **Use `MergeClasses()` for class conflicts** - TailwindMerge.NET handles px-4 vs px-6 intelligently
- **Update CHANGELOG.md before committing features** - Don't wait until after merge
- **Self-host DemoApp images in `wwwroot/images`** - Never hotlink other sites (flowbite-react.com `/_next/image` URLs broke the landing page)
