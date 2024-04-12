function ScrollLeft() {
    let itemlist = document.getElementById("newlyAddedItemList");
    let scrollPosition = itemlist.scrollLeft;
    itemlist.scroll({
        left: scrollPosition - remToPx(81),
        behavior: "smooth",
    });
}

function ScrollRight() {
    let itemlist = document.getElementById("newlyAddedItemList");
    let scrollPosition = itemlist.scrollLeft;
    itemlist.scroll({
        left: scrollPosition + remToPx(81),
        behavior: "smooth",
    });
}