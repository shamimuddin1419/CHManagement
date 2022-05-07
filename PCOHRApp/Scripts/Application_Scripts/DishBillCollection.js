$(document).ready(function () {
    loadInitialization();
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
    else {
        var inputData = {
            cid: $('#customerId').val(),
            connFee: $('#connFeeId').val(),
            reConnFee: $('#reConnFeeId').val(),
            othersAmount: $('#otherFeeId').val(),
            monthlyFee: $('#monFeeId').val(),
            fromMonth: $('#fromMonthId').val(),
            toMonth: $('#toMonthId').val(),
            shiftingCharge: $('#shiftingFeeId').val(),
            descriptionId: $('#descriptionId').val(),
            netAmount: $('#netAmountId').val(),
            rcvAmount: $('#receiveAmountId').val(),
            adjustAdvance: $('#advanceId').val(),
            customerSL: $('#custSLId').val(),
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



$('#connFeeId,#reConnFeeId,#otherFeeId,#monFeeId,#shiftingFeeId').change(function () {
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
    $('#pageNoId').val('');
    $('#collectedById').val('0');
    $('#receivedById').val('0');
    $('#customerNameId').val('');
    $('#addressNameId').val('');
    $('#hostNameId').val('');
    $('#zoneNameId').val('');
    $('#assignedUserNameId').val('');
    $('#connectionMonthId').val('');
    $('#monthlyBillId').val('');
    $('#customerId').trigger('change');
    $('#fromMonthId').trigger('change');
    $('#toMonthId').trigger('change');
    $('#collectedById').trigger('change');
    $('#receivedById').trigger('change');
    $('#tblHistTbody').empty();

}
function calculateAmount() {
    var connFee = $('#connFeeId').val() == '' ? 0 : parseInt($('#connFeeId, #advanceId').val());
    var reConnFee = $('#reConnFeeId').val() == '' ? 0 : parseInt($('#reConnFeeId').val());
    var otherFee = $('#otherFeeId').val() == '' ? 0 : parseInt($('#otherFeeId').val());
    var monFee = $('#monFeeId').val() == '' ? 0 : parseInt($('#monFeeId').val());
    var shiftingFee = $('#shiftingFeeId').val() == '' ? 0 : parseInt($('#shiftingFeeId').val());
    var advanceAmount = $('#advanceId').val() == '' ? 0 : parseInt($('#advanceId').val());
    $('#netAmountId').val(connFee + reConnFee + otherFee + monFee + shiftingFee);
    $('#receiveAmountId').val(connFee + reConnFee + otherFee + monFee + shiftingFee - advanceAmount);
    
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
    var users = []
    $.get('/Account/GetUserDropdownList', function (data) {
        users = data.data;
        $('#collectedById').select2({
            data: users
        });
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
            $('#zoneNameId').val(data.data.zoneName);
            $('#assignedUserNameId').val(data.data.assignedUserName);
            $('#monthlyBillId').val(data.data.monthBill);

            $('#connectionMonthId').val(data.data.connMonth == '0' ? '' : data.data.connMonth + ',' + (data.data.connYearName == 'null' ? '' : data.data.connYearName));
        });
        $.get('/DishBillCollection/GetPreviousInfoList/' + $('#customerId').val(), function (data) {
            $('#tblHistTbody').empty();
            $.each(data.data, function (k, v) {
                $('#tblHistTbody').append('<tr><td>' + v.collectionType + '</td><td>' + v.transactionAmount + '</td><td>' + v.transactionMonth + '</td><td>' + v.yearName + '</td>');
            });
            if (data.data.length > 0) {
                $('#tblHistTbody').append('<tr style="font-style : italic"><td>Advance</td><td colspan="3">' + data.data[0].advanceAmount + '</td>');
                $('#tblHistTbody').append('<tr style="font-style : italic"><td>Payable Amount</td><td colspan="3">' + data.data[0].pendingAmount + '</td>');
                $('#advanceId').val(data.data[0].advanceAmount);
            }
            var months = [];
            
            $("#fromMonthId").html('<option value="0">Select Month</option>').select2();
            $("#toMonthId").html('<option value="0">Select Month</option>').select2();
            $('#fromMonthId').val('0');
            $('#toMonthId').val('0');

            if (data.data1 != null && data.data1.length > 0) {
                months = data.data1
            }
            
            $('#fromMonthId').select2({
                data: data.data1
            });
            
            $('#toMonthId').select2({
                data: months
            });
            $('#fromMonthId').trigger('change');
            $('#toMonthId').trigger('change');
            
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
            
            { "data": "voucherNo", "autoWidth": true },
            { "data": "customerSerial", "autoWidth": true },
            { "data": "customerName", "autoWidth": true },
            { "data": "connFee", "autoWidth": true },
            { "data": "reConnFee", "autoWidth": true },
            { "data": "fromMonthYear", "autoWidth": true },
            { "data": "toMonthYear", "autoWidth": true },
            { "data": "monthlyFee", "autoWidth": true },
            { "data": "shiftingCharge", "autoWidth": true },
            { "data": "netAmount", "autoWidth": true },
            { "data": "rcvAmount", "autoWidth": true },
            { "data": "customerSL", "autoWidth": true },
            { "data": "pageNo", "autoWidth": true },
            { "data": "collectedDateString", "autoWidth": true },
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
            else if(data.success == false){
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
    $.get('/DishBillCollection/GetMonthlyBill?cid=' + $('#customerId').val() + "&fromMonthYear=" + $('#fromMonthId').val() + "&toMonthYear=" + $('#toMonthId').val(), function (data) {
        if (data.success == true) {
            $('#monFeeId').val(data.data);
            calculateAmount();
        }
        else {
            showErrorMessage(data.message);
        }
    })
}