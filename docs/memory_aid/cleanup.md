# Cleanup: Temp Files and Artifacts

## tmpclaude-*-cwd Files

**Pattern:** `tmpclaude-*-cwd` (e.g., `tmpclaude-878b-cwd`)

**Location:** Repository root

**Cause:** Claude Code creates these temporary files during sessions. They accumulate over time.

**Action:** Delete at the start of each session or when noticed.

```bash
# Delete all tmpclaude temp files
rm -f tmpclaude-*-cwd
```

**Prevention:** These files should be in `.gitignore` but still clutter the working directory.

---

## Other Temp Files to Watch

| Pattern | Location | Action |
|---------|----------|--------|
| `.demoapp.pid` | Root | Auto-managed by build.py, don't commit |
| `demoapp.log` | Root | Log file, don't commit |
| `*.min.css` | wwwroot dirs | DO commit - these are build outputs |

---

## Cleanup Checklist

Before committing or at session start:

- [ ] `rm -f tmpclaude-*-cwd`
- [ ] Check `git status` for unexpected untracked files
- [ ] Ensure `.demoapp.pid` is not staged
