/**
 * Floating UI positioning utilities for Flowbite Blazor.
 */
import { computePosition, flip, shift, offset, autoUpdate, arrow } from '@floating-ui/dom';

// Store for active floating instances
const instances = new Map();

/**
 * Initializes floating positioning for an element pair.
 * @param {string} id - Unique identifier for this floating instance
 * @param {object} options - Positioning options
 * @param {string} options.placement - Initial placement (top, bottom, left, right, etc.)
 * @param {number} options.offset - Offset distance from reference element
 * @param {boolean} options.enableFlip - Whether to flip placement when constrained
 * @param {boolean} options.enableShift - Whether to shift along the axis when constrained
 * @param {number} options.shiftPadding - Padding for shift middleware
 * @returns {Promise<object>} Result containing actual placement
 */
export async function initialize(id, options = {}) {
    const trigger = document.querySelector(`[data-floating-trigger="${id}"]`);
    const floating = document.querySelector(`[data-floating-element="${id}"]`);
    const arrowElement = floating?.querySelector(`[data-floating-arrow="${id}"]`);

    if (!trigger || !floating) {
        console.warn(`[FloatingUI] Could not find trigger or floating element for id: ${id}`);
        return null;
    }

    // Destroy existing instance if present
    if (instances.has(id)) {
        destroy(id);
    }

    const {
        placement = 'bottom',
        offset: offsetValue = 8,
        enableFlip = true,
        enableShift = true,
        shiftPadding = 8
    } = options;

    // Build middleware array
    const middleware = [offset(offsetValue)];

    if (enableFlip) {
        middleware.push(flip());
    }

    if (enableShift) {
        middleware.push(shift({ padding: shiftPadding }));
    }

    if (arrowElement) {
        middleware.push(arrow({ element: arrowElement }));
    }

    const update = async () => {
        const result = await computePosition(trigger, floating, {
            placement,
            middleware
        });

        const { x, y, placement: actualPlacement, middlewareData } = result;

        Object.assign(floating.style, {
            left: `${x}px`,
            top: `${y}px`,
            position: 'absolute'
        });

        // Store the actual placement for CSS styling
        floating.dataset.placement = actualPlacement;

        // Position arrow if present
        if (arrowElement && middlewareData.arrow) {
            const { x: arrowX, y: arrowY } = middlewareData.arrow;

            const staticSide = {
                top: 'bottom',
                right: 'left',
                bottom: 'top',
                left: 'right'
            }[actualPlacement.split('-')[0]];

            Object.assign(arrowElement.style, {
                left: arrowX != null ? `${arrowX}px` : '',
                top: arrowY != null ? `${arrowY}px` : '',
                right: '',
                bottom: '',
                [staticSide]: '-4px'
            });
        }

        return actualPlacement;
    };

    // Initial position
    const actualPlacement = await update();

    // Set up auto-update (handles scroll, resize, etc.)
    const cleanup = autoUpdate(trigger, floating, update);

    instances.set(id, {
        cleanup,
        floating,
        trigger,
        arrowElement
    });

    return { placement: actualPlacement };
}

/**
 * Manually triggers a position update for a floating element.
 * @param {string} id - The floating instance identifier
 */
export async function updatePosition(id) {
    const instance = instances.get(id);
    if (!instance) return;

    const { trigger, floating, arrowElement } = instance;
    if (!trigger || !floating) return;

    const placement = floating.dataset.originalPlacement || 'bottom';

    const middleware = [offset(8), flip(), shift({ padding: 8 })];
    if (arrowElement) {
        middleware.push(arrow({ element: arrowElement }));
    }

    const result = await computePosition(trigger, floating, {
        placement,
        middleware
    });

    const { x, y, placement: actualPlacement } = result;

    Object.assign(floating.style, {
        left: `${x}px`,
        top: `${y}px`
    });

    floating.dataset.placement = actualPlacement;
}

/**
 * Destroys a floating instance and cleans up resources.
 * @param {string} id - The floating instance identifier
 */
export function destroy(id) {
    const instance = instances.get(id);
    if (instance) {
        if (instance.cleanup) {
            instance.cleanup();
        }
        instances.delete(id);
    }
}

/**
 * Checks if a floating instance exists.
 * @param {string} id - The floating instance identifier
 * @returns {boolean} True if the instance exists
 */
export function exists(id) {
    return instances.has(id);
}

/**
 * Gets the current placement of a floating element.
 * @param {string} id - The floating instance identifier
 * @returns {string|null} The current placement or null if not found
 */
export function getPlacement(id) {
    const instance = instances.get(id);
    if (!instance || !instance.floating) return null;
    return instance.floating.dataset.placement || null;
}
