$(document).ready(function () {
    $('.btn').click(function () {

        var text = $('#text').val();
        var instruction = $('#instruction').val();

        if (instruction.trim().length < 1) {
            alert("Cannot Leave Instruction Blank");
        }
        else {
            $.ajax({
                url: '/TextWebApi/SendRequest',
                type: 'POST',
                data: {
                    "text": text,
                    "instruction": instruction
                },
                success: function (response) {
                    // Handle the response from the API if needed

                    const datagrid = $("#chat-datagrid").dxDataGrid("instance");

                    if (datagrid) {
                        datagrid.refresh();
                    }

                    $('#text').val("");
                    $('#instruction').val("");

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