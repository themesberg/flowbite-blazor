// Initialize highlight.js for markdown code blocks
window.highlightMarkdownCode = function (element) {
    if (!element || typeof hljs === 'undefined') {
        return;
    }

    // Find all code blocks within the markdown content
    const codeBlocks = element.querySelectorAll('pre code');
    
    codeBlocks.forEach(block => {
        // Only highlight if not already highlighted
        if (!block.classList.contains('hljs')) {
            hljs.highlightElement(block);
        }
    });
};
