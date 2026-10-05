// Verifies every visible <img> loads (naturalWidth > 0) on the given pages,
// in light and dark mode, and saves a screenshot of each run.
//
// Usage (from the repo root, DemoApp running via `python build.py start`):
//   playwright-cli open http://localhost:5290/
//   playwright-cli resize 375 812
//   playwright-cli --raw run-code --filename=scripts/playwright/verify-images.js
//   playwright-cli close
//
// Edit the constants below, then attach screenshots with `gh pr create/comment --attach`.
// See docs/memory_aid/playwright-cli.md for the gotchas this script works around.
async page => {
  const BASE = 'http://localhost:5290';
  const PAGES = ['/', '/docs/components/card'];
  const SLUG = 'verify-images'; // use the branch name with "/" replaced by "-"
  const OUT = `.playwright-cli/screenshots/${SLUG}`;
  const width = page.viewportSize()?.width ?? 0;

  const results = {};
  for (const path of PAGES) {
    for (const scheme of ['light', 'dark']) {
      await page.goto(BASE + path);
      await page.waitForTimeout(4000); // let WASM boot, or it overrides the theme toggle below
      await page.evaluate(s => {
        localStorage.setItem('color-theme', s);
        document.documentElement.classList.toggle('dark', s === 'dark');
        document.querySelectorAll('img').forEach(i => (i.loading = 'eager')); // lazy images never load while offscreen
      }, scheme);
      await page.waitForTimeout(3000);

      results[`${path} ${scheme}`] = await page.evaluate(() => {
        const imgs = [...document.images];
        // light/dark variants are swapped by the parent's display, so only count rendered ones
        const visible = imgs.filter(i => i.getBoundingClientRect().width > 0 && getComputedStyle(i).visibility !== 'hidden');
        return {
          total: imgs.length,
          visible: visible.length,
          broken: visible.filter(i => !(i.naturalWidth > 0)).map(i => i.currentSrc || i.src),
          externalHosts: [...new Set(imgs.map(i => new URL(i.currentSrc || i.src).host))].filter(h => h !== location.host),
        };
      });

      const name = path === '/' ? 'home' : path.split('/').filter(Boolean).pop();
      await page.screenshot({ path: `${OUT}/${name}-${width}-${scheme}.png` });
    }
  }
  return results;
}
