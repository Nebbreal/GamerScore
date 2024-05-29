﻿function showCreateReview() {
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

function changeRating(starId) {
    let trueRating = starId + 1;
    var ratingInput = document.getElementById("rating");
    
    ratingInput.value = trueRating.toString();

    for (let i = 0; i <= parseInt(starId); i++) { //Selected
        //Change the image
        var starImg = document.querySelector("#star" + i + " img");
        starImg.src = "../icons/star-favorite-half.svg";
    }
    for (let i = parseInt(starId) + 1; i <= 9; i++) { //Deselected
        //Change the image
        var starImg = document.querySelector("#star" + i + " img");
        starImg.src = "../icons/star-favorite-half-empty.svg";
    }
    console.log(ratingInput.value);
}