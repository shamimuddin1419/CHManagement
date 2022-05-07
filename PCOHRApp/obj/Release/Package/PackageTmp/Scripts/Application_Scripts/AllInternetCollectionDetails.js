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
    var fromDate = $('#fromDateId input').val();
    var toDate = $('#toDateId input').val();
    var cid = $('#customerId').val();
    var collectedBy = $('#collectedById').val(); collectedBy
    var receivedBy = $('#receivedById').val();
    var ctype = $('#ctypeId').val();
    var reportType = 'InternetBillDetails';
    if (fromDate == '' || toDate == '') {
        showErrorMessage('From Date and To Date cannot be empty!!');
    }
    else {
        var viewURL = '/CableReport/ShowReport?fileType=' + fileType + '&reportType=' + reportType + '&fromDate=' + fromDate + '&toDate=' + toDate + "&cid=" + cid + '&receivedBy=' + receivedBy + '&collectedBy=' + collectedBy + '&ctype=' + ctype;

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

    $('#fromDateId').datetimepicker({
        useCurrent: false,
        format: 'DD/MM/YYYY'
    });
    $('#toDateId').datetimepicker({
        useCurrent: false,
        format: 'DD/MM/YYYY',
    });
    $('#customerId').select2({
        ajax: {
            url: '/InternetCustomer/GetCustomerListForDropdown',
            data: function (params) {
                var query = {
                    search: params.term,
                    page: params.page || 1,
                    selectedId: 0
                }
                return query
            }
        }
    });
    $.get('/Account/GetUserDropdownList', function (data) {
        users = data.data;
        $('#collectedById').select2({
            data: users
        });
        $('#receivedById').select2({
            data: users
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