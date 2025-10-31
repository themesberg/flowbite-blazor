#### Carousel Examples

The Carousel component allows you to create image slideshows or content carousels with navigation controls and indicators.

**Main Components:**
- `Carousel` - Main container component
- `CarouselItem` - Individual slide wrapper
- `CarouselControls` - Previous/Next navigation buttons
- `CarouselIndicators` - Dot indicators for direct slide navigation

**Available CarouselImageFit options:**
- CarouselImageFit.Cover (default)
- CarouselImageFit.Contain
- CarouselImageFit.Fill
- CarouselImageFit.ScaleDown
- CarouselImageFit.None

**Available CarouselIndicatorPosition options:**
- CarouselIndicatorPosition.Bottom (default)
- CarouselIndicatorPosition.Top

```razor
<!-- Basic carousel with images -->
<Carousel>
    <CarouselItem ImageSrc="/images/slide1.jpg" ImageAlt="Slide 1" />
    <CarouselItem ImageSrc="/images/slide2.jpg" ImageAlt="Slide 2" />
    <CarouselItem ImageSrc="/images/slide3.jpg" ImageAlt="Slide 3" />
    <CarouselControls />
    <CarouselIndicators />
</Carousel>

<!-- Auto-advancing carousel (3 second interval) -->
<Carousel AutoAdvanceInterval="3000">
    <CarouselItem ImageSrc="/images/slide1.jpg" ImageAlt="Slide 1" />
    <CarouselItem ImageSrc="/images/slide2.jpg" ImageAlt="Slide 2" />
    <CarouselControls />
    <CarouselIndicators />
</Carousel>

<!-- Custom content slides -->
<Carousel>
    <CarouselItem>
        <div class="flex items-center justify-center h-full bg-blue-500 text-white">
            <h3>Custom Content</h3>
        </div>
    </CarouselItem>
    <CarouselItem>
        <div class="flex items-center justify-center h-full bg-green-500 text-white">
            <h3>Another Slide</h3>
        </div>
    </CarouselItem>
    <CarouselControls />
    <CarouselIndicators Position="CarouselIndicatorPosition.Top" />
</Carousel>

<!-- Controlled carousel with two-way binding -->
<Carousel @bind-Index="currentSlide">
    <CarouselItem ImageSrc="/images/slide1.jpg" ImageAlt="Slide 1" />
    <CarouselItem ImageSrc="/images/slide2.jpg" ImageAlt="Slide 2" />
    <CarouselItem ImageSrc="/images/slide3.jpg" ImageAlt="Slide 3" />
    <CarouselControls />
    <CarouselIndicators />
</Carousel>

@code {
    private int currentSlide = 0;
}

<!-- Image fit options -->
<Carousel>
    <CarouselItem 
        ImageSrc="/images/slide1.jpg" 
        ImageAlt="Cover fit" 
        ImageFit="CarouselImageFit.Cover" />
    <CarouselItem 
        ImageSrc="/images/slide2.jpg" 
        ImageAlt="Contain fit" 
        ImageFit="CarouselImageFit.Contain" />
    <CarouselControls />
    <CarouselIndicators />
</Carousel>
```

**Key Parameters:**
- `Index` / `@bind-Index` - Current slide index (zero-based)
- `AutoAdvanceInterval` - Auto-advance interval in milliseconds (null = disabled)
- `TransitionDuration` - Transition duration in milliseconds (default: 1000)
- `OnSlideChanged` - Event callback when slide changes
- `ImageSrc` - Image URL for CarouselItem
- `ImageAlt` - Alt text for image
- `ImageFit` - How image fits in container
- `Position` - Indicator position (Top/Bottom)
