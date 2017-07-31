var appUser = (function() {
    var setHeightGrid = function() {
        var height = $(window).height();
        $("#GridAccounts .k-grid-content").css("height", height - 325);
    }

    return {
        setHrightGrid: setHeightGrid
    }
})();