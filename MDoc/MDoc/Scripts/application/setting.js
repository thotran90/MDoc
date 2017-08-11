var checklist = (function() {
    var setHeightGrid = function () {
        var height = $(window).height();
        $("#GridChecklists .k-grid-content").css("height", height - 375);
    }

    var remove = function(elm) {
        var action = $(elm).data("action");
        if (action) {
            $.post(action, function (res) {
                if (res === "OK") {
                    var grid = $("#GridChecklists").data("kendoGrid");
                    grid.dataSource.fetch();
                } else {

                }
            });
        }
    }

    return {
        setHeightGrid: setHeightGrid,
        remove: remove
    }
})();