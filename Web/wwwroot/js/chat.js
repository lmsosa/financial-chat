"use strict";
var userName = null;

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

connection.on("ReceiveMessage", function (message, user, dateTime) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    let $div = null;

    if (user == userName) {
        $div = $(`<div class="outgoing_msg">
                       <div class="sent_msg">
                           <p>${ msg }</p>
                           <span class="time_date">You     |  ${ dateTime }</span>
                       </div>
                   </div>`);
    } else {
        $div = $(`<div class="incoming_msg">
                   <div class="incoming_msg_img"> <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil"> </div>
                   <div class="received_msg">
                       <div class="received_withd_msg">
                           <p>${msg}</p> 
                           <span class="time_date">${user}    |    11:01 AM    |    June 9</span>
                       </div>
                   </div>
               </div>`);
    }

    $div.appendTo($("#msg_history"));

    $("#msg_history").scrollTop($("#msg_history")[0].scrollHeight);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (e) {
    $('<div id = "newElement">A Computer Science portal for geeks</div>');
    e.preventDefault();
    var message = document.getElementById("message").value;
    connection.invoke("SendMessageToAll", message, userName).catch(function (err) {
        return console.error(err.toString());
    });

    $("#message").val('');
});