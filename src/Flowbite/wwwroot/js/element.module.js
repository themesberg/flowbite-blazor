/**
 * Flowbite Blazor Element Utilities Module
 * ES module for DOM element operations with lazy loading support.
 */

/**
 * Gets the scroll height of an element for smooth collapse/expand animations.
 * @param {HTMLElement} element - The element to measure.
 * @returns {number} The scroll height in pixels, or 0 if element is null.
 */
export function getScrollHeight(element) {
    return element?.scrollHeight ?? 0;
}

/**
 * Gets the client height of an element.
 * @param {HTMLElement} element - The element to measure.
 * @returns {number} The client height in pixels, or 0 if element is null.
 */
export function getClientHeight(element) {
    return element?.clientHeight ?? 0;
}

/**
 * Gets the bounding client rect of an element.
 * @param {HTMLElement} element - The element to measure.
 * @returns {DOMRect|null} The bounding rectangle or null if element is null.
 */
export function getBoundingClientRect(element) {
    return element?.getBoundingClientRect() ?? null;
}

/**
 * Sets focus on an element.
 * @param {HTMLElement} element - The element to focus.
 * @param {boolean} preventScroll - Whether to prevent scrolling when focusing.
 */
export function setFocus(element, preventScroll = false) {
    element?.focus({ preventScroll });
}

/**
 * Scrolls an element into view.
 * @param {HTMLElement} element - The element to scroll into view.
 * @param {ScrollIntoViewOptions} options - Scroll options.
 */
export function scrollIntoView(element, options = { behavior: 'smooth', block: 'nearest' }) {
    element?.scrollIntoView(options);
}
