var uri = "api/user/";
google.load("visualization", "1.1", { packages: ["table"] });
google.setOnLoadCallback(load);

$(document).ready(load);

function load() {
    hideAddUserSection();

    $.getJSON(uri)
        .done(function (dataTable) {
            drawTable(dataTable);
    });
}

function hideAddUserSection() {
    document.getElementById("createForm").style.display = "none";
}

function drawTable(dataTable) {
    var data = new window.google.visualization.DataTable();
    data.addColumn("number", "Id");
    data.addColumn("string", "Name");
    data.addColumn("string", "Username");
    for (var index = 0; index < dataTable.length; index++) {
        var arr = $.map(dataTable[index], function (el) { return el; });
        data.addRow([arr[0], arr[1], arr[2]]);
    }

    var tableDiv = document.getElementById('table_div');
    
    if (tableDiv == null) {
        return;
    }

    var table = new google.visualization.Table(document.getElementById('table_div'));
    table.draw(data, { showRowNumber: true, width: '100%', height: '100%' });
}

function refreshData() {
    load();
}

function formatItem(item) {
    if (item == null) {
        return "item null";
    }

    return item.Id + " : " + item.Name + " : " + item.Username;
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
        url: uri,
        contentType: "application/json"
    }).done(function() {
        alert("Added user!");
    });
}

function deleteUser() {
    var id = $("#userId").val();

    $.ajax({
        type: "DELETE",
        url: uri + "/" + id,
        contentType: "application/json"
    }).done(function () {
        alert("Deleted user with id: " + id + " !");
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