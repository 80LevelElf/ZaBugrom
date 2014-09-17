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
//For example: GetAtrrData("[link src="text"]", "src") = "text"
function GetAtrrData(text, attr) {
    var exp = AttrRegex(attr);
    var firstMatch = text.match(exp); //is attr="attrValue"

    if (firstMatch.length != 0) {
        var secondMatch = firstMatch[0].match(BracketRegex()); //is "attrValue"
        return secondMatch[0].replace(/["']/g, '');
    } else
        return "";
}

function ReplaceOpenItem(item) {
    //[tag] -> <tag>
    item = item.replace('[', '<').replace(']', '>');

    //[link src=""]
    if (item.indexOf("link") > -1)
        item = (item.replace("link", "a").replace("src", "href") + GetAtrrData(item, "title") + "</a>")
        .replace(AttrRegex("title"), "");

    //[video src=""]
    if (item.indexOf("video") > -1)
        item = item.replace("video", "iframe") + "</iframe>";

    //[img src=""]
    //There is no needed conversion

    //[quote] - just open tag
    if (item.indexOf("quote") > -1)
        item = item.replace("quote", "div class=\"quote\"");

    return item;
}

function ReplaceCloseItem(item) {
    //[/tag] -> </tag>
    item = item.replace('[', '<').replace(']', '>');

    //[/quote] - just close tag
    if (item.indexOf("quote") > -1)
        item = item.replace("quote", "div");

    return item;
}

//Get post markup by plain text
function GetPostData(text) {
    //To escape not provided tags
    text = EscapeHtml(text);

    //Change our tags to html tags
    text = text.replace(/\[[^\/].*?\]/g, ReplaceOpenItem).replace(/\[\/.*?\]/g, ReplaceCloseItem);

    //And create string breaking
    text = text.replace(/\n/g, "</br>");
    return text;
}