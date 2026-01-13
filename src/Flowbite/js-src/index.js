/**
 * Flowbite Blazor Floating UI Module
 *
 * This module provides viewport-aware positioning for dropdowns, tooltips, and popovers.
 * It integrates @floating-ui/dom for intelligent flip/shift behavior when elements
 * would otherwise overflow the viewport.
 */

import * as positioning from './positioning.js';
import * as dom from './dom.js';

// Export all positioning functions to global scope for Blazor JS interop
window.FlowbiteFloating = {
    /**
     * Initializes floating positioning for an element pair.
     * @param {string} id - Unique identifier for this floating instance
     * @param {object} options - Positioning options
     * @returns {Promise<object>} Result containing actual placement
     */
    initialize: positioning.initialize,

    /**
     * Manually triggers a position update for a floating element.
     * @param {string} id - The floating instance identifier
     */
    updatePosition: positioning.updatePosition,

    /**
     * Destroys a floating instance and cleans up resources.
     * @param {string} id - The floating instance identifier
     */
    destroy: positioning.destroy,

    /**
     * Checks if a floating instance exists.
     * @param {string} id - The floating instance identifier
     * @returns {boolean} True if the instance exists
     */
    exists: positioning.exists,

    /**
     * Gets the current placement of a floating element.
     * @param {string} id - The floating instance identifier
     * @returns {string|null} The current placement or null if not found
     */
    getPlacement: positioning.getPlacement,

    /**
     * DOM utilities
     */
    dom: {
        portal: dom.portal,
        waitForElement: dom.waitForElement
    }
};

// Log initialization in development
if (typeof process === 'undefined' || process.env?.NODE_ENV !== 'production') {
    console.log('[FlowbiteFloating] Module loaded');
}
