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
    $('#txtPassword').text('');
    $('#txtMonthlyBIll').text('');    
    $('.customerBillForm').show();
    $('#customerId').select2({
        ajax: {
            url: '/DishCustomer/GetCustomerListForDropdown',
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
};

function changeCustomer() {
    if ($('#customerId').val() != 0) {
        $.get('/DishCustomer/GetCustomerById/' + $('#customerId').val(), function (data) {
            $('#hostNameId').val(data.data.hostName);
            $('#hostPhoneId').val(data.data.hostPhone);
            $('#zoneNameId').val(data.data.zoneName);
            $('#txtMonthlyBIll').val(data.data.monthBill);
            $('#connectionMonthId').val(data.data.connMonth == '0' ? '' : data.data.connMonth + ',' + (data.data.connYearName == 'null' ? '' : data.data.connYearName));
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
    else {
        var inputData = {
            cid: $('#customerId').val(),
            Password: $('#txtPassword').val()           
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

        $.post('/DishCustomerDelete/DishCustomerDelete', inputData, function (data) {
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
    $('#customerId').val('0');   
    $('#customerId').trigger('change');
    $('#txtPassword').val('');
    $('#connectionMonthId').val('');
    $('#hostNameId').val('');
    $('#hostPhoneId').val('');
    $('#zoneNameId').val('');
    $('#txtMonthlyBIll').val('');

}

$('#btnClearId').click(function () {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#errorMessageBoxId').val('');
    $('#successMessageBoxId').val('');
    clearUI();
});