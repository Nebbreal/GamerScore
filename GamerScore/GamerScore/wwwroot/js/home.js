let scrollAmount = remToPx(78)
function ScrollLeft(itemListId) {
    let itemlist = document.getElementById(itemListId);
    let scrollPosition = itemlist.scrollLeft;
    itemlist.scroll({
        left: scrollPosition - scrollAmount,
        behavior: "smooth",
    });
}

function ScrollRight(itemListId) {
    let itemlist = document.getElementById(itemListId);
    let scrollPosition = itemlist.scrollLeft;
    itemlist.scroll({
        left: scrollPosition + scrollAmount,
        behavior: "smooth",
    });
}