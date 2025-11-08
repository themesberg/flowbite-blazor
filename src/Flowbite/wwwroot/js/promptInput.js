const registry = new WeakMap();

function normalizeOptions(options) {
  return {
    globalDrop: Boolean(options?.globalDrop)
  };
}

function preventDefaults(event) {
  event.preventDefault();
  event.stopPropagation();
}

function hasFiles(event) {
  const types = event?.dataTransfer?.types;
  if (!types) {
    return false;
  }

  return Array.from(types).includes('Files');
}

function cleanupFiles(files) {
  if (!files) {
    return;
  }

  for (const file of files) {
    try {
      file?.stream?.dispose?.();
    } catch {
      // Swallow disposal errors to avoid interrupting the UI flow.
    }
  }
}

function toFilePayload(fileList) {
  if (!fileList || fileList.length === 0) {
    return [];
  }

  const results = [];

  for (let index = 0; index < fileList.length; index += 1) {
    const file = fileList.item(index);
    if (!file) {
      continue;
    }

    try {
      const stream = typeof DotNet !== 'undefined' && DotNet.createJSStreamReference
        ? DotNet.createJSStreamReference(file.stream())
        : null;

      if (!stream) {
        continue;
      }

      results.push({
        name: file.name ?? '',
        size: file.size ?? 0,
        contentType: file.type ?? '',
        lastModified: file.lastModified ?? Date.now(),
        stream
      });
    } catch {
      // Ignore files that cannot be streamed (e.g., because the browser does not expose a stream API).
    }
  }

  return results;
}

function notifyDragState(element, state, isActive) {
  if (state.dragActive === isActive) {
    return;
  }

  state.dragActive = isActive;
  if (!state.dotNetReference) {
    return;
  }

  state.dotNetReference.invokeMethodAsync('SetDragState', isActive).catch(() => {});
}

function registerListeners(element, state) {
  const listeners = {
    dragEnter: event => {
      if (!hasFiles(event)) {
        return;
      }
      preventDefaults(event);
      state.dragCounter += 1;
      notifyDragState(element, state, true);
    },
    dragOver: event => {
      if (!hasFiles(event)) {
        return;
      }
      preventDefaults(event);
    },
    dragLeave: event => {
      if (!hasFiles(event)) {
        return;
      }
      preventDefaults(event);
      state.dragCounter = Math.max(0, state.dragCounter - 1);
      if (state.dragCounter === 0) {
        notifyDragState(element, state, false);
      }
    },
    drop: event => {
      if (!hasFiles(event)) {
        return;
      }

      preventDefaults(event);
      state.dragCounter = 0;
      notifyDragState(element, state, false);

      const payload = toFilePayload(event?.dataTransfer?.files);
      if (payload.length === 0) {
        cleanupFiles(payload);
        return;
      }

      state.dotNetReference?.invokeMethodAsync('ReceiveFiles', payload).catch(() => {
        cleanupFiles(payload);
      });
    },
    paste: event => {
      const payload = toFilePayload(event?.clipboardData?.files);
      if (payload.length === 0) {
        cleanupFiles(payload);
        return;
      }

      preventDefaults(event);
      state.dotNetReference?.invokeMethodAsync('ReceiveFiles', payload).catch(() => {
        cleanupFiles(payload);
      });
    }
  };

  element.addEventListener('dragenter', listeners.dragEnter);
  element.addEventListener('dragover', listeners.dragOver);
  element.addEventListener('dragleave', listeners.dragLeave);
  element.addEventListener('drop', listeners.drop);
  element.addEventListener('paste', listeners.paste);

  return listeners;
}

export function openFilePicker(element) {
  if (!element) {
    return;
  }

  element.click();
}

export function registerPromptInput(element, dotNetReference, options) {
  if (!element || !dotNetReference) {
    return;
  }

  unregisterPromptInput(element);

  const normalized = normalizeOptions(options);
  const state = {
    dotNetReference,
    options: normalized,
    dragCounter: 0,
    dragActive: false
  };

  const listeners = registerListeners(element, state);
  registry.set(element, { ...state, listeners });
}

export function updatePromptInputOptions(element, options) {
  const state = registry.get(element);
  if (!state) {
    return;
  }

  state.options = normalizeOptions({ ...state.options, ...options });
}

export function unregisterPromptInput(element) {
  const entry = registry.get(element);
  if (!entry) {
    return;
  }

  const { listeners } = entry;
  element.removeEventListener('dragenter', listeners.dragEnter);
  element.removeEventListener('dragover', listeners.dragOver);
  element.removeEventListener('dragleave', listeners.dragLeave);
  element.removeEventListener('drop', listeners.drop);
  element.removeEventListener('paste', listeners.paste);

  notifyDragState(element, entry, false);
  registry.delete(element);
}
