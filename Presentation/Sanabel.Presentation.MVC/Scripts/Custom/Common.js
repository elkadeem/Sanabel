/// <reference path="../jquery-3.2.1.js" />

(function ($) {
    $.fn.fillDropDownList = function (options) {
        var defaults = {
            url: "",
            dataValeuProperty: "",
            dataTextProperty: "",
            keepFirstOption: true,
            selectedValue: "",
            callback: function () { }
        };

        var settings = $.extend(true, defaults, options);
        var elements = this;
        $.get(settings.url).done(function (data) {
            elements.each(function () {
                var element = $(this);
                if (settings.keepFirstOption) {
                    element.find(':first-child ~ option').remove();
                }

                if (data) {
                    $.each(data, function (index, item) {
                        $('<option/>', {
                            'value': item[settings.dataValeuProperty],
                            text: item[settings.dataTextProperty],
                        }).appendTo(element);
                    });
                }

                if (element.find('option[value="' + settings.selectedValue + '"]').length > 0) {
                    element.val(settings.selectedValue);
                }

                settings.callback.call(settings.selectedValue);

            });
        });

        return this;
    };
}(jQuery));