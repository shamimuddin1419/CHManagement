$(document).ready(function () {
    loadInitialization();
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

$("input[name='inlineRadioOptions']").click(function () {
    $('#customerId').val('0');
    loadInitialization();
});
function loadInitialization() {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#customerId').select2({
        ajax: {
            url: '/DishCustomer/GetCustomerListForDropdown?searchBy=' + $("input[name='inlineRadioOptions']:checked").val(),
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

function GenerateRDLC(fileType) {
    var cid = $('#customerId').val();
    var reportType = 'DishBillCard';
    if (cid == '0') {
        showErrorMessage('Select Customer!!');
    }
    else {
        var viewURL = '/CableReport/ShowReport?fileType=' + fileType + '&reportType=' + reportType + '&fromDate=' + "&cid=" + cid;

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