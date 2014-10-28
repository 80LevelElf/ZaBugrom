$( function() {
    $("div#filter-div select").selectmenu();
    $("div#filter-div input[type='number']").spinner();

    //Create posts
    $("div.post.simple-post div.post-content").each(function () {
        SetPostData(this);
    });

    AddBindings();
})