$( function() {
    //Create posts
    $("div.post div.post-content").each(function () {
        SetPostData(this);
    });

    AddBindings();

})