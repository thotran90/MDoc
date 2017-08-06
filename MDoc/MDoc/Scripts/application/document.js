var appDocument = (function() {
    var setHeightGrid = function() {
        var height = $(window).height();
        $("#GridDocuments .k-grid-content").css("height", height - 325);
    }
    
    var fillCustomerInformation = function(customer) {
        $("#Customer_GenderId").select2("val", customer.GenderId);
        $("#Customer_LastName").val(customer.LastName);
        $("#Customer_FirstName").val(customer.FirstName);
        $("#Customer_Email").val(customer.Email);
        $("#Customer_Mobile").val(customer.Mobile);
        $("#Customer_Address").val(customer.Address);
        $("#Customer_IdentityCardNo").val(customer.IdentityCardNo);
        $("#Customer_CountryId").select2("val", customer.CountryId);
        $("#Customer_ProvinceId").select2("val", customer.ProvinceId);
        $("#Customer_DistrictId").select2("val", customer.DistrictId);
        $("#Customer_WardId").select2("val", customer.WardId);
        $("#Customer_NationalityId").select2("val", customer.NationalityId);
        $("#Customer_IdentityCardPlaceId").select2("val", customer.IdentityCardPlaceId);
        $("#Customer_DOB").data("kendoDatePicker").value(customer.DOB);
        $("#Customer_IdentityCardDateValid").data("kendoDatePicker").value(customer.IdentityCardDateValid);
        $("#Customer_IdentityCardDateExpired").data("kendoDatePicker").value(customer.IdentityCardDateExpired);
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

    var updateStatus = function (elm) {
        var documentId = $(elm).data("id");
        var statusId = $(elm).data("status");
        if (documentId && statusId) {
            var action = $("#UpdateStatusUrl").val();
            action += "?documentId=" + documentId + "&statusId=" + statusId;
            $.post(action, function(res) {
                if (res === "OK") {
                    var grid = $("#GridDocuments").data("kendoGrid");
                    grid.dataSource.fetch();
                } else {
                    
                }
            });
        }
    }

    return {
        setHeightGrid: setHeightGrid,
        loadExistDocument: loadExistDocument,
        updateStatus: updateStatus
    }
})();