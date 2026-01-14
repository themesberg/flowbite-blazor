/**
 * Flowbite Blazor JavaScript Utilities
 *
 * This file provides global JavaScript functions for Flowbite Blazor components.
 *
 * MIGRATION NOTE: New code should use lazy-loaded ES modules via injected services:
 * - IClipboardService (from clipboard.js module)
 * - IElementService (from element.module.js)
 * - IFocusManagementService (from focus-management.module.js)
 *
 * The global namespace (window.Flowbite, window.flowbiteBlazor) is maintained
 * for backward compatibility but new components should prefer the services.
 */

// Dynamically load the Floating UI bundle if not already loaded
(function() {
    if (typeof window.FlowbiteFloating === 'undefined') {
        const script = document.createElement('script');
        // Get the base path from the current script
        const currentScript = document.currentScript || document.querySelector('script[src*="flowbite.js"]');
        const basePath = currentScript ? currentScript.src.replace(/flowbite\.js.*$/, '') : '/_content/Flowbite/';
        script.src = basePath + 'js/floating-ui.bundle.js';
        script.async = false; // Load synchronously to ensure it's available
        document.head.appendChild(script);
    }
})();

/**
 * Legacy global namespace for Flowbite utilities.
 * @deprecated New code should use IClipboardService instead.
 */
window.Flowbite = {
    init: function() {
        // Initialization complete
    },
    /**
     * @deprecated Use IClipboardService.CopyToClipboardAsync instead.
     * This function is kept for backward compatibility.
     */
    copyToClipboard: async function(content) {
        if (typeof content !== 'string' || !content) {
            console.error('[Flowbite.js] Invalid content provided');
            return false;
        }

        if (navigator.clipboard) {
            try {
                await navigator.clipboard.writeText(content);
                return true;
            } catch (err) {
                if (err.name !== 'NotAllowedError') {
                    console.error('[Flowbite.js] Failed to copy:', err.message);
                    return false;
                }
            }
        }

        // Fallback for older browsers or permission denied
        try {
            const textArea = document.createElement('textarea');
            textArea.value = content;
            textArea.style.cssText = 'position:fixed;opacity:0;pointer-events:none';
            document.body.appendChild(textArea);
            textArea.select();
            const success = document.execCommand('copy');
            document.body.removeChild(textArea);
            return success;
        } catch (err) {
            console.error('[Flowbite.js] Fallback copy failed:', err.message);
            return false;
        }
    },
    /**
     * Highlights code using Prism.js
     */
    highlightCode: async function(element) {
        if (!element) {
            console.warn('highlightCode: No element provided');
            return false;
        }

        if (typeof Prism === 'undefined') {
            console.error('highlightCode: Prism is not defined');
            return false;
        }

        try {
            Prism.highlightElement(element);
            return true;
        } catch (error) {
            console.error('Error highlighting element:', error);
            return false;
        }
    }
};

/**
 * Flowbite Blazor Off-Canvas Components JavaScript Interop
 * 
 * This section contains JavaScript functions for the off-canvas components
 * such as modals, drawers, and toasts.
 */

window.flowbiteBlazor = window.flowbiteBlazor || {};

/**
 * Focus management for off-canvas components
 */
window.flowbiteBlazor.focusManagement = {
    /**
     * The element that had focus before the off-canvas component was shown
     */
    _previouslyFocused: null,
    
    /**
     * Traps focus within the specified element
     * @param {string} elementId - The ID of the element to trap focus within
     */
    trapFocus: function (elementId) {
        const element = document.getElementById(elementId);
        if (!element) return;
        
        // Store the element that had focus before opening the component
        this._previouslyFocused = document.activeElement;
        
        // Find all focusable elements
        const focusableElements = element.querySelectorAll(
            'a[href], button:not([disabled]), textarea:not([disabled]), input:not([disabled]), select:not([disabled]), [tabindex]:not([tabindex="-1"])'
        );
        
        if (focusableElements.length === 0) return;
        
        // Focus the first element
        focusableElements[0].focus();
        
        // Set up event listener for tab key
        element._handleTabKey = function(e) {
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
        
        element.addEventListener('keydown', element._handleTabKey);
    },
    
    /**
     * Restores focus to the previously focused element
     */
    restoreFocus: function () {
        if (this._previouslyFocused && this._previouslyFocused.focus) {
            this._previouslyFocused.focus();
            this._previouslyFocused = null;
        }
    }
};

/**
 * Event handling for off-canvas components
 */
window.flowbiteBlazor.eventHandling = {
    /**
     * Initializes a drawer component
     * @param {string} elementId - The ID of the drawer element
     */
    initializeDrawer: function (elementId) {
        const element = document.getElementById(elementId);
        if (!element) return;
        
        // Add event listener for escape key
        element._handleEscapeKey = function(e) {
            if (e.key === 'Escape') {
                // Find and click the close button
                const closeButton = element.querySelector('[aria-label="Close"]');
                if (closeButton) closeButton.click();
            }
        };
        
        document.addEventListener('keydown', element._handleEscapeKey);
    },
    
    /**
     * Initializes a toast component
     * @param {string} elementId - The ID of the toast element
     * @param {number} duration - The duration in milliseconds before auto-hiding (0 for no auto-hide)
     * @param {Function} closeCallback - The callback function to invoke when the toast is closed
     */
    initializeToast: function (elementId, duration, closeCallback) {
        const element = document.getElementById(elementId);
        if (!element) return;
        
        if (duration > 0) {
            setTimeout(() => {
                if (closeCallback && typeof closeCallback === 'function') {
                    closeCallback();
                }
            }, duration);
        }
    }
};

/**
 * Cleanup functions for off-canvas components
 */
window.flowbiteBlazor.cleanup = {
    /**
     * Cleans up event listeners for an element
     * @param {string} elementId - The ID of the element to clean up
     */
    cleanupElement: function (elementId) {
        const element = document.getElementById(elementId);
        if (!element) return;
        
        if (element._handleTabKey) {
            element.removeEventListener('keydown', element._handleTabKey);
            delete element._handleTabKey;
        }
        
        if (element._handleEscapeKey) {
            document.removeEventListener('keydown', element._handleEscapeKey);
            delete element._handleEscapeKey;
        }
    }
};

/**
 * Body scrolling management for off-canvas components
 */
window.flowbiteBlazor.bodyScrolling = {
    /**
     * Disables body scrolling
     */
    disableBodyScroll: function () {
        document.body.style.overflow = 'hidden';
    },
    
    /**
     * Enables body scrolling
     */
    enableBodyScroll: function () {
        document.body.style.overflow = '';
    }
};

/**
 * Shorthand functions for common operations
 */
window.flowbiteBlazor.trapFocus = function (elementId) {
    return window.flowbiteBlazor.focusManagement.trapFocus(elementId);
};

window.flowbiteBlazor.restoreFocus = function () {
    return window.flowbiteBlazor.focusManagement.restoreFocus();
};

window.flowbiteBlazor.disableBodyScroll = function () {
    return window.flowbiteBlazor.bodyScrolling.disableBodyScroll();
};

window.flowbiteBlazor.enableBodyScroll = function () {
    return window.flowbiteBlazor.bodyScrolling.enableBodyScroll();
};

window.flowbiteBlazor.initializeDrawer = function (elementId) {
    return window.flowbiteBlazor.eventHandling.initializeDrawer(elementId);
};

window.flowbiteBlazor.initializeToast = function (elementId, duration, dotNetReference) {
    return window.flowbiteBlazor.eventHandling.initializeToast(elementId, duration, function() {
        if (dotNetReference) {
            dotNetReference.invokeMethodAsync('CloseToast');
        }
    });
};

window.flowbiteBlazor.cleanupElement = function (elementId) {
    return window.flowbiteBlazor.cleanup.cleanupElement(elementId);
};

/**
 * Element utilities for animations and measurements
 */
window.flowbiteBlazor.element = {
    /**
     * Gets the scroll height of an element for smooth collapse/expand animations
     * @param {HTMLElement} element - The element to measure
     * @returns {number} The scroll height in pixels, or 0 if element is null
     */
    getScrollHeight: function (element) {
        return element?.scrollHeight ?? 0;
    }
};

// Shorthand for element utilities
window.flowbiteBlazor.getScrollHeight = function (element) {
    return window.flowbiteBlazor.element.getScrollHeight(element);
};

// Initialize Flowbite
window.Flowbite.init();
