var GigDetailsController = function (followingService) {

    var init = function (container) {
        $(container).on("click", ".js-toggle-follow", toggleFollowing);
    };

    var toggleFollowing = function (e) {
        var button = $(e.target);
        var userId = button.attr("data-user-id");
        if (button.hasClass("btn-default"))
            followingService.createFollowing(
                userId, function () { toggleButton(button, "Following"); }, fail);
        else
            followingService.deleteFollowing(
                userId, function () { toggleButton(button, "Follow"); }, fail);
    };

    var toggleButton = function (button, text) {
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