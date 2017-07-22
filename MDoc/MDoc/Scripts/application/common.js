var common = (function() {
    
    var loadModal = function () {
        // Fill modal with content from link href
        $(".modal").on("show.bs.modal", function (e) {

            var link = $(e.relatedTarget);
            //$(this).find(".modal-content").load(link.attr("href"));
            $(this).find(".modal-content").load(link.attr("href"), function (responseText, textStatus, xmlhttpRequest) {
                if (textStatus === "success") {
                    jQuery.validator.unobtrusive.parse(link.attr("data-target"));
                } else if (textStatus === "error") {
                    common.showError(responseText);
                }
            });
        });
    }

    var closeModal = function () {
        $("#modal").modal('toggle');
    }

    var initTinymce = function (ele) {
        //default values
        var options = {
            isShowMenuBar: false,
            isShowToolbar: false,
            width: "100%",
            height: 400,
            plugins: [
                'advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker',
                'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
                'save table contextmenu directionality emoticons template paste textcolor'
            ],
            theme: "modern",
            menubar: 'file edit view',
            toolbar: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor emoticons'
        };

        //show menu
        if (ele.attr("data-showmenubar") != null && ele.attr("data-showmenubar").toLowerCase() === "true")
            options.isShowMenuBar = true;
        // menu bar
        if (ele.attr("data-menubar") != null && ele.attr("data-menubar") !== "")
            options.menubar = ele.attr("data-menubar");
        //show toolbar
        if (ele.attr("data-showtoolbar") != null && ele.attr("data-showtoolbar").toLowerCase() === "true")
            options.isShowToolbar = true;
        // toolbar
        if (ele.attr("data-toolbar") != null && ele.attr("data-toolbar") !== "")
            options.toolbar = ele.attr("data-toolbar");
        // theme
        if (ele.attr("data-theme") != null && ele.attr("data-theme") !== "")
            options.theme = ele.attr("data-theme");
        // plugin
        if (ele.attr("data-plugin") != null && ele.attr("data-plugin") !== "")
            options.plugins = ele.attr("data-plugin");
        // width
        if (ele.attr("data-width") != null && ele.attr("data-width") !== "")
            options.width = ele.attr("data-width");
        // height
        if (ele.attr("data-height") != null && ele.attr("data-height") !== "")
            options.height = ele.attr("data-height");

        if (options.isShowMenuBar === false) options.menubar = false;
        if (options.isShowToolbar === false) options.toolbar = false;
        console.log("opions: menubar: " + options.menubar + " toolbar: " + options.toolbar);
        tinymce.init({
            selector: '.tinymce',
            theme: options.theme,
            width: options.width,
            height: options.height,
            plugins: options.plugins,
            toolbar: options.toolbar,
            menubar: options.menubar,
            setup: function (editor) {
                editor.on('change', function () {
                    tinymce.triggerSave();
                });
            }
        });
    }

    var loadAllHtmlEditor = function () {
        $('.tinymce').each(function () {
            initTinymce($(this));
        });
    }

    var setHeightContent = function() {
         var height = $(window).height();
        $("#mainContent").css("height", height - 150);
        $("#mainContent").css("overflow", "auto");
    }

    var registerComponent = function() {
        loadModal();
        //$.validator.setDefaults({
        //    ignore: []
        //});
        loadAllSelect2();
        loadAllHtmlEditor();
        setHeightContent();
        $('[data-toggle=confirmation]').confirmation({
            rootSelector: '[data-toggle=confirmation]'
            // other options
        });
    }

    return {
        closeModal: closeModal,
        initHtmlEditor: loadAllHtmlEditor,
        registerComponent: registerComponent
    }
})();

common.registerComponent();