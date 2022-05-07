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
    $('#totalDueId').text('');
    $('#txtPassword').text('');
    $('#txtMonthlyBIll').text('');
    $('#totalDeleteAmtId').text('');
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
};

function changeCustomer() {
    if ($('#customerId').val() != 0) {
        $.get('/InternetCustomer/GetCustomerById/' + $('#customerId').val(), function (data) {
            $('#hostNameId').val(data.data.hostName);
            $('#hostPhoneId').val(data.data.hostPhone);
            $('#zoneNameId').val(data.data.zoneName);
            $('#txtMonthlyBIll').val(data.data.monthBill);
            $('#connectionMonthId').val(data.data.connMonth == '0' ? '' : data.data.connMonth + ',' + (data.data.connYearName == 'null' ? '' : data.data.connYearName));
        });

        $.get('/InternetBillCollection/GetPreviousInfoList/' + $('#customerId').val(), function (data) {
            $('#monthlyDueListPanel').empty();
            var totalDue = 0;
            $.each(data.data, function (k, v) {
                totalDue = totalDue + v.transactionAmount
            });
            $('#totalDueId').text(totalDue);

            var tableTag = "";
            tableTag = tableTag + '<table class="table" style="background-color: #072428 !important;color:white !important"><tbody>';
            if (data.data2 != null) {
                tableTag = tableTag + '<tr><td>' + data.data2.month + ',' + data.data2.yearName + ' | ' + data.data2.receivedDateString + ' | ' + data.data2.receivedByString + ' | ' + 'SL-' + data.data2.customerSL + '/' + data.data2.pageNo + ' | PAID' + '</td></tr>'
            }
            if (data.data1 != null && data.data1.length > 0) {

                $.each(data.data1, function (k, v) {
                    tableTag = tableTag + '<tr><td><input type= "checkbox" onClick="ChangeToMonth()" class="payCheck" id="' + v.id + '" />  ' + v.text + '</td></tr>';
                });
                tableTag = tableTag + '</tbody></table>';
            }
            $('#monthlyDueListPanel').append(tableTag);



        });
    }
}
function ChangeToMonth() {
    var billDetails = [];
    $('.payCheck:checkbox:checked').each(function () {
        billDetails.push($(this).attr("id"));
    })

    var billLen = billDetails.length;
    if (billLen > 0) {
        var inputData = {
            billDetailsIds: billDetails
        };
        $.ajax({
            url: '/InternetBillCollection/GetMonthlyBillByDetailIds',
            type: 'POST',
            data: JSON.stringify({ _obj: inputData }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.success == true) {
                    $('#totalDeleteAmtId').text(data.data);
                }
                else {
                    showErrorMessage(data.message);
                }
            },
            error: function (request) {
                // ...
            }
        });

    } else {
        $('#totalDeleteAmtId').text(0);
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
        var billDetails = [];
        $('.payCheck:checkbox:checked').each(function () {
            billDetails.push($(this).attr("id"));
        })

        var billLen = billDetails.length;
        if (billLen > 0) {
            var inputData = {
                cid: $('#customerId').val(),
                password: $('#txtPassword').val(),
                billDetailsIds: billDetails
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

            $.ajax({
                url: '/InternetMonthlyBillDelete/InternetMonthlyBillDelete',
                type: 'POST',
                data: JSON.stringify({ _obj: inputData }),
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
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
                },
                error: function (request) {
                    // ...
                }
            });
        }
        else {
            showErrorMessage("Please select months");
        }
    }
});

function clearUI() {
    $('#customerId').val('0');
    $('#monthlyDueListPanel').empty();
    $('#totalDueId').text('');
    $('#totalDeleteAmtId').text('');
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