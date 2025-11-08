const outsideClickHandlers = new Map();

export function registerOutsideClick(id, element, dotNetRef) {
    unregisterOutsideClick(id);

    if (!element || !dotNetRef) {
        return;
    }

    const handler = (event) => {
        if (!element.contains(event.target)) {
            dotNetRef.invokeMethodAsync("HandleOutsideClickAsync");
        }
    };

    document.addEventListener("mousedown", handler, true);
    outsideClickHandlers.set(id, handler);
}

export function unregisterOutsideClick(id) {
    const handler = outsideClickHandlers.get(id);
    if (!handler) {
        return;
    }

    document.removeEventListener("mousedown", handler, true);
    outsideClickHandlers.delete(id);
}
