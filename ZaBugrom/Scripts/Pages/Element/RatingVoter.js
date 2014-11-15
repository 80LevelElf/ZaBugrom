$(function() {
    AddBindings();
});

function AddBindings() {
    //For posts in post list
    $("div.post-header a.rating-up").unbind("click");
    $("div.post-header a.rating-up").bind("click", PostRatingUp);

    $("div.post-header a.rating-down").unbind("click");
    $("div.post-header a.rating-down").bind("click", PostRatingDown);

    //For post in detail view
    $("div#post-title a.rating-up").unbind("click");
    $("div#post-title a.rating-up").bind("click", DetailPostRatingUp);

    $("div#post-title a.rating-down").unbind("click");
    $("div#post-title a.rating-down").bind("click", DetailPostRatingDown);

    //For comments
    $("div.comment-title a.rating-up").unbind("click");
    $("div.comment-title a.rating-up").bind("click", CommentRatingUp);

    $("div.comment-title a.rating-down").unbind("click");
    $("div.comment-title a.rating-down").bind("click", CommentRatingDown);
}

//Post
function PostRatingUp() {
    var postId = $(this).parent().parent().parent().attr("data-id");    

    //If there is detail post
    if (postId == undefined) {
        postId = $(this).parent().parent().attr("data-id");
    }

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
                var newRating = parseInt(ratingLabel.text()) + 1;

                var upButton = ratingLabel.parent().find("a.rating-up");
                upButton.addClass("selected");

                //If user already voted
                var downButton = ratingLabel.parent().find("a.rating-down");
                if (downButton.hasClass("selected")) {
                    downButton.removeClass("selected");
                    newRating++;
                }

                ratingLabel.text(newRating);
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
                var newRating = parseInt(ratingLabel.text()) - 1;

                var downButton = ratingLabel.parent().find("a.rating-down");
                downButton.addClass("selected");

                //If user already voted
                var upButton = ratingLabel.parent().find("a.rating-up");
                if (upButton.hasClass("selected")) {
                    upButton.removeClass("selected");
                    newRating--;
                }

                ratingLabel.text(newRating);
            }
        }
    });
}

//Detail post
function DetailPostRatingUp() {
    var postId = $(this).parent().parent().attr("data-id");

    $.ajax({
        type: "POST",
        url: ActionPath("TryToVote", "Post"),
        data:
        {
            postId: postId,
            isVoteUp: true
        },
        success: function (data) {
            if (data.Result) {
                var ratingLabel = $("div#post-title[data-id='" + data.Id + "'] label.rating");
                var newRating = parseInt(ratingLabel.text()) + 1;

                var upButton = ratingLabel.parent().find("a.rating-up");
                upButton.addClass("selected");

                //If user already voted
                var downButton = ratingLabel.parent().find("a.rating-down");
                if (downButton.hasClass("selected")) {
                    downButton.removeClass("selected");
                    newRating++;
                }

                ratingLabel.text(newRating);
            }
        }
    });
}

function DetailPostRatingDown() {
    var postId = $(this).parent().parent().attr("data-id");

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
                var ratingLabel = $("div#post-title[data-id='" + data.Id + "'] label.rating");
                var newRating = parseInt(ratingLabel.text()) - 1;

                var downButton = ratingLabel.parent().find("a.rating-down");
                downButton.addClass("selected");

                //If user already voted
                var upButton = ratingLabel.parent().find("a.rating-up");
                if (upButton.hasClass("selected")) {
                    upButton.removeClass("selected");
                    newRating--;
                }

                ratingLabel.text(newRating);
            }
        }
    });
}

//Comment
function CommentRatingUp() {
    var commentId = $(this).parent().parent().parent().attr("data-id");

    $.ajax({
        type: "POST",
        url: ActionPath("TryToVote", "Comment"),
        data:
        {
            commentId: commentId,
            isVoteUp: true
        },
        success: function (data) {
            if (data.Result) {
                var ratingLabel = $("div.comment[data-id='" + data.Id + "'] label.rating");
                var newRating = parseInt(ratingLabel.text()) + 1;

                var upButton = ratingLabel.parent().find("a.rating-up");
                upButton.addClass("selected");

                //If user already voted
                var downButton = ratingLabel.parent().find("a.rating-down");
                if (downButton.hasClass("selected")) {
                    downButton.removeClass("selected");
                    newRating++;
                }

                ratingLabel.text(newRating);
            }
        }
    });
}

function CommentRatingDown() {
    var commentId = $(this).parent().parent().parent().attr("data-id");

    $.ajax({
        type: "POST",
        url: ActionPath("TryToVote", "Comment"),
        data:
        {
            commentId: commentId,
            isVoteUp: false
        },
        success: function (data) {
            if (data.Result) {
                var ratingLabel = $("div.comment[data-id='" + data.Id + "'] label.rating");
                var newRating = parseInt(ratingLabel.text()) - 1;

                var downButton = ratingLabel.parent().find("a.rating-down");
                downButton.addClass("selected");

                //If user already voted
                var upButton = ratingLabel.parent().find("a.rating-up");
                if (upButton.hasClass("selected")) {
                    upButton.removeClass("selected");
                    newRating--;
                }

                ratingLabel.text(newRating);
            }
        }
    });
}