$(function () {
    let gameNameInput = document.getElementById("gameName");
    let gameDescriptionInput = document.getElementById("gameDescription");
    let gameDeveloperInput = document.getElementById("gameDeveloper");
    let gameThumbnailImageUrlInput = document.getElementById("gameThumbnailImageUrl");
    let deleteFormGameIdInput = document.getElementById("deleteFormGameId");

    $('#game').select2({
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text)),
    }).on('select2:select', function (e) {
        var selectedOption = $(e.params.data.element);

        var gameName = selectedOption.data('gamename');
        var gameDescription = selectedOption.data('gamedescription');
        var gameDeveloper = selectedOption.data('gamedeveloper');
        var gameImageUrl = selectedOption.data('gameimageurl');
        

        gameNameInput.value = gameName;
        gameDescriptionInput.value = gameDescription;
        gameDeveloperInput.value = gameDeveloper;
        gameThumbnailImageUrlInput.value = gameImageUrl;
        deleteFormGameIdInput.value = selectedOption.val();
    });

});

//Genre selector via select2
$(function () {
    $('#genre').select2({
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text)),
    });
});

//Adding and removing images
$(function () {
    $(document).on('click', '.add-button', function (e) {
        e.preventDefault();
        $('#imageUrlList').append('<div class="d-flex flex-row align-content-center"><input required name="ImageUrl[]" placeholder="Enter the image url of the game here..." /><button class="remove-button d-flex justify-content-center align-items-center"><p class="mb-0">X</p></button></div>');
    });

    $(document).on('click', '.remove-button', function (e) {
        e.preventDefault();
        $(this).parent().remove();
    });
});