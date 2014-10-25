var bracketsRegexText = "['\"].*?['\"]";

//Return regex object to be matching with specified attribute.
//For example: AttrRegex("title") = new Regexp("regex to find this attribute with value")
function AttrRegex(attr) {
    return new RegExp(attr + "=" + bracketsRegexText);
}

function BracketRegex() {
    return new RegExp(bracketsRegexText);
}

//Get plain text instead of html
function EscapeHtml(text) {
    var map = {
        '<': '&lt;',
        '>': '&gt;'
    };

    return text.replace(/[<>]/, function (m) { return map[m]; });
}

//Return data of item with some attribute
//For example: GetAtrrData("[link src="text"]", "src") = text
function GetAtrrData(text, attr) {
    var exp = AttrRegex(attr);
    var firstMatch = text.match(exp); //is attr="attrValue"

    if (firstMatch.length != 0) {
        var secondMatch = firstMatch[0].match(BracketRegex()); //is "attrValue"
        return secondMatch[0].replace(/["']/g, '');
    } else
        return "";
}

function SetAttrData(text, attr, data) {
    return text.replace(AttrRegex(attr), attr + "=" + "'" + data + "'");
}

//<link src="" title=""> -> <a href="">title</a>
function ProceedTags(text) {

    //[link src="" title=""]
    text.find("link").each(function () {
        var currentElement = $(this);
        var title = currentElement.attr("title");
        var source = currentElement.attr("src");

        var newElement = $("<a>" + title +"</a>");
        newElement.attr("href", source);

        currentElement.replaceWith(newElement);
    });

    //[video src=""]
    text.find("video").each(function () {
        var currentElement = $(this);
        var source = currentElement.attr("src");

        var newElement = $("<iframe></iframe>");
        newElement.attr("width", "560");
        newElement.attr("height", "315");
        newElement.attr("src", "//www.youtube.com/embed/" + source);

        currentElement.replaceWith(newElement);
    });

    //[quote src=""]
    text.find("quote").each(function () {
        var currentElement = $(this);
        var html = currentElement.html();

        var newElement = $("<div></div>");
        newElement.addClass("quote");
        newElement.html(html);

        currentElement.replaceWith(newElement);
    });

    //[img src="" flow="yes/no"]
    text.find("img").each(function () {
        var currentElement = $(this);
        var html = currentElement.html();

        var src = currentElement.attr("src");
        var flow = currentElement.attr("flow");
        if (flow == undefined) {
            flow = "no";
        }

        var isFlow = (flow == "yes") ? true : false;

        var newElement = $("<img>");
        newElement.attr("src", src);

        if (isFlow) {
            newElement.addClass("flow");
        }

        newElement.html(html);

        currentElement.replaceWith(newElement);
    });
}

//Get post markup by plain text
function SetPostData(postContent) {
    postContent = $(postContent);

    //Remove extra spaces
    var postText = postContent.html();
    postText = $.trim(postText);

    //To escape not provided tags
    postText = EscapeHtml(postText);

    //[tag] -> <tag>
    postText = postText.replace(/\[/g, '<').replace(/\]/g, '>');

    //And create string breaking
    postText = postText.replace(/\n/g, "</br>");

    postContent.html(postText);

    ProceedTags(postContent);

    return postContent;
}

//When user want to add his post we need to use function to prepare data for it. For example to get youtube video id
//instead of full link to video.
function PrepareNewPostForAdding(text) {
    //Remove extra spaces
    text = $.trim(text);

    //Get youtube video id instead of full link
    text = text.replace(/\[[^\/].*?\]/g, function(item) {
        if (item.indexOf("video") > -1) {
            var attrData = GetYoutubeVideoId(GetAtrrData(item, "src"));
            item = SetAttrData(item, "src", attrData);
        }

        return item;
    });

    return text;
}