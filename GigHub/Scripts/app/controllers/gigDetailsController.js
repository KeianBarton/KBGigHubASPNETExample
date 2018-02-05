var GigDetailsController = function (followingService) {

    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-follow", toggleFollowing);
    };

    var toggleFollowing = function (e) {
        button = $(e.target);
        var userId = button.attr("data-user-id");
        if (button.hasClass("btn-default"))
            followingService.createFollowing(
                userId, function () { toggleButton("Following"); }, fail);
        else
            followingService.deleteFollowing(
                userId, function () { toggleButton("Follow"); }, fail);
    };

    var toggleButton = function (text) {
        button
            .toggleClass("btn-info")
            .toggleClass("btn-default")
            .text(text);
    };

    var fail = function () {
        alert("Something failed!");
    };

    return {
        init: init
    };

}(FollowingService);