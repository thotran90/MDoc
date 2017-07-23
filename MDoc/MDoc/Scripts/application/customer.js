var customer = (function() {
    var setHeightGrid = function () {
        var height = $(window).height();
        $("#GridCustomers .k-grid-content").css("height", height - 325);
    }

    return {
        setHeightGrid: setHeightGrid
    }
})();