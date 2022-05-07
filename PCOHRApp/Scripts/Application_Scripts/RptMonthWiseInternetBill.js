$(document).ready(function () {
    loadInitialization();
});
$('#fromDateId #toDateId').on('changeDate', function (ev) {
    $(this).datepicker('hide');
});

$('#btnSaveId').click(function () {
    GenerateRDLC("PDF");
});
$('#btnExcelId').click(function () {
    GenerateRDLC('Excel');
});
$('#btnWordId').click(function () {
    GenerateRDLC('Word');
});


function GenerateRDLC(fileType) {
    var month = $('#monthId').val();
    var year = $('#yearId').val();

    var reportType = 'MonthWiseInternetBillGenerate';
    if (month == '0' || year == '0') {
        showErrorMessage('Provide Month and Year!!');
    }
    else {
        var viewURL = '/CableReport/ShowReport?fileType=' + fileType + '&reportType=' + reportType + '&month=' + month + '&year=' + year;

        $.fancybox(
            {
                'title': 'Report Window',
                'type': 'iframe',
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'speedIn': 1000,
                'speedOut': 700,
                autoSize: false,
                closeClick: false,
                afterShow: after_show(fileType),
                'href': viewURL
            });
        $('#messageBoxId').hide();
    }
}
function after_show(fileType) {
    if (fileType != "PDF") {
        setTimeout(function () { $.fancybox.close(); }, 19000);
    }
}

function loadInitialization() {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();

    var years = [];
    $.get('/Dropdown/GetYearList', function (data) {
        years = data.data;
        $('#yearId').select2({
            data: years
        });
    });
}
function showErrorMessage(errorText) {
    $('#messageBoxId').show();
    $('#errorMessageBoxId').show();
    $('#successMessageBoxId').hide();
    $('#errorMessageBoxId').html(errorText);
    $('#messageBoxId').focus();
}
function showSuccessMessage(successText) {
    $('#messageBoxId').show();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').show();
    $('#successMessageBoxId').html(successText);
    $('#messageBoxId').focus();
}