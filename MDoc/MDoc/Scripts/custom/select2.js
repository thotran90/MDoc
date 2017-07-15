$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};

loadAllSelect2();

function loadAllSelect2() {
    $('.select2-ajax').not('.select2-container').not('.select2-offscreen').each(function () {
        initSelect2Autocomplete($(this));
    });
    $('.toSelect2').not('.select2-ajax').not('.select2-container').not('.select2-offscreen').each(function () {
        initSelect2Autocomplete($(this));
    });
}

function initSelect2Autocomplete(elt) {
    //Init default value
    if ($(elt).attr('data-default-value') != null && $(elt).attr('data-default-value') != '') {
        $(elt).val($(elt).attr('data-default-value'));
    } else if ($(elt).select(':selected') != null) {

    } else {
        $(elt).val('');
    }
    //DefaultName of return data
    var name = 'text';
    if ($(elt).data('name') != null) {
        name = $(elt).data('name');
    }

    //default values
    var options = {
        isMultiple: false,
        allowClear: true,
        minimumInputLength: 3,
        width: '220px',
        formatResult: function (element) {
            return element[name];
        },
        formatSelection: function (element) {
            return element[name];
        }
    };

    //options for Multiple select2
    if ($(elt).attr('multiple') && $(elt).attr("multiple") == 'multiple') {
        options.isMultiple = true;
        if ($(elt).attr('data-maximum-selection-size') != null) {
            options.maximumSelectionSize = parseInt($(elt).attr('data-maximum-selection-size'));
        }
        if ($(elt).attr('data-closeOnSelect') != null && $(elt).attr("data-closeOnSelect") == 'false') {
            options.closeOnSelect = false;
        }
    }
    if (options.isMultiple && $(elt).prop('tagName') !== "SELECT")
        options.multiple = 'true';

    //allowClear
    if (elt.attr('data-allowClear') != null && elt.attr('data-allowClear').toLowerCase() == 'false')
        options.allowClear = false;

    //MaximumInputLength
    if (elt.attr('data-max-length') != null && elt.attr("data-max-length") != '')
        options.maximumInputLength = parseInt(elt.attr("data-max-length"));

    //MinimumInputLength
    if (elt.attr('data-min-length') != null && elt.attr("data-min-length") != '')
        options.minimumInputLength = parseInt(elt.attr("data-min-length"));
    //Width
    if (elt.attr('data-width') != null && elt.attr("data-width") != '')
        options.width = $(elt).attr('data-width');

    //DataFunction
    var dataFunction = function (term) {
        return { 'query': term };
    };


    //CreateChoiceOnSearch
    if ($(elt).hasAttr('data-createOnSearch')) {
        options.createSearchChoice = function (term) {
            return { id: term, text: '(Create)' + term };
        }
        options.createSearchChoicePosition = 'top';
    }
    //Ajax
    // ReSharper disable once UnknownCssClass
    if ($(elt).hasClass('select2-ajax')) {


        dataFunction = function (term) {

            var data = [];
            if ($(this).hasAttr('data-dependson')) {
                var elements = $(this).data('dependson');
                $.each(elements.split(','), function (index, value) {
                    data.push({ name: value, value: $('#' + value).val() });
                });
            }
            data.push({ name: 'query', value: term });
            return $.param(data);
        };

        if ($(elt).hasAttr('data-ajax-dataFunction'))
            dataFunction = $(elt).attr('data-ajax-dataFunction');

        var url = elt.data("action");
        if (!url) {
            console.error('Query function not defined. Please provide a source.');
        }


        options.ajax = {
            url: url,
            dataType: 'json',
            data: dataFunction,
            results: function (data) {
                return { results: data };
            }
        }; //InitSelection
        var defaultActionUrl = elt.attr("data-action");
        if ($(elt).attr("data-default-action") != null && $(elt).attr("data-default-action") != "") {
            defaultActionUrl = $(elt).attr("data-default-action");
        }

        // debounce parameter
        if (elt.data('quietmillis')) {
            options.ajax.quietMillis = elt.data('quietmillis');
        }

        options.initSelection = function (element, callback) {
            var id = $(element).val();
            if (id !== "") {

                var data = [];
                if ($(element).hasAttr('data-dependson')) {
                    var elements = $(element).data('dependson');
                    $.each(elements.split(','), function (index, value) {
                        var cSharpValue = value.split('_');
                        cSharpValue = cSharpValue[cSharpValue.length - 1];
                        data.push({ name: cSharpValue, value: $('#' + value).val() });
                    });
                }
                data.push({ name: 'id', value: id });
                $.ajax(defaultActionUrl, {
                    data: data,
                    dataType: "json"
                }).success(function (data) {
                    callback(data);
                });
            } else {
                $.ajax(defaultActionUrl, {
                    dataType: "json"
                }).success(function (data) {
                    callback(data);
                });
            }
        };;
    }
    if ($(elt).hasAttr('data-onchange')) {
        $(elt).select2(options).on('change', function () {
            window[$(elt).data('onchange')]($(this));
        });
    } else {
        $(elt).select2(options);
    }
}

if ($.validator != undefined && $.validator.setDefaults != undefined) {
    $.validator.setDefaults({
        ignore: $(':hidden').not($('.select2-container:visible').next('input.toSelect2,input.select2-ajax'))
    });
}
