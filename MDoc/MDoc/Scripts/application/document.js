var appDocument = (function() {
    var setHeightGrid = function() {
        var height = $(window).height();
        $("#GridDocuments .k-grid-content").css("height", height - 325);
    }
    
    var fillCustomerInformation = function(customer) {
        $("#Customer_GenderId").select2("val", customer.GenderId);
    }

    var loadExistDocument = function () {
        if (window.whatever === false) return;
        var customerId = $("#s2id_CustomerId").select2("val");
        if (customerId > 0) {
            window.whatever = false;
            var action = $("#GetCustomerInformationUrl").val();
            action += "\\" + customerId;
            $.get(action, function(response) {
                fillCustomerInformation(response);
                window.whatever = true;
            });
        }
    }

    return {
        setHeightGrid: setHeightGrid,
        loadExistDocument: loadExistDocument
    }
})();