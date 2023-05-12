$(function () {
    hideLoadingSpinner();
    var data = [];
    $('#submit-button').on('click', function () {
        if ($("#companies-dropdown").val() == '') {
            $('#companies_dropdown_chosen').css('border', '2px solid');
            $('#companies_dropdown_chosen').css('border-color', '#ff0000');
            return
        }
        showLoadingSpinner();
        $.ajax({
            url: '',
            type: "POST",
            data: { 'SelectedCompanies': data }
        }).then(function (data) {
            $('#alert-container').html(data);
            hideLoadingSpinner();
        })
    });
    $('#reset-button').on('click', function () {
        window.location.reload();
    });
    var config = {
        '.chosen-select-width': { width: "75%" }
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }
    $("#companies-dropdown").change(function () {
        $('#companies_dropdown_chosen').css('border', 'none');
        data = [];
        $.each($("#companies-dropdown :selected"), function () {
            var selectedCompanies = {
                CompanyId: "",
                CompanyName: "",
                CompanyBaan: ""
            };
            selectedCompanies.CompanyBaan = $(this).attr('data-baan');
            selectedCompanies.CompanyId = $(this).attr('data-companyId');
            selectedCompanies.CompanyName = $(this).attr('data-companyName');
            data.push(selectedCompanies);
        });
    });
});