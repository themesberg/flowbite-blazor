# Assets: DemoApp Images

## Self-host images, never hotlink

**Rule:** Host every DemoApp image under `src/DemoApp/wwwroot/images/` and reference it with a root-relative path (e.g. `/images/components/card.svg`). Never hotlink images from other sites.

**Cause:** The landing page and `CardPage.razor` hotlinked `https://flowbite-react.com/_next/image?url=...&w=..&q=..` (Next.js optimizer URLs). Those stopped loading, and relative `/_next/image?...` srcset entries never worked on our domain, so the images broke.

**Action:**
- Copy the file into `wwwroot/images/` (flowbite-react's `apps/web/public/images/` is MIT licensed and mirrors the same layout).
- Never reference `/_next/image` URLs; use the original file path and drop `srcset` entries that point at the same file.
- GitHub avatars (`https://avatars.githubusercontent.com/u/<id>?v=4&s=<size>`) are the one exception: they are a stable, public avatar API.
- After changing image references, check each visible `<img>` has `naturalWidth > 0` in light and dark mode.
