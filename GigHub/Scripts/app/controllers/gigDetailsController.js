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
                userId, toggleButton, fail);
        else
            followingService.deleteFollowing(
                userId, toggleButton, fail);
    };

    var toggleButton = function () {
        var text = (button.text().trim() == "Follow") ? "Following" : "Follow";

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