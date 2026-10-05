# Build: Local Environment Gotchas

## Use `build.py`, not `dotnet build`

**Cause:** On macOS/Linux the csproj Tailwind target runs the standalone CLI with `--postcss`, which fails (`lightningcss ... ERR_DLOPEN_FAILED`). `build.py` runs Tailwind itself and then builds the solution.

**Action:** Run `python build.py build` (or `start`/`test`), not `dotnet build FlowbiteBlazor.sln`.

## `NU1301: local source 'nuget-local' doesn't exist`

**Cause:** DemoApp restores from `nuget-local/`, which is gitignored and absent on a fresh clone.

**Action:** `mkdir -p nuget-local` (or run `python build.py pack`).

## `psutil` blocked by PEP 668

**Cause:** System Python is externally managed, so `pip install psutil` fails.

**Action:** Use a venv outside the repo: `python3 -m venv /tmp/fbvenv && /tmp/fbvenv/bin/pip install psutil`, then `/tmp/fbvenv/bin/python build.py ...`. Run `npm ci` once at the repo root as well.

## Regenerated `*.min.css` differ from the committed files

**Cause:** The local Tailwind binary emits different output than what is committed (`flowbite.min.css` shrank by thousands of lines).

**Action:** Commit `*.min.css` only when you added or changed Tailwind classes. Otherwise `git checkout -- src/DemoApp/wwwroot/css/app.min.css src/Flowbite/wwwroot/flowbite.min.css` after building.

## Pushing large changes over HTTPS

**Cause:** Pushing tens of MB (e.g. many images) can fail with `RPC failed; HTTP 400`.

**Action:** `git -c http.postBuffer=524288000 push ...`
