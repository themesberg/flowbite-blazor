

window.getInnerWidth = function() {
    console.log("DemoApp::getInnerWidth()");
    if (typeof window !== "undefined")
        return window.innerWidth;
}