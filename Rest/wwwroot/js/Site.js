$(document).ready(function () {
    displayPosts();

    $('#Save').click(function (event) {
        event.preventDefault();
        if ($('input[name=Id]').val() === "")
            createPost();
        else
            updatePost();
    }); 
    
    $('#AddPost').click(function (event) {
        event.preventDefault();
        $('.modal-title').html("Add Post");

        $('input[name=Id]').val("");
        $('input[name=Name]').val("");
        $('textarea[name=Description]').val("");
    });
});

function openEdit(id) {
    
    $.ajax({
        url: '/posts/' + id,
        type: 'GET',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            $('#postModal').modal("show");
            $('.modal-title').html("Edit Post '" + data.name + "'");

            $('input[name=Id]').val(data.id);
            $('input[name=Name]').val(data.name);
            $('textarea[name=Description]').val(data.description);
        },
        error: function () {
            typeMessage("danger", "Your post is not found.");
            displayPosts();
        }
    });
}

function dropPost(id) {
    $.ajax({
        url: '/posts/' + id,
        type: 'DELETE',
        success: function () {
            typeMessage("warning", "Your post is successfully deleted.");
            displayPosts();
        },
        error: function () {
            typeMessage("warning", "Your post is already deleted.");
            displayPosts();
        }
    });
}

function updatePost() {
    var post = {
        Id: $('input[name=Id]').val(),
        Name: $('input[name=Name]').val(),
        Description: $('textarea[name=Description]').val()
    };

    $.ajax({
        url: '/posts',
        type: 'PUT',
        data: JSON.stringify(post),
        contentType: 'application/json;charset=utf-8',
        success: function () {
            typeMessage("info", "Your post is successfully updated.");
            displayPosts();
        },
        error: function () {
            typeMessage("danger", "Your post is not found.");
            displayPosts();
        }
    });
}

function createPost() {
    var post = {
        Name: $('input[name=Name]').val(),
        Description: $('textarea[name=Description]').val()
    };

    $.ajax({
        url: '/posts',
        type: 'POST',
        data: JSON.stringify(post),
        contentType: 'application/json;charset=utf-8',
        success: function () {
            typeMessage("success", "Your post successfully created!");
            displayPosts();
        },
        error: function () {
            typeMessage("danger", "Error occured. Plaese recreate post.");
        }
    });
}

function displayPosts() {
    $.ajax({
        url: '/posts',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('.row').html(printPosts(data));
        }
    });
}

function printPosts(posts) {
    var htmlPosts = "";
    posts.forEach(function (post) {
        htmlPosts += '<div class="col-sm-4">';
        htmlPosts += '<span class="action">';
        htmlPosts += '<a href="javascript:void(0);" onclick="openEdit(' + post.id + ')">Edit</a> | ';
        htmlPosts += '<a href="javascript:void(0);" onclick="dropPost(' + post.id + ')">Drop</a>';
        htmlPosts += '</span>';
        htmlPosts += '<h3>' + post.name + '</h3>';
        htmlPosts += '<p>' + post.description + '</p>';
        htmlPosts += '</div>';
    });
    return htmlPosts;
}

function typeMessage(type, message) {
    var htmlMessage = "<div class='alert alert-" + type + " alert-dismissable'>";
    htmlMessage += "<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>";
    htmlMessage += message + "</div>";

    $('#notice').html(htmlMessage);
}