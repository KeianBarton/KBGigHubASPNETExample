var GigsController = function (attendanceService) {

    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);
        var gigId = button.attr("data-gig-id");
        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(
                gigId, function () { toggleButton("Going"); }, fail);
        else
            attendanceService.deleteAttendance(
                gigId, function () { toggleButton("Going ?"); }, fail);
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

}(AttendanceService);