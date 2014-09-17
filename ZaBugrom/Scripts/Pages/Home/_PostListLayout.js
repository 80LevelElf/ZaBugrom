$( function() {
    $("div#filter-div select").selectmenu();
    $("div#filter-div input[type='number']").spinner();

    //Create posts
    $("div.post-content").each(function () {
        $(this).html(GetPostData($(this).html()));
    });
})