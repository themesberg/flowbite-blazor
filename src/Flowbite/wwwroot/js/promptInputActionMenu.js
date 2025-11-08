let clickOutsideHandler = null;

export function initialize(element, dotNetRef) {
    // Initial setup if needed
}

export function setupClickOutside(element, dotNetRef) {
    // Remove existing handler if any
    dispose();

    // Add a small delay to prevent immediate triggering
    setTimeout(() => {
        clickOutsideHandler = (event) => {
            if (element && !element.contains(event.target)) {
                dotNetRef.invokeMethodAsync('OnClickOutside');
            }
        };
        document.addEventListener('click', clickOutsideHandler, true);
    }, 10);
}

export function dispose() {
    if (clickOutsideHandler) {
        document.removeEventListener('click', clickOutsideHandler, true);
        clickOutsideHandler = null;
    }
}
