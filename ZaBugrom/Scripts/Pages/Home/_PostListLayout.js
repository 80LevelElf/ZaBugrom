$( function() {
    $("div#filter-div select").selectmenu(
    {
        disabled: true
    });
    $("div#filter-div input[type='number']").spinner(
    {
        disabled: true
    });

    //Create posts
    $("div.post div.post-content").each(function () {
        SetPostData(this);
    });

    AddBindings();

})