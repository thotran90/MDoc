var notice = (function() {
    var setHeightGrid = function () {
        var height = $(window).height();
        $("#GridNotices .k-grid-content").css("height", height - 325);
    }

    return {
        setHeightGrid: setHeightGrid
    }
})();