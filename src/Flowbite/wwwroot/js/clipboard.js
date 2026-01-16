/**
 * Flowbite Blazor Clipboard Module
 * ES module for clipboard operations with lazy loading support.
 */

/**
 * Copies content to the system clipboard.
 * Uses the modern Clipboard API with a fallback for older browsers.
 * @param {string} content - The text content to copy to clipboard.
 * @returns {Promise<boolean>} True if copy was successful, false otherwise.
 */
export async function copyToClipboard(content) {
    // Validate content
    if (typeof content !== 'string' || !content) {
        console.error('[Flowbite.Clipboard] Invalid content provided');
        return false;
    }

    // Try modern Clipboard API first
    if (navigator.clipboard) {
        try {
            await navigator.clipboard.writeText(content);
            return true;
        } catch (err) {
            // Fall through to fallback if permission denied
            if (err.name !== 'NotAllowedError') {
                console.error('[Flowbite.Clipboard] Failed to copy:', err.message);
                return false;
            }
        }
    }

    // Fallback for older browsers or permission denied
    return fallbackCopy(content);
}

/**
 * Fallback copy method using execCommand (deprecated but widely supported).
 * @param {string} content - The text content to copy.
 * @returns {boolean} True if copy was successful, false otherwise.
 */
function fallbackCopy(content) {
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
        console.error('[Flowbite.Clipboard] Fallback copy failed:', err.message);
        return false;
    }
}
