const registry = new WeakMap();

function isAtBottom(element) {
  return element.scrollHeight - element.scrollTop - element.clientHeight <= 8;
}

function scrollToBottom(element) {
  element.scrollTo({
    top: element.scrollHeight,
    behavior: 'smooth'
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
  // Capture the initial scroll position after the current frame to ensure measurements are up to date.
  requestAnimationFrame(() => listener());
}

export function unregisterConversation(element) {
  const entry = registry.get(element);
  if (!entry) {
    return;
  }

  const { listener } = entry;
  element.removeEventListener('scroll', listener);
  registry.delete(element);
}

export { scrollToBottom };
