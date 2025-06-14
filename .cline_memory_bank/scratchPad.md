# Scratch Pad

## Modal Component Implementation Notes

### Compound Component Pattern Implementation
The Modal component has been fully implemented using the compound component pattern with ModalHeader, ModalBody, and ModalFooter components. This pattern provides more flexibility and control over the modal's structure and content.

### Key Components
1. **ModalContext**: A class that shares state between the parent Modal component and its child components. It includes:
   - Id: The unique identifier for the modal
   - Dismissible: Whether the modal can be dismissed by clicking outside or pressing Escape
   - CloseAsync: A function to close the modal

2. **Modal**: The parent component that manages the state and provides the context to its children.

3. **ModalHeader**: The header component that displays a title and optionally a close button.

4. **ModalBody**: The body component that contains the main content of the modal.

5. **ModalFooter**: The footer component that typically contains action buttons.

### Implementation Details
- The Modal component uses a CascadingValue to provide the ModalContext to its children.
- The Modal component supports both the new compound component pattern and the legacy API for backward compatibility.
- The ModalHeader, ModalBody, and ModalFooter components inherit from FlowbiteComponentBase to leverage common functionality.
- The Modal component inherits from OffCanvasComponentBase to leverage common functionality for off-canvas components.

### CSS Fixes and Improvements
- Removed the fixed `mt-10` margin-top class from the modal content div, which was interfering with the positioning options.
- Removed the `overflow-y-auto` and `overflow-x-hidden` classes from the backdrop div, which were causing scrolling issues.
- Added `p-12` padding to the backdrop div to ensure proper spacing around the modal.
- Added `w-full` class to the modal container to ensure proper sizing, especially when using the Size parameter.
- Increased the max height from 80dvh to 90dvh to allow more content to be displayed.

### Positioning and Sizing
- The positioning of the modal is controlled by the Position parameter, which maps to specific CSS classes for flex alignment.
- The size of the modal is controlled by the Size parameter, which maps to specific CSS classes for maximum width.
- Both positioning and sizing work correctly and are demonstrated in the ModalPage.razor demo page.

### Demo Page Improvements
- Added a Modal Sizing example to showcase different size options.
- Updated the Default Modal example to demonstrate handling user choices (Accept/Decline) with a visual indicator.
- Ensured all examples are well-documented and easy to understand.

### Lessons Learned
1. When implementing compound components, it's important to carefully manage the CSS classes to ensure proper styling and layout.
2. Using a CascadingValue to share state between parent and child components is a powerful pattern for creating flexible and reusable components.
3. Tailwind CSS classes can sometimes interfere with each other, so it's important to test different combinations to ensure they work as expected.
4. The `w-full` class is essential for ensuring proper sizing of flex containers, especially when using max-width classes.
5. Removing unnecessary overflow classes can help prevent scrolling issues in modal dialogs.

## Drawer Component Implementation Notes

### Compound Component Pattern Implementation
The Drawer component has been implemented using the compound component pattern with DrawerHeader and DrawerItems components, similar to the Modal component. This pattern provides flexibility and control over the drawer's structure and content.

### Key Components
1. **DrawerContext**: A class that shares state between the parent Drawer component and its child components. It includes:
   - Id: The unique identifier for the drawer
   - Dismissible: Whether the drawer can be dismissed by clicking outside or pressing Escape
   - CloseAsync: A function to close the drawer

2. **Drawer**: The parent component that manages the state and provides the context to its children.

3. **DrawerHeader**: The header component that displays a title and can optionally toggle the drawer when clicked.

4. **DrawerItems**: The component that contains the main content of the drawer.

### Implementation Details
- The Drawer component uses a CascadingValue to provide the DrawerContext to its children.
- The Drawer component supports both the new compound component pattern and the legacy API for backward compatibility.
- The DrawerHeader and DrawerItems components inherit from FlowbiteComponentBase to leverage common functionality.
- The Drawer component inherits from OffCanvasComponentBase to leverage common functionality for off-canvas components.

### Inheritance and Method Overriding
- Changed `override` to `new` for the ShowAsync and HideAsync methods to avoid conflicts with the base class implementation.
- Added a HandleEscapeKey method that calls the base HandleEscapeKeyAsync method to properly handle keyboard events.
- Removed duplicate Class properties from Drawer, DrawerHeader, and DrawerItems components since they already inherit from FlowbiteComponentBase.

### CSS and Positioning
- The positioning of the drawer is controlled by the Position parameter, which maps to specific CSS classes for transform and placement.
- The drawer supports four positions: Left, Right, Top, and Bottom, each with its own CSS classes for proper placement and animation.
- The backdrop is optional and can be toggled with the Backdrop parameter.
- Body scrolling can be enabled or disabled with the BodyScrolling parameter.

### Edge Feature Issue
- The Edge feature is designed to show a small part of the drawer when it's closed, but currently the header is not visible in this mode.
- Potential causes:
  1. CSS z-index issues: The drawer might have a z-index that's too low when in edge mode.
  2. Positioning issues: The positioning of the drawer in edge mode might be incorrect.
  3. Visibility issues: The header might be hidden or clipped in edge mode.
  4. Transform issues: The transform property might be affecting the visibility of the header.
- Possible solutions to investigate:
  1. Adjust the z-index of the drawer in edge mode.
  2. Modify the positioning of the drawer in edge mode to ensure the header is visible.
  3. Add specific CSS classes for the header in edge mode.
  4. Review the transform properties to ensure they don't hide the header.

### Demo Page Implementation
- Added comprehensive examples to showcase all features of the Drawer component:
  - Default Drawer
  - Navigation Drawer
  - Drawer Placement (Left, Right, Top, Bottom)
  - No Backdrop
  - Body Scrolling
  - Edge Drawer
- Each example demonstrates a specific feature and includes explanatory text.
- Fixed Button component event handler syntax from `@onclick` to `OnClick` to follow the correct pattern.

### Next Steps for Edge Feature
1. Investigate the CSS classes applied to the drawer in edge mode.
2. Check how the Edge parameter affects the positioning and visibility of the drawer.
3. Test different CSS combinations to ensure the header is visible in edge mode.
4. Consider adding specific CSS classes for the header in edge mode to ensure visibility.
5. Update the demo page once the issue is fixed to properly showcase the Edge feature.
