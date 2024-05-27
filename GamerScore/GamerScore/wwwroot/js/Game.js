function showCreateReview() {
    var backgroundFade = document.getElementById("fade-bg");
    var reviewContainer = document.getElementById("review-container");

    reviewContainer.style.display = "flex";
    backgroundFade.style.display = "block";
}

function closeReview() {
    var backgroundFade = document.getElementById("fade-bg");
    var reviewContainer = document.getElementById("review-container");

    reviewContainer.style.display = "none";
    backgroundFade.style.display = "none";
}