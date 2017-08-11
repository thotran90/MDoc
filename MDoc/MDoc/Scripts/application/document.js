var appDocument = (function () {
    // Set height of grid to fix screen
    var setHeightGrid = function() {
        var height = $(window).height();
        $("#GridDocuments .k-grid-content").css("height", height - 350);
    }
    // fill customer data to customer area
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
        $("#Customer_BackupMobile").val(customer.BackupMobile);
        $("#Customer_PassportNumber").val(customer.PassportNumber);
        $("#Customer_PassportValidDate").data("kendoDatePicker").value(customer.PassportValidDate);
        $("#Customer_PassportExpiredDate").data("kendoDatePicker").value(customer.PassportExpiredDate);
    }
    // load exists customer
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
    // updatd document status
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
    // add document comment
    var addComment = function() {
        var documentId = $("#DocumentId").val();
        if (documentId > 0) {
            var comment = {
                DocumentId: documentId,
                Content: $("textarea#Content").val()
            };
            comment.Content = comment.Content.replace(/\n\r?/g, '<br />');
            var action = $("#AddCommentUrl").val();
            $.ajax({
                url: action,
                data: comment,
                type: "POST"
            }).done(function (response) {
                $("textarea#Content").val("");
                $("#list-of-comment").append(response);
            }).fail(function(xhr, status, errorThrown) {
                console.log(status);
            });
        }
    }
    // update checklist
    var saveChecklist = function(elm) {
        var documentId = $("#DocumentId").val();
        if (documentId > 0) {
            var itemid = $(elm).data("id");
            var item = {
                DocumentId: documentId,
                ChecklistId: itemid,
                IsChecked: $(elm).is(':checked')
            };
            var action = $("#SaveChecklistUrl").val();
            $.ajax({
                url: action,
                data: item,
                type: "POST"
            }).done(function (response) {
                $("#checklist-item-"+itemid).html(response);
            }).fail(function (xhr, status, errorThrown) {
                console.log(status);
            });
        }
    }
    // toggle option created contract
    var toggleContract = function () {

        var isNeedContract = $("#IsNeedContract").is(':checked');
        console.log(isNeedContract);
        if (isNeedContract) {
            $("#created-contract-area").removeClass("hidden");
        } else {
            $("#IsCreatedContract").prop('checked', false);
            $("#created-contract-area").addClass("hidden");
        }
    }

    return {
        setHeightGrid: setHeightGrid,
        loadExistDocument: loadExistDocument,
        updateStatus: updateStatus,
        addComment: addComment,
        saveChecklist: saveChecklist,
        toggleContract: toggleContract
    }
})();