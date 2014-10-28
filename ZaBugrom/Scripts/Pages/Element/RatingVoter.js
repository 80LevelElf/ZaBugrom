function AddBindings() {
    $("div.post-header a.rating-up").unbind("click");
    $("div.post-header a.rating-up").bind("click", PostRatingUp);

    $("div.post-header a.rating-down").unbind("click");
    $("div.post-header a.rating-down").bind("click", PostRatingDown);
}

function PostRatingUp() {
    var postId = $(this).parent().parent().parent().attr("data-id");

    $.ajax({
        type: "POST",
        url: ActionPath("TryToVote", "Post"),
        data:
        {
            postId : postId,
            isVoteUp : true
        },
        success: function (data) {
            if (data.Result) {
                var ratingLabel = $("div.post[data-id='" + data.Id + "'] label.rating");
                ratingLabel.text(parseInt(ratingLabel.text()) + 1);
            }
        }
    });
}

function PostRatingDown() {
    var postId = $(this).parent().parent().parent().attr("data-id");

    $.ajax({
        type: "POST",
        url: ActionPath("TryToVote", "Post"),
        data:
        {
            postId: postId,
            isVoteUp: false
        },
        success: function (data) {
            if (data.Result) {
                var ratingLabel = $("div.post[data-id='" + data.Id + "'] label.rating");
                ratingLabel.text(parseInt(ratingLabel.text()) - 1);
            }
        }
    });
}