$(document).ready(function () {
    $('.btn').click(function () {

        var prompt = $('#prompt').val();
        var numberOfImages = $('#numberOfImages').val();
        var imageSize = $('#imageSize').val();

        if (prompt.trim().length < 1) {
            alert("Cannot leave Prompt Blank")
        }
        else {
            $.ajax({
                url: '/ImageWebApi/SendRequest',
                type: 'POST',
                data: {
                    "prompt": prompt,
                    "numberOfImages": parseInt(numberOfImages),
                    "imageSize": imageSize
                },
                success: function (response) {
                    // Handle the response from the API if needed

                    const datagrid = $("#chat-datagrid").dxDataGrid("instance");

                    if (datagrid) {
                        datagrid.refresh();
                    }

                    $('#prompt').val("");
                    $('#numberOfImages').val("");
                    $('#imageSize').val("");

                    //updateLocalStorage();
                },
                error: function (xhr, textStatus, errorThrown) {
                    // Handle any errors that occur during the AJAX request
                    console.log('Error: ' + errorThrown);
                }
            });
        }
    });
});