$(document).ready(function () {
    $('.btn').click(function () {

        var input = $('#msgBox').val();

        $.ajax({
            url: '/ChatWebApi/SendRequest',
            type: 'POST',
            data: {
                "message": input
            },
            success: function (response) {
                // Handle the response from the API if needed
                console.log(response);

                const datagrid = $("#chat-datagrid").dxDataGrid("instance");

                if (datagrid) {
                    datagrid.refresh();
                }

                updateLocalStorage();
            },
            error: function (xhr, textStatus, errorThrown) {
                // Handle any errors that occur during the AJAX request
                console.log('Error: ' + errorThrown);
            }
        });
    });
});

var glob;

function updateLocalStorage() {
    $.ajax({
        url: '/ChatWebApi/GetMessages',
        type: 'POST',
        success: function (response) {
            var jsonObj = JSON.parse(response);
            localStorage.setItem(jsonObj.CacheKey, JSON.stringify(jsonObj.Messages));
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(`Err: ${errorThrown}`);
        }
    });
}


