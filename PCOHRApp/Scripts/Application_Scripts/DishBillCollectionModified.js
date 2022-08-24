$(document).ready(function () {
    loadInitialization();
    HideFeeFields();
});
$('#collectionDateId').on('changeDate', function (ev) {
    $(this).datepicker('hide');
});
$('#btnCreateId').click(function () {
    loadInitialization();
    clearUI();
});
$('#btnClearId').click(function () {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#errorMessageBoxId').val('');
    $('#successMessageBoxId').val('');
    clearUI();
});

$('#btnListId').click(function () {
    $('#customerBillListId').show();
    $('#messageBoxId').hide();
    $('.customerBillForm').hide();
    $('#btnClearId').hide();
    $('#btnSaveId').hide();
    $('#btnCreateId').show();
    $('#btnSearchId').show();

    $('#btnListId').hide();
    LoadBillDatatable();

});
$('#toMonthId').change(function () {
    $.get('DishBillCollection/GetMonthlyBill?cid=' + $('#customerId').val() + "&fromMonthYear=" + $('#fromMonthId').val() + "&toMonthYear=" + $('#toMonthId').val(), function (data) {
        alert($('#fromMonthId').val());
        if (data.success == true) {
            $('#monFeeId').val(data.data);
            calculateAmount();
        }
        else {
            showErrorMessage(data.message);
        }
    })
})


$('#btnSaveId').click(function () {
    if ($('#collectedById').val() == '0') {
        showErrorMessage("Provide Collected By!");
    }
    else if ($('#receiveAmountId').val() == '') {
        showErrorMessage("Received Amount cannot be empty!");
    }
    else if ($('#collectionDateId input').val() == '') {
        showErrorMessage("Provide Collection Date!");
    }
    //else if ($('#pageNoId').val() == '') {
    //    showErrorMessage("Provide Page No!");
    //}
    //else if ($('#SlNoId').val() == '') {
    //    showErrorMessage("Provide Serial No!");
    //}
    //else if ($('#receivedById').val() == '0') {
    //    showErrorMessage("Provide Received By!");
    //}
    else {
        var months = [];
        $('.payCheck:checkbox:checked').each(function () {
            months.push($(this).attr("id"));
        })
    
        var monthLen = months.length;
       
        var inputData = {
            cid: $('#customerId').val(),
            connFee: $('#connFeeId').val(),
            reConnFee: $('#reConnFeeId').val(),
            othersAmount: $('#otherFeeId').val(),
            monthlyFee: $('#monFeeId').val(),
            fromMonth: monthLen > 0 ? months[0] : "",
            toMonth: monthLen > 0 ? months[monthLen-1] : "",
            shiftingCharge: $('#shiftingFeeId').val(),
            descriptionId: $('#descriptionId').val(),
            netAmount: $('#netAmountId').val(),
            rcvAmount: $('#receiveAmountId').val(),
            adjustAdvance: $('#advanceId').val(),
            discount : $('#discountId').val(),
            customerSL: $('#SlNoId').val(),
            pageNo: $('#pageNoId').val(),
            collectedBy: $('#collectedById').val(),
            receivedBy: $('#receivedById').val(),
            collectedDateString: $('#collectionDateId input').val()
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

        $.post('/DishBillCollection/InsertBillCollection', inputData, function (data) {
            if (data.success == true) {
                showSuccessMessage(data.message);
                if ($('#btnSaveId').text().indexOf('Update') >= 0) {
                    $('#btnCreateId').click();
                    $('#messageBoxId').show();
                    $('#successMessageBoxId').show();

                }
                else
                    clearUI();

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

$("input[name='inlineRadioOptions']").click(function () {
    clearUI();
    loadInitialization();
});

$('#connFeeId,#reConnFeeId,#otherFeeId,#monFeeId,#shiftingFeeId,#discountId,#advanceId').change(function () {
    calculateAmount();
});

function clearUI() {
    $('#customerId').val('0');
    $('#connFeeId').val('0');
    $('#reConnFeeId').val('0');
    $('#otherFeeId').val('0');
    $('#monFeeId').val('0');
    $("#fromMonthId").html('<option value="0">Select Month</option>').select2();
    $("#toMonthId").html('<option value="0">Select Month</option>').select2();
    $('#fromMonthId').val('0');
    $('#toMonthId').val('0');
    $('#shiftingFeeId').val('0');
    $('#descriptionId').val('');
    $('#netAmountId').val('0');
    $('#receiveAmountId').val('0');
    $('#advanceId').val('0');
    $('#custSLId').val('');
    //$('#pageNoId').val('');
    //$('#collectedById').val('0');
    //$('#receivedById').val('0');
    $('#customerNameId').val('');
    $('#addressNameId').val('');
    $('#hostNameId').val('');
    $('#zoneNameId').val('');
    $('#assignedUserNameId').val('');
    $('#connectionMonthId').val('');
    $('#monthlyBillId').val('');
    $('#lblCusAddBillForTop').text('');
    $('#lblMonthlyBillForTop').text('');
    $('#lblEntryDateForTop').text('');
    $('#customerId').trigger('change');
    $('#fromMonthId').trigger('change');
    $('#toMonthId').trigger('change');
    //$('#collectedById').trigger('change');
    //$('#receivedById').trigger('change');
    $('#monthlyDueListPanel').empty();
    $('#connFeeDueId').text('');
    $('#reconnFeeDueId').text('');
    $('#monthlyFeeDueId').text('');
    $('#otherFeeDueId').text('');
    $('#shiftingFeeDueId').text('');
    $('#totalDueId').text('');
    $('#hostPhoneId').val('');
    $('#collectionDateId').val('');
    $('#SlNoId').val('');
    $('#discountId').val('0');
    
}
function calculateAmount() {
    var connFee = $('#connFeeId').val() == '' ? 0 : parseInt($('#connFeeId, #advanceId').val());
    var reConnFee = $('#reConnFeeId').val() == '' ? 0 : parseInt($('#reConnFeeId').val());
    var otherFee = $('#otherFeeId').val() == '' ? 0 : parseInt($('#otherFeeId').val());
    var monFee = $('#monFeeId').val() == '' ? 0 : parseInt($('#monFeeId').val());
    var shiftingFee = $('#shiftingFeeId').val() == '' ? 0 : parseInt($('#shiftingFeeId').val());
    var advanceAmount = $('#advanceId').val() == '' ? 0 : parseInt($('#advanceId').val());
    var discountAmount = $('#discountId').val() == '' ? 0 : parseInt($('#discountId').val());

    $('#netAmountId').val(connFee + reConnFee + otherFee + monFee + shiftingFee - discountAmount);
    $('#receiveAmountId').val(connFee + reConnFee + otherFee + monFee + shiftingFee - advanceAmount - discountAmount);

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
function loadInitialization() {

    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#customerBillListId').hide();
    $('#btnCreateId').hide();
    $('#btnSearchId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
    $('.customerBillForm').show();
    //var radioValue = $("input[name='inlineRadioOptions']:checked").val();
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
    var users = []
    $.get('/Account/GetUserDropdownList', function (data) {
        users = data.data;
        $('#collectedById').select2({
            data: users
        });
        
    });

    $.get('/Account/GetUserDropdownListForManagement', function (data) {
        users = data.data;
        $('#receivedById').select2({
            data: users
        });
    });

    $('#collectionDateId').datetimepicker({
        useCurrent: false,
        format: 'DD/MM/YYYY'
    });
    //var years = [];
    //$.get('/Dropdown/GetYearList', function (data) {
    //    years = data.data;
    //    $('#yearId').select2({
    //        data: years
    //    });
    //});

};
function polulateCustomerDropDown() {
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
            $('#customerNameId').val(data.data.customerName);
            $('#addressNameId').val(data.data.customerAddress);
            $('#hostNameId').val(data.data.hostName);
            $('#hostPhoneId').val(data.data.hostPhone);
            $('#zoneNameId').val(data.data.zoneName);
            $('#assignedUserNameId').val(data.data.assignedUserName);
            $('#monthlyBillId').val(data.data.monthBill);
            $('#lblCusAddBillForTop').text(data.data.customerAddress);
            $('#lblMonthlyBillForTop').text(data.data.monthBill);
            $('#lblEntryDateForTop').text(data.data.EntryDateString);
            $('#lblDisconnectionDateId').text(data.data.disconnectedDateString);


            $('#connectionMonthId').val(data.data.connMonth == '0' ? '' : data.data.connMonth + ',' + (data.data.connYearName == 'null' ? '' : data.data.connYearName));
        });
        $.get('/DishBillCollection/GetPreviousInfoList/' + $('#customerId').val(), function (data) {
            HideFeeFields();
            $('#reconnFeeDueId').text("");
            $('#connFeeDueId').text("");
            $('#otherFeeDueId').text("");
            $('#shiftingFeeDueId').text("");
            $('#monthlyFeeDueId').text("");
            $('#monthlyDueListPanel').empty();
            
            var totalMonthlyDue = 0;
            var totalDue = 0;
            $.each(data.data, function (k, v) {
                if (v.requestName === "Reconnect") {
                    $('#reconnFeeDueId').text('Reconn Fee Due : ' + v.transactionAmount);
                    $('.reConnFeeFrmId').show();
                }
                else if (v.collectionType === "Connection Fee") {
                    $('#connFeeDueId').text('Connection Fee Due : ' + v.transactionAmount);
                    $('.connFeeFrmId').show();
                    

                }
                else if (v.collectionType === "Others Fee") {
                    $('#otherFeeDueId').text('Other Fee Due : ' + v.transactionAmount);
                    $('.otherFeeFormId').show();
                }
                else if (v.requestName === "Shifting") {
                    $('#shiftingFeeDueId').text('Shifting Fee Due : ' + v.transactionAmount);
                    $('.shiftingFeeFormId').show();
                }
                else if (v.collectionType === "Monthly Bill") {
                    totalMonthlyDue = totalMonthlyDue + v.transactionAmount
                }
                totalDue = totalDue + v.transactionAmount

            });
            if (totalMonthlyDue != "0") {
                //$('#monthlyFeeDueId').text('Monthly Bill : ' + totalMonthlyDue);
                $('#monthlyFeeDueId').text('Total Month Bill : ' + totalMonthlyDue);
            }
            $('#totalDueId').text(totalDue);
            if (data.data.length > 0) {
                $('#tblHistTbody').append('<tr style="font-style : italic"><td>Advance</td><td colspan="3">' + data.data[0].advanceAmount + '</td>');
                $('#tblHistTbody').append('<tr style="font-style : italic"><td>Payable Amount</td><td colspan="3">' + data.data[0].pendingAmount + '</td>');
                $('#advanceId').val(data.data[0].advanceAmount);
            }
            else {
                $('#advanceId').val('0');
            }
            $('#discountId').val('0');
            var months = [];


            //$("#fromMonthId").html('<option value="0">Select Month</option>').select2();
            //$("#toMonthId").html('<option value="0">Select Month</option>').select2();
            //$('#fromMonthId').val('0');
            //$('#toMonthId').val('0');
            var tableTag = "";
            tableTag = tableTag + '<table class="table" style="background-color: #072428 !important;color:white !important"><tbody>';
            if (data.data2 != null) {
                tableTag = tableTag + '<tr><td>' + data.data2.month + ',' + data.data2.yearName + ' | ' + data.data2.receivedDateString + ' | ' + data.data2.receivedByString + ' | ' + 'SL-' + data.data2.customerSL + '/' + data.data2.pageNo + ' | PAID' + '</td></tr>'
            }
            if (data.data1 != null && data.data1.length > 0) {

                $.each(data.data1, function (k, v) {
                    tableTag = tableTag + '<tr><td><input type= "checkbox" onClick="ChangeToMonth()" class="payCheck" id="' + v.text + '" />  ' + v.text + '</td></tr>';
                });
                tableTag = tableTag + '</tbody></table>';
            }
            $('#monthlyDueListPanel').append(tableTag);

            //$('#fromMonthId').select2({
            //    data: data.data1
            //});

            //$('#toMonthId').select2({
            //    data: months
            //});
            //$('#fromMonthId').trigger('change');
            //$('#toMonthId').trigger('change');

        });
    }
}
function LoadBillDatatable() {
    var table = $('#customerBillListTableId').DataTable();
    table.destroy();
    $('#customerBillListTableId').DataTable({
        scrollX: true,
        "ajax":
            {
                "url": '/DishBillCollection/GetBillCollectionList',
                "type": "POST",
                "datatype": "json"
            },
        //"responsive": true,


        //"processing": true,
        //"serverSide": true,
        "columns":
        [
            {
                "data": null,
                'width': '5%',
                "className": "center",
                render: function (data, type, row) {
                    return '<button type="button" onclick = "DeleteCollection(' + data.collectionId + ')" class="edituser btn info"><i class="fa fa-remove"></i></a>'
                }

            },
            {
                "data": null,
                "className": "center",
                render: function (data, type, row) {
                    return 'SL-' + data.customerSL + '/' + data.pageNo;
                }

            },

            { "data": "customerSerial", "autoWidth": true },
            { "data": "customerName", "autoWidth": true },
            { "data": "connFee", "autoWidth": true },
            { "data": "collectedDateString", "autoWidth": true },            
            { "data": "fromMonthYear", "autoWidth": true },
            { "data": "toMonthYear", "autoWidth": true },
            { "data": "monthlyFee", "autoWidth": true },
            { "data": "shiftingCharge", "autoWidth": true },
            { "data": "netAmount", "autoWidth": true },
            { "data": "rcvAmount", "autoWidth": true },
            { "data": "reConnFee", "autoWidth": true },
        ],
        "serverSide": true,
        "order": [1, "asc"],
        "processing": "true",
        "language": {
            "processing": "processing... please wait"
        },


    });
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();

}
function DeleteCollection(id) {
    if (confirm("Are you sure want delete the Collection?") == true) {
        $.get('/DishBillCollection/DeleteCollection/' + id, function (data) {
            if (data.success) {
                LoadBillDatatable();
                showSuccessMessage(data.message);
            }
            else if (data.success == false) {
                showErrorMessage(data.message);
            }
            else {
                showErrorMessage("Something error occured, Refresh the page and try again!");
            }

        }).fail(function (response) {
            showErrorMessage(response.responseText);
        });
    }
}

function ChangeToMonth() {
    var months = [];
    $('.payCheck:checkbox:checked').each(function () {
        months.push($(this).attr("id"));
    })
    
    var monthLen = months.length;
    if (monthLen > 0)
    {
        $.get('/DishBillCollection/GetMonthlyBill?cid=' + $('#customerId').val() + "&fromMonthYear=" + months[0] + "&toMonthYear=" + months[monthLen-1], function (data) {
            if (data.success == true) {
                $('#monFeeId').val(data.data);
                calculateAmount();
            }
            else {
                showErrorMessage(data.message);
            }
        })
    }
    
}
function HideFeeFields() {
    $('.connFeeFrmId').hide();
    $('.reConnFeeFrmId').hide();
    $('.otherFeeFrmId').hide();
    $('.shiftingFeeFormId').hide();
    
}