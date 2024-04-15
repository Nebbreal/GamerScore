let scrollAmount = remToPx(78)
function ScrollLeft() {
    let itemlist = document.getElementById("newlyAddedItemList");
    let scrollPosition = itemlist.scrollLeft;
    itemlist.scroll({
        left: scrollPosition - scrollAmount,
        behavior: "smooth",
    });
}

function ScrollRight() {
    let itemlist = document.getElementById("newlyAddedItemList");
    let scrollPosition = itemlist.scrollLeft;
    itemlist.scroll({
        left: scrollPosition + scrollAmount,
        behavior: "smooth",
    });
}