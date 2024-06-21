$(function () {
    let genreNameInput = document.getElementById("genreName");
    let genreImageUrlInput = document.getElementById("genreImageUrl");
    let deleteFormGenreIdInput = document.getElementById("deleteFormGenreId");

    $('#genre').select2({
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text)),
    }).on('select2:select', function (e) {
        var selectedOption = $(e.params.data.element);

        var genreName = selectedOption.data('genrename');
        var genreImageUrl = selectedOption.data('genreimageurl');

        genreNameInput.value = genreName;
        genreImageUrlInput.value = genreImageUrl;
        deleteFormGenreIdInput.value = selectedOption.val();
    });

});