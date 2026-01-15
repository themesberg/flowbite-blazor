/**
 * Flowbite Blazor Focus Management Module
 * ES module for focus trap and restoration in modal/drawer components.
 */

/**
 * Focusable element selector for focus trap.
 */
const FOCUSABLE_SELECTOR =
    'a[href], button:not([disabled]), textarea:not([disabled]), ' +
    'input:not([disabled]), select:not([disabled]), [tabindex]:not([tabindex="-1"])';

/**
 * Map to store focus trap state for each element.
 */
const focusTraps = new Map();

/**
 * The element that had focus before a component was opened.
 */
let previouslyFocused = null;

/**
 * Traps focus within the specified element.
 * @param {string} elementId - The ID of the element to trap focus within.
 * @returns {boolean} True if focus trap was set up successfully.
 */
export function trapFocus(elementId) {
    const element = document.getElementById(elementId);
    if (!element) {
        console.warn(`[Flowbite.FocusManagement] Element not found: ${elementId}`);
        return false;
    }

    // Store the element that had focus before opening
    previouslyFocused = document.activeElement;

    // Find all focusable elements
    const focusableElements = element.querySelectorAll(FOCUSABLE_SELECTOR);
    if (focusableElements.length === 0) {
        return false;
    }

    // Focus the first element
    focusableElements[0].focus();

    // Set up event listener for tab key
    const handleTabKey = (e) => {
        if (e.key !== 'Tab') return;

        const firstElement = focusableElements[0];
        const lastElement = focusableElements[focusableElements.length - 1];

        if (e.shiftKey) {
            if (document.activeElement === firstElement) {
                lastElement.focus();
                e.preventDefault();
            }
        } else {
            if (document.activeElement === lastElement) {
                firstElement.focus();
                e.preventDefault();
            }
        }
    };

    element.addEventListener('keydown', handleTabKey);
    focusTraps.set(elementId, { element, handleTabKey });

    return true;
}

/**
 * Restores focus to the previously focused element.
 * @returns {boolean} True if focus was restored successfully.
 */
export function restoreFocus() {
    if (previouslyFocused && previouslyFocused.focus) {
        previouslyFocused.focus();
        previouslyFocused = null;
        return true;
    }
    return false;
}

/**
 * Cleans up focus trap for the specified element.
 * @param {string} elementId - The ID of the element to clean up.
 * @returns {boolean} True if cleanup was successful.
 */
export function cleanupFocusTrap(elementId) {
    const trap = focusTraps.get(elementId);
    if (trap) {
        trap.element.removeEventListener('keydown', trap.handleTabKey);
        focusTraps.delete(elementId);
        return true;
    }
    return false;
}

/**
 * Disables body scrolling (for modal/drawer components).
 */
export function disableBodyScroll() {
    document.body.style.overflow = 'hidden';
}

/**
 * Enables body scrolling.
 */
export function enableBodyScroll() {
    document.body.style.overflow = '';
}

/**
 * Initializes a drawer component with escape key handling.
 * @param {string} elementId - The ID of the drawer element.
 * @param {object} dotNetReference - The .NET object reference for callbacks.
 * @returns {boolean} True if initialization was successful.
 */
export function initializeDrawer(elementId, dotNetReference) {
    const element = document.getElementById(elementId);
    if (!element) {
        return false;
    }

    const handleEscapeKey = (e) => {
        if (e.key === 'Escape') {
            // Find and click the close button
            const closeButton = element.querySelector('[aria-label="Close"]');
            if (closeButton) {
                closeButton.click();
            } else if (dotNetReference) {
                dotNetReference.invokeMethodAsync('Close');
            }
        }
    };

    document.addEventListener('keydown', handleEscapeKey);

    // Store the handler for cleanup
    element._escapeHandler = handleEscapeKey;

    return true;
}

/**
 * Cleans up event listeners for an element.
 * @param {string} elementId - The ID of the element to clean up.
 * @returns {boolean} True if cleanup was successful.
 */
export function cleanupElement(elementId) {
    const element = document.getElementById(elementId);
    if (!element) {
        return false;
    }

    // Clean up focus trap
    cleanupFocusTrap(elementId);

    // Clean up escape handler
    if (element._escapeHandler) {
        document.removeEventListener('keydown', element._escapeHandler);
        delete element._escapeHandler;
    }

    return true;
}

/**
 * Initializes a toast component with auto-dismiss.
 * @param {string} elementId - The ID of the toast element.
 * @param {number} duration - Duration in milliseconds before auto-hide (0 for no auto-hide).
 * @param {object} dotNetReference - The .NET object reference for callbacks.
 * @returns {number|null} The timeout ID if auto-dismiss was set up, null otherwise.
 */
export function initializeToast(elementId, duration, dotNetReference) {
    const element = document.getElementById(elementId);
    if (!element) {
        return null;
    }

    if (duration > 0 && dotNetReference) {
        const timeoutId = setTimeout(() => {
            dotNetReference.invokeMethodAsync('CloseToast');
        }, duration);
        return timeoutId;
    }

    return null;
}

/**
 * Cancels a toast auto-dismiss timeout.
 * @param {number} timeoutId - The timeout ID to cancel.
 */
export function cancelToastTimeout(timeoutId) {
    if (timeoutId) {
        clearTimeout(timeoutId);
    }
}
