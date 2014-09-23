var youtubePrefix = "?v=";

// Try to identify special Youtube video id from Yuotube url.
// For example: http://www.youtube.com/watch?v=i4RPuPKzp0U&list=UUSZ69a-0I1RRdNssyttBFcA -> i4RPuPKzp0U
function GetYoutubeVideoId(url) {
    var startIndex = url.indexOf(youtubePrefix);

    if (startIndex == -1)
        return null;

    var endIndex = url.indexOf('&', startIndex + youtubePrefix.length);

    if (endIndex == -1)
        endIndex = url.length;

    var count = endIndex - startIndex - youtubePrefix.length;
    return url.substr(startIndex + youtubePrefix.length, count);
}