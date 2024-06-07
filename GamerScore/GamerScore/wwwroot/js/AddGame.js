//Genre selector via select2
$(document).ready(function () {
    $('#genre').select2({
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text)),
    });
});

//Adding and removing images
$(document).ready(function () {
    $('.add-button').click(function (e) {
        e.preventDefault();
        $('#imageUrlList').append('<div class="d-flex flex-row align-content-center"><input required name="ImageUrl[]" placeholder="Enter the image url of the game here..." /><button class="remove-button d-flex justify-content-center align-items-center"><p class="mb-0">X</p></button></div>');
    });

    $(document).on('click', '.remove-button', function (e) {
        e.preventDefault();
        $(this).parent().remove();
    });
});