$(function() {
    //Create posts
    $("div.post-content").each(function() {
        SetPostData(this);
    });

    //Create comments
    $("div.comment-source").each(function () {
        SetPostData(this);
    });

    var editor = $("div#editor");

    //Comment post
    $("a#reply").bind("click", function() {
        if (editor.attr("data-comment-id") == "null") {
            editor.removeClass("showed");
            editor.removeAttr("data-comment-id");
            return;
        }

        editor.attr("data-comment-id", "null");
        editor.appendTo("div#post-area");
        editor.addClass("showed");
    });

    //Comment another comment
    var commentAnotherCommentFunction = function() {
        var comment = $(this).closest(".comment");

        if (editor.attr("data-comment-id") == comment.attr("data-id")) {
            editor.removeClass("showed");
            editor.removeAttr("data-comment-id");
            return;
        }

        editor.attr("data-comment-id", comment.attr("data-id"));
        editor.insertAfter(comment);
        editor.addClass("showed");
    };

    $("div.comment-footer a.reply").bind("click", commentAnotherCommentFunction);

    //Add new comment
    $("div#editor button#send").bind("click", function() {
        var postId = ParamValueOfAddress("postId");
        var commentId = null;

        //Get comment id
        var editorCommentIdAttr = editor.attr("data-comment-id");
        if (editorCommentIdAttr != "null") {
            commentId = editorCommentIdAttr;
        }

        var source = editor.find("textarea[name='Source']").val().trim();
        source = PrepareNewPostForAdding(source);
        editor.find("textarea[name='Source']").val("");

        //Clear editor
        editor.removeClass("showed");
        editor.removeAttr("data-comment-id");

        //Add comment
        $.ajax({
            type: "POST",
            url: ActionPath("AddComment", "Post"),
            data: JSON.stringify(
            {
                PostId: postId,
                ParentCommentId: commentId,
                Source: source
            }),
            success: function(newComment) {
                //Add new comment
                newComment = $(newComment);

                if (commentId == null) {
                    //Add as first comment
                    $("div#comment-area").prepend(newComment);
                } else {
                    var parentComment = $("div.comment[data-id=" + commentId + "]");
                    var commentArea = editor.next();

                    if (commentArea.hasClass("comment-shift-area")) {
                        commentArea.prepend(newComment);
                    } else {
                        //Create comment area for parent of current comment
                        commentArea = $("<div class='comment-shift-area comment-shift-area-border'></div>");
                        parentComment.after(commentArea);

                        commentArea.append(newComment);
                    }
                }

                newComment.find("a.reply").bind("click", commentAnotherCommentFunction);
                SetPostData(newComment.find(".comment-source"));

                //Animate title of new comment to show it cool
                newComment.addClass("just-added");
                newComment.find("div.comment-title").animate(
                {
                    backgroundColor: "#f5f5f5"
                }, 1000);
            },
            error: function() {
                alert("Error in time of adding nnew comment");
            },
            contentType: 'application/json'
        });
    });
});