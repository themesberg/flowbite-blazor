// Flowbite JavaScript utilities
// console.log('[Flowbite.js] Initializing Flowbite JavaScript utilities...');

window.Flowbite = {
    init: function() {
        console.log('[Flowbite.js] Flowbite JavaScript utilities initialized');
        // console.log('[Flowbite.js] Clipboard API available:', !!navigator.clipboard);
    },
    copyToClipboard: async function(content) {

        // Check if content is valid
        if (typeof content !== 'string' || !content) {
            console.error('[Flowbite.js] Invalid content provided');
            return false;
        }

        // Check if clipboard API is available
        if (!navigator.clipboard) {
            console.error('[Flowbite.js] Clipboard API not available');
            return false;
        }

        try {
            await navigator.clipboard.writeText(content);
            // console.log('[Flowbite.js] Text successfully copied to clipboard');
            return true;
        } catch (err) {
            console.error('[Flowbite.js] Failed to copy text:', err.message);
            
            // Fallback to legacy approach if permission denied
            if (err.name === 'NotAllowedError') {
                try {
                    const textArea = document.createElement('textarea');
                    textArea.value = content;
                    textArea.style.position = 'fixed';
                    textArea.style.opacity = '0';
                    document.body.appendChild(textArea);
                    textArea.select();
                    const success = document.execCommand('copy');
                    document.body.removeChild(textArea);
                    
                    if (success) {
                        // console.log('[Flowbite.js] Text copied using fallback method');
                        return true;
                    }
                } catch (fallbackErr) {
                    console.error('[Flowbite.js] Fallback copy method failed:', fallbackErr.message);
                }
            }
            
            return false;
        }
    },
    highlightCode: async function(element) {
        console.log('highlightCode called with element:', element);
        
        if (!element) {
            console.warn('highlightCode: No element provided');
            return false;
        }
        
        if (!Prism) {
            console.error('highlightCode: Prism is not defined');
            return false;
        }
        
        try {
            console.log('Attempting to highlight element with language:', element.className);
            Prism.highlightElement(element);
            console.log('Element highlighted successfully');
            return true;
        } catch (error) {
            console.error('Error highlighting element:', error);
            return false;
        }
    }
};



// Initialize Flowbite
window.Flowbite.init();
