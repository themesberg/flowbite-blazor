/**
 * DOM utilities for floating elements.
 */

/**
 * Moves an element to a target container (portal pattern).
 * @param {HTMLElement} element - The element to move
 * @param {HTMLElement} target - The target container (defaults to document.body)
 */
export function portal(element, target = document.body) {
    if (element && element.parentElement !== target) {
        target.appendChild(element);
    }
}

/**
 * Waits for an element to appear in the DOM.
 * @param {string} selector - CSS selector for the element
 * @param {number} timeout - Maximum wait time in milliseconds
 * @returns {Promise<HTMLElement>} Resolves with the element when found
 */
export function waitForElement(selector, timeout = 5000) {
    return new Promise((resolve, reject) => {
        const el = document.querySelector(selector);
        if (el) return resolve(el);

        const observer = new MutationObserver(() => {
            const el = document.querySelector(selector);
            if (el) {
                observer.disconnect();
                resolve(el);
            }
        });

        observer.observe(document.body, { childList: true, subtree: true });
        setTimeout(() => {
            observer.disconnect();
            reject(new Error(`Element ${selector} not found within ${timeout}ms`));
        }, timeout);
    });
}
