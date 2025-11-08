const registry = new WeakMap();

function isAtBottom(element) {
  return element.scrollHeight - element.scrollTop - element.clientHeight <= 8;
}

function scrollToBottom(element) {
  const entry = registry.get(element);

  if (entry?.pendingFallback) {
    clearTimeout(entry.pendingFallback);
    registry.set(element, { ...entry, pendingFallback: undefined });
  }

  if (!entry?.dotNetReference) {
    return;
  }

  const triggerUpdate = () => {
    const currentEntry = registry.get(element);
    if (currentEntry) {
      handleScroll(element, currentEntry.dotNetReference);
    }
  };

  // Double requestAnimationFrame to ensure scrollHeight is accurate after all layout updates
  requestAnimationFrame(() => {
    requestAnimationFrame(() => {
      element.scrollTo({
        top: element.scrollHeight,
        behavior: 'smooth'
      });

      const handleCompletion = () => {
        // Force instant scroll to absolute bottom to ensure we reach it
        element.scrollTo({
          top: element.scrollHeight,
          behavior: 'instant'
        });
        
        // Trigger update after forcing final position
        requestAnimationFrame(() => triggerUpdate());
      };

      if ('onscrollend' in element) {
        element.addEventListener('scrollend', handleCompletion, { once: true });
      } else {
        setTimeout(handleCompletion, 500);
      }
    });
  });
}

function handleScroll(element, dotNetReference) {
  const atBottom = isAtBottom(element);
  const previous = registry.get(element);
  if (!previous || previous.atBottom !== atBottom) {
    registry.set(element, { ...previous, atBottom });
    dotNetReference.invokeMethodAsync('UpdateIsAtBottom', atBottom).catch(() => {});
  }
}

export function registerConversation(element, dotNetReference) {
  if (!element) {
    return;
  }

  const listener = () => handleScroll(element, dotNetReference);
  element.addEventListener('scroll', listener, { passive: true });
  registry.set(element, { listener, dotNetReference, atBottom: true });
  requestAnimationFrame(() => listener());
}

export function unregisterConversation(element) {
  const entry = registry.get(element);
  if (!entry) {
    return;
  }

  const { listener, pendingFallback } = entry;
  element.removeEventListener('scroll', listener);
  if (pendingFallback) {
    clearTimeout(pendingFallback);
  }
  registry.delete(element);
}

export { scrollToBottom };
