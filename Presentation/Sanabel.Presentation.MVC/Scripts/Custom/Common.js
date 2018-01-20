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

        var settings = $.extend({}, defaults, options);
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

                if (settings.callback)
                    settings.callback(settings.selectedValue);

            });
        });

        return this;
    };


}(jQuery));


var $util = $util || {};

$util.confirmDialogWithActionForm = function (confirmationText, title, actionUrl, confirmButtonText, cancelButtonText) {
    var html = '<div class="modal fade" id="confirmDialogWithFormModal" tabindex="-1" role="dialog" aria-labelledby="confirmDialogModalTitle" aria-hidden="true">'
        + '<div class="modal-dialog modal-danger" role= "document">'
        + ' <div class="modal-content">'
        + '   <div class="modal-header">'
        + '     <h4 class="modal-title" id="confirmDialogModalTitle">' + title + '</h4>'
        + '     <button type="button" class="close" data-dismiss="modal" aria-label="Close">'
        + '         <span aria-hidden="true">×</span>'
        + '     </button>'
        + ' </div>'
        + ' <div class="modal-body">'
        + '     <p>' + confirmationText + '</p>'
        + ' </div>'
        + ' <div class="modal-footer">'
        + '     <button type="button" class="btn btn-secondary" data-dismiss="modal">' + cancelButtonText + '</button>'
        + '     <form id="deleteForm" action="' + actionUrl + '" method="post">'
        + '         <button type="submit" class="btn btn-danger">' + confirmButtonText + '</button>'
        + '     </form>'
        + ' </div> '
        + ' </div> '
        + '   </div >'
        + ' </div >';

    $(html).appendTo('body');
    $('#confirmDialogWithFormModal').modal('show');
    $('#confirmDialogWithFormModal').on("hidden.bs.modal", function (e) {
        $('#confirmDialogWithFormModal').modal('dispose');
        $('body #confirmDialogWithFormModal').remove();
    });

    $('#confirmDialogWithFormModal btn-secondary').click(function () {
        $('#confirmDialogWithFormModal').modal('hide');
    });
};

$util.populatePlaces = function (placeBaseUrl, countryElment, regionElement, cityElement, districtElement) {

    $(countryElment).fillDropDownList({
        url: placeBaseUrl + '/Countries',
        dataValeuProperty: 'countryId',
        dataTextProperty: 'countryName',
        selectedValue: '@Model.CountryId',
        callback: fillRegions
    }).change(function () {
        fillRegions($(this).val());
    });

    $(regionElement).change(function () {
        fillCities($(regionElement), $(this).val());
    });

    $(cityElement).change(function () {
        fillDistricts(cityElement, $(this).val());
    });

};


function fillRegions(countryId, regionElement) {
    $(regionElement).fillDropDownList({
        url: '@Url.Content("~/api/Settings/Places/CountryRegions")?countryId=' + countryId,
        dataValeuProperty: 'regionId',
        dataTextProperty: 'regionName',
        selectedValue: '@Model.RegionId',
        callback: fillCities
    });
}

function fillCities(regionId, cityElement) {
    $(cityElement).fillDropDownList({
        url: '@Url.Content("~/api/Settings/Places/RegionCities")?regionId=' + regionId,
        dataValeuProperty: 'cityId',
        dataTextProperty: 'cityName',
        selectedValue: '@Model.CityId',
        callback: fillDistricts
    });
}

function fillDistricts(cityId) {
    $('#DistrictId').fillDropDownList({
        url: '@Url.Content("~/api/Settings/Places/CityDistricts")?cityId=' + cityId,
        dataValeuProperty: 'districtId',
        dataTextProperty: 'districtName',
        selectedValue: '@Model.DistrictId',
    });
}