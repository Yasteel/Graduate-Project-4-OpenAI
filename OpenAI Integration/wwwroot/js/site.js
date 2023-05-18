$(document).ready(function () {
    $('.btn').click(function () {

        var input = $('#msgBox').val();

        $.ajax({
            url: '/ChatWebApi/Test',
            type: 'POST',
            data: {
                "message": input
            },
            success: function (response) {
                // Handle the response from the API if needed
                console.log(response);
            },
            error: function (xhr, textStatus, errorThrown) {
                // Handle any errors that occur during the AJAX request
                console.log('Error: ' + errorThrown);
            }
        });
    });
});