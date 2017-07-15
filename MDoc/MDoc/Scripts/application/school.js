var school = (function() {

    var setHeightGrid = function() {
        var height = $(window).height();
        $("#GridSchools .k-grid-content").css("height",height - 325);
    }

    return {
        setHeightGrid: setHeightGrid
    }
})();