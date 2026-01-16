# Memory Aid Index

> Lessons learned and gotchas discovered through development. Claude MUST read this before editing source files.

## Topic Files

| Topic | File | Description |
|-------|------|-------------|
| Cleanup | [cleanup.md](./memory_aid/cleanup.md) | Temp files and artifacts to clean up |
| TailwindMerge | [tailwindmerge.md](./memory_aid/tailwindmerge.md) | TailwindMerge.NET usage patterns |

## Quick Reminders

- **Always delete `tmpclaude-*-cwd` files** - These temp files accumulate and clutter the repo
- **Use `MergeClasses()` for class conflicts** - TailwindMerge.NET handles px-4 vs px-6 intelligently
- **Update CHANGELOG.md before committing features** - Don't wait until after merge
