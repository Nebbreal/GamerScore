function remToPx(rem) {
    // Get the root font size in pixels
    const rootFontSize = parseFloat(getComputedStyle(document.documentElement).fontSize);

    // Convert rem to pixels
    const px = rem * rootFontSize;

    return px;
}
