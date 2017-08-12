var customer = (function() {
    var setHeightGrid = function () {
        var height = $(window).height();
        $("#GridCustomers .k-grid-content").css("height", height - 300);
    }

    return {
        setHeightGrid: setHeightGrid
    }
})();