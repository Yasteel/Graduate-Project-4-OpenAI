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

    $(document).on('click', '.historyItem', function(){

        getSavedChat($(this));
    });

    updateChatHistory();
});

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

function updateChatHistory() {
    var local = window.localStorage;
    var chatKeys = [];


    for (key in local) {
        if (key.includes("chat;")) {

            var keySplit = key.split(";");

            chatKeys.push({
                "prefix": keySplit[0],
                "timestamp": keySplit[1],
                "title": keySplit[2]
            });
        }
    }

    $.ajax({
        url: '/ChatWebApi/SetChatHistory',
        type: 'POST',
        data: {
            "history": JSON.stringify(chatKeys)
        },
        success: function (response) {
            const datagrid = $("#chat-history-datagrid").dxDataGrid("instance");

            if (datagrid) {
                datagrid.refresh();
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(`Err: ${errorThrown}`);
        }
    });
}

function getSavedChat(element) {

    var key = `chat;${$(element).attr("data-key")}`;

    var localChat = localStorage.getItem(key);

    $.ajax({
        url: '/ChatWebApi/GetSavedChat',
        type: 'POST',
        data: {
            "chat": localChat,
            "key": key
        },
        success: function (response) {
            const datagrid = $("#chat-datagrid").dxDataGrid("instance");

            if (datagrid) {
                datagrid.refresh();
            }


            $('.historyItem.active').removeClass('active');
            $(element).addClass('active');
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(`Err: ${errorThrown}`);
        }
    });

}
