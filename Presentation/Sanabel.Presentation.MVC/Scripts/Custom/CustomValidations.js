/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
(function ($) {
    $.validator.unobtrusive.adapters.add("collectionlengthvalidation", ["minimumlength", "maximumlength"], function (options) {
        options.rules['collectionlengthvalidation'] = {
            minimumlength: options.params.minimumlength,
            maximumlength: options.params.maximumlength
        };
        options.messages['collectionlengthvalidation'] = options.message;
    });

    $.validator.addMethod("collectionlengthvalidation", function (value, element, params) {
        var currentElement = $(element);
        if (currentElement.is('input[type="checkbox"]')) {
            var parentDiv = currentElement.parent().parent();
            var checkedElments = parentDiv.find('input:checked');
            if (checkedElments.length < params.minimumlength)
                return false;
            if (params.maximumlength > 0 && checkedElments.length > params.maximumlength)
                return false;
        }
        else if (currentElement.is('select[multiple]'))
        {
            var selectedItems = currentElement.val();
            if (selectedItems instanceof Array)
            {
                if (selectedItems.length < params.minimumlength)
                    return false;
                if (params.maximumlength > 0 && selectedItems.length > params.maximumlength)
                    return false;
            }
        }
        return true;
    });


}(jQuery));