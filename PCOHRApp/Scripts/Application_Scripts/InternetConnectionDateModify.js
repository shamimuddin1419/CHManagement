$(document).ready(function () {
    loadInitialization();
});
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
function loadInitialization() {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
    $('#txtPassword').text('');
    $('.customerBillForm').show();
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

    var connYears = [];
    $.get('/Dropdown/GetYearList', function (data) {
        connYears = data.data;
        $('#connYearId').select2({
            data: connYears
        });
    });
};

function changeCustomer() {
    if ($('#customerId').val() != 0) {
        $.get('/InternetCustomer/GetCustomerById/' + $('#customerId').val(), function (data) {
            $('#hostNameId').val(data.data.hostName);
            $('#hostPhoneId').val(data.data.hostPhone);
            $('#zoneNameId').val(data.data.zoneName);
            $('#connectionMonthId').val(data.data.connMonth == '0' ? '' : data.data.connMonth + ',' + (data.data.connYearName == 'null' ? '' : data.data.connYearName));
            $('#connFeeId').val(data.data.connFee);
            $('#monthBillId').val(data.data.monthBill);
            $('#connMonthId').val(data.data.connMonth);
            $('#connYearId').val(data.data.connYear);
            $('#connYearId').trigger('change');
            $('#connMonthId').trigger('change');
        });

    }
}

$('#btnSaveId').click(function () {
    debugger;
    if ($('#customerId').val() == '0') {
        toastr.error("Please Select Customer!!");
    }
    else if ($('#txtPassword').val() == '') {
        toastr.error('Provide Password!!');
    }
    else if ($('#connMonthId').val() == '0') {
        toastr.error('Provide Connection Month!!');
    }
    else if ($('#connYearId').val() == '0') {
        toastr.error('Provide Connection Month!!');
    }
    else {
        var inputData = {
            customerId: $('#customerId').val(),
            Password: $('#txtPassword').val(),
            connMonth: $('#connMonthId').val(),
            connYear: $('#connYearId').val(),
            connFee: $('#connFeeId').val(),
            monthBill: $('#monthBillId').val()
        };
        $.blockUI({
            message: '<div class="ft-refresh-cw icon-spin font-medium-2"></div>',
            overlayCSS: {
                backgroundColor: '#FFF',
                opacity: 0.8,
                cursor: 'wait'
            },
            css: {
                border: 0,
                padding: 0,
                backgroundColor: 'transparent'
            }
        });

        $.post('/InternetConnectionDateModify/InternetConnectionDateUpdate', inputData, function (data) {
            if (data.success == true) {
                if (data.message == "Please input correct Password.") {
                    showErrorMessage(data.message);
                } else {
                    showSuccessMessage(data.message);
                    clearUI();
                }
            }
            else if (data.success == false) {
                showErrorMessage(data.message);
            }
            else {
                showErrorMessage("Something error occured, Refresh the page and try again!");
            }
            $.unblockUI();
        }).fail(function (response) {
            showErrorMessage(response.responseText);
            $.unblockUI();
        });

    }
});

function clearUI() {
    debugger;
    $('#customerId').val('0');
    $('#customerId').trigger('change');
    $('#txtPassword').val('');
    $('#connectionMonthId').val('');
    $('#hostNameId').val('');
    $('#hostPhoneId').val('');
    $('#zoneNameId').val('');

    $('#connMonthId').val(0);
    $('#connYearId').val(0);
    $('#connYearId').trigger('change');
    $('#connMonthId').trigger('change');

    $('#connFeeId').val('');
    $('#monthBillId').val('');
}

$('#btnClearId').click(function () {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#errorMessageBoxId').val('');
    $('#successMessageBoxId').val('');
    clearUI();
});