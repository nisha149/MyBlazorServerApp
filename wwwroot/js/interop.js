window.isMobileDevice = () => {
    return window.innerWidth <= 768;
};

window.setupResizeListener = (dotNetObject) => {
    const handleResize = () => {
        const isMobile = window.innerWidth <= 768;
        dotNetObject.invokeMethodAsync('UpdateMobileState', isMobile);
    };
    window.addEventListener('resize', handleResize);
    // Initial call to set the state
    handleResize();
};