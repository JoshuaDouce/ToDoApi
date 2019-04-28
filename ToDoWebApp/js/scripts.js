var toDoItems;

$(document).ready(function () {
    getAllItems(updateToDoItemsCallback);
});

function getAllItems(callback) {
    $.ajax({
        type: "GET",
        url: "https://localhost:44305/api/ToDoItem",
        cache: false,
        success: callback
    });
}

function updateToDoItemsCallback(result) {
    toDoItems = result;

    clearList()

    result.value.forEach(element => {
        appendToDoItem(element)
    });

    updateEventListeners();
}

function appendToDoItem(todoitem) {
    var id = todoitem.href.split('/')
    id = id[id.length - 1]

    var $row = $("<div/>")
        .attr("id", "todoitem" + id)
        .addClass("row")
        .addClass("todo-item-row");

    var checked = "";

    if (todoitem.isComplete) {
        checked = "checked"
    }

    var $col = $("<div/>")
        .addClass("col-md")
        .html("<input type=\"checkbox\" class=\"iscomplete-checkbox\"" + checked + ">" +
            "<label>" + todoitem.name + "</label>" +
            "<button type=\"button\" class=\"btn btn-danger item-delete\">Delete</button>" +
            "<button type=\"button\" class=\"btn btn-primary item-edit\">Edit</button>");

    $row.append($col);

    $("#list-container").append($row);
}

function deleteItem(resourceLink) {
    $.ajax({
        type: "DELETE",
        url: resourceLink,
        cache: false,
        success: function(){
            getAllItems(updateToDoItemsCallback)
        }
    });
}

function updateItem(item){
    $.ajax({
        type: "PUT",
        url: item.href,
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        cache: false,
    }); 
}

function addItem(item){
    $.ajax({
        type: "POST",
        url: "https://localhost:44305/api/ToDoItem",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        cache: false,
        success: function(){
            getAllItems(updateToDoItemsCallback)
        }
    });
}

function updateEventListeners() {
    var itemDeleteButtons = document.getElementsByClassName("item-delete");

    for (let index = 0; index < itemDeleteButtons.length; index++) {
        const element = itemDeleteButtons[index];
        element.addEventListener("click", function () {
            var id = element.parentNode.parentNode.id;
            var subString = id.substr(8);
            var idNo = parseInt(findItemIndex(subString));
            deleteItem(toDoItems.value[idNo].href);
            $("#" + id).remove();
        });
    };

    var checkboxes = document.getElementsByClassName("iscomplete-checkbox");

    for (let index = 0; index < checkboxes.length; index++) {
        const element = checkboxes[index];
        element.addEventListener("click", function () {
            var id = element.parentNode.parentNode.id;
            var subString = id.substr(8);
            var idNo = parseInt(findItemIndex(subString));
            item = toDoItems.value[idNo];

            item.isComplete = element.checked;
            
            updateItem(item);
        });
    };

    var itemEditButtons = document.getElementsByClassName("item-edit");

    for (let index = 0; index < itemEditButtons.length; index++) {
        const element = itemEditButtons[index];
        element.addEventListener("click", function () {
            alert("Item edit clicked")
        });
    };
}

function findItemIndex(idNo){
    var index = undefined;

    for (let i = 0; i < toDoItems.value.length; i++) {
        const element = toDoItems.value[i];
        
        var hrefSplit = element.href.split('/')

        if(hrefSplit[hrefSplit.length - 1] == idNo){
            index = i;
            break;
        }
    }

    return index
}

function clearList(){
    $( ".todo-item-row" ).remove();
}

var addButton = document.getElementById("modal-add-item-btn");

addButton.addEventListener("click", function () {
    var element = $("#new-todo-item");

    var item = {
        name: element[0].value,
        isComplete: false
    };

    addItem(item);
});

var clearButton = document.getElementById("clear-btn");

clearButton.addEventListener("click", function () {
    toDoItems.value.forEach(element => {
        deleteItem(element.href)
    });

    clearList();
});