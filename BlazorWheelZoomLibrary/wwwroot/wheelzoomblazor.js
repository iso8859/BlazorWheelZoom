window.WheelZoomJsFunctions = {
    naturalWidth: function (el) {
        return el.naturalWidth;
    },
    naturalHeight: function (el) {
        return el.naturalHeight;
    },
    boundingRect: function (el) {
        return el.getBoundingClientRect();
    },
    setFocus: function (el) {
        el.focus();
    },
    capturePointer: function (el, p) {
        el.setPointerCapture(p);
    },
    releasePointer: function (el, p) {
        el.releasePointerCapture(p);
    }
};