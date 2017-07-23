var appDocument = (function() {
    var setHeightGrid = function() {
        var height = $(window).height();
        $("#GridDocuments .k-grid-content").css("height", height - 325);
    }
    var test = function() {
        alert('aa');
    }
    return {
        setHeightGrid: setHeightGrid,
        test: test
    }
})();