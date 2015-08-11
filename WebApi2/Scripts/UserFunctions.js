var uri = "api/user/get";

$(document).ready(function () {
    $.getJSON(uri)
        .done(function (data) {
            $.each(data, function (key, item) {
                $("<li>", { text: formatItem(item) }).appendTo($("#users"));
            });
        });
});

function refreshData() {
    $.getJSON(uri)
        .done(function (data) {
            $("#users").html("");
            $.each(data, function (key, item) {
                $("<li>", { text: formatItem(item) }).appendTo($("#users"));
            });
        });
}

function formatItem(item) {
    if (item == null) {
        return "item null";
    }

    return item.Name + " : " + item.Username;
}

function addUser() {
    var currentStyle = document.getElementById("createForm").style.display;
    if (currentStyle === "block") {
        document.getElementById("createForm").style.display = "none";
    } else {
        document.getElementById("createForm").style.display = "block";
    }
}

function displaySingleUser(user) {
    if (user == null) {
        return;
    }

    $("#userdetails").html("");

    for (var property in user) {
        if (user.hasOwnProperty(property)) {
            $("<li>", { text: property + " : " + user[property] }).appendTo($("#userdetails"));
        }
    }
}

function createUser() {
    var name = $("#newName").val();
    var userName = $("#newUsername").val();
    var password = $("#newPassword").val();

    var user = new Object();
    user.Name = name;
    user.Username = userName;
    user.Password = password;

    $.ajax({
        type: "POST",
        data: JSON.stringify(user),
        url: "api/user",
        contentType: "application/json"
    }).done(function() {
        alert("Added user!");
    });
}

function find() {
    var id = $("#userId").val();
    $.getJSON(uri + "/" + id)
        .done(function (data) {
            displaySingleUser(data);
        })
        .fail(function () {
            $("#product").text("Error");
        });
}