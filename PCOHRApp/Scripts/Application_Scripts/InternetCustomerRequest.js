$(document).ready(function () {
    loadInitialization();
});
var dropTrigger = 1;

function changeCustomer() {
    if ($('#customerId').val() != 0) {
        $.get('/InternetCustomer/GetCustomerById/' + $('#customerId').val(), function (data) {
            $('#customerNameId').val(data.data.customerName);
            $('#addressNameId').val(data.data.customerAddress);
            $('#hostNameId').val(data.data.hostName);
            $('#zoneNameId').val(data.data.zoneName);
            $('#assignedUserNameId').val(data.data.assignedUserName);
            $('#requiredNetInfoId').val(data.data.requiredNet);
            $('#monthlyBillId').val(data.data.monthBill);

            $('#connectionMonthId').val(data.data.connMonth == '0' ? '' : data.data.connMonth + ',' + (data.data.connYearName == 'null' ? '' : data.data.connYearName));
        })
    }
}



function loadInitialization() {
    $('.customerRequestForm').show();
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#customerRequestListId').hide();
    $('#btnCreateId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
    $('#requiredNetDiv').hide();
    $('#updatedMonthlyBillFormGroup').hide();
    $('.shiftingRow').hide();
    $('.disconnectRow').hide();
    $('#requestChargeFormGroup').show();
    

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
        $('#requestYearId').select2({
            data: connYears
        });
    });
    requestTypeList = [];
    $.get('/Dropdown/GetCustomerRequestTypeList?requestTypeGroup=Internet', function (data) {
        requestTypeList = data.data;
        $('#requestTypeId').select2({
            data: requestTypeList
        });
    });
    var zones = [];
    $.get('/Zone/GetZoneListForDropdown', function (data) {
        zones = data.data;
        $('#zoneId').select2({
            data: zones
        });
    });
    var assignedUsers = []
    $.get('/Account/GetUserDropdownList', function (data) {
        assignedUsers = data.data;
        $('#assignedUserId').select2({
            data: assignedUsers
        });
    });

    $("#addressId").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/InternetCustomer/GetCustomerAddresses',
                dataType: "json",
                data: { search: $("#addressId").val() },
                success: function (data) {
                    response($.map(data.data, function (item) {
                        return { label: item, value: item };
                    }));
                },
                error: function (xhr, status, error) {
                    alert("Error");
                }
            });
        }
    });
};

function onReqTypeChanged() {
    onRequestTypeClearUI();
    var text = $("#requestTypeId option:selected").text();
    if (text == 'Bandwidth Upgrade' || text == 'Bandwidth Downgrade') {
        $('#requiredNetDiv').show();
        $('#updatedMonthlyBillFormGroup').show();
        $('#requestChargeFormGroup').hide();
        $('.shiftingRow').hide();
        $('.disconnectRow').hide();
        
    }
    else if (text == 'Shifting') {
        $('#requiredNetDiv').hide();
        $('.shiftingRow').show();
        $('.disconnectRow').hide();
        $('#updatedMonthlyBillFormGroup').hide();
        $('#requestChargeFormGroup').show();
       
    }
    else if(text == 'Discontinue'){
        $('#requiredNetDiv').hide();
        $('.shiftingRow').hide();
        $('.disconnectRow').show();
        $('#updatedMonthlyBillFormGroup').hide();
        $('#requestChargeFormGroup').hide();
        
    }
    else {
        $('#requiredNetDiv').hide();
        $('.shiftingRow').hide();
        $('.disconnectRow').hide();
        $('#updatedMonthlyBillFormGroup').hide();
        $('#requestChargeFormGroup').show();
        
    }
}
$('#btnCreateId').click(function () {
    loadInitialization();
    clearUI();
});
$('#btnListId').click(function () {
    $('#customerRequestListId').show();
    $('#messageBoxId').hide();
    $('.customerRequestForm').hide();
    $('#btnClearId').hide();
    $('#btnSaveId').hide();
    $('#btnCreateId').show();
    $('#btnListId').hide();
    loadCustomerRequestDatatable();
});



$('#btnSaveId').click(function () {
    var text = $("#requestTypeId option:selected").text();

    if ($('#cid').val() == '') {
        showErrorMessage('Provide Customer!!');
    }
    else if ($('#requestTypeId').val() == '0') {
        showErrorMessage('Provide Request Type!!');
    }
    else if (text == 'Discontinue' && $('#requestMonthId').val() == 0) {
        showErrorMessage('Provide Month!!');
    }
    else if (text == 'Discontinue' && $('#requestYearId').val() == 0) {
        showErrorMessage('Provide Year!!');
    }
    else if (text == 'Shifting' && $('#hostId').val() == 0) {
        showErrorMessage('Provide Host!!');
    }
    else if (text == 'Shifting' && $('#hostPhoneId').val() == 0) {
        showErrorMessage('Provide Host Phone!!');
    }
    else if (text == 'Shifting' && $('#zoneId').val() == 0) {
        showErrorMessage('Provide Zone!!');
    }
    else if (text == 'Shifting' && $('#assignedUserId').val() == 0) {
        showErrorMessage('Provide Assigned User!!');
    }
    else if ((text == 'Bandwidth Upgrade' || text == 'Bandwidth Downgrade') && $('#requiredNetId').val() == '') {
        showErrorMessage('Provide Required Net!!');
    }
    else if ((text == 'Bandwidth Upgrade' || text == 'Bandwidth Downgrade') && $('#updatedMonthlyBillId').val() == '') {
        showErrorMessage('Provide New Monthly Bill!!');
    }
   
    
    else {
        var inputData = {};
        if (text == 'Discontinue') {
            inputData = {
                cid: $('#customerId').val(),
                requestTypeId: $('#requestTypeId').val(),
                remarks: $('#remarksId').val(),
                requestMonth: $('#requestMonthId').val(),
                requestYear: $('#requestYearId').val()

            }
        }
        else if (text == 'Reconnect') {
            inputData = {
                cid: $('#customerId').val(),
                requestTypeId: $('#requestTypeId').val(),
                requestCharge: $('#requestChargeId').val(),
                remarks: $('#remarksId').val(),
            }
        }
        else if(text == 'Shifting'){
            inputData = {
                cid: $('#customerId').val(),
                requestTypeId: $('#requestTypeId').val(),
                requestCharge: $('#requestChargeId').val(),
                remarks: $('#remarksId').val(),
                customerAddress: $('#addressId').val(),
                hostId: $('#hostId').val(),
                zoneId: $('#zoneId').val(),
                assignedUserId: $('#assignedUserId').val()
            
            }
        }
        else if (text == 'Bandwidth Upgrade' || text == 'Bandwidth Downgrade') {
            inputData = {
                cid: $('#customerId').val(),
                requestTypeId: $('#requestTypeId').val(),
                updatedMontlyBill: $('#updatedMonthlyBillId').val(),
                requiredNet: $('#requiredNetId').val(),
                remarks: $('#remarksId').val()
            }
        }
        $.post('/InternetCustomerRequest/InsertCustomerRequest', inputData, function (data) {
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
                showErrorMessage(data.message)
            }
            else {
                showErrorMessage("Something error occured, Refresh the page and try again!");
            }
        }).fail(function (response) {
            showErrorMessage(response.responseText);
        });
    }
});

$('#btnClearId').click(function () {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#errorMessageBoxId').val('');
    $('#successMessageBoxId').val('');
    clearUI();
})

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

function clearUI() {
    $('#customerId').val('0');
    $('#requestTypeId').val('0');
    $('#updatedMonthlyBillId').val('');
    $('#requestChargeId').val('0');
    $('#requiredNetId').val('');
    $('#remarksId').val('');
    $('#addressId').val('');
    $('#hostId').val('0');
    $('#hostPhoneId').val('0');
    $('#zoneId').val('0');
    $('#assignedUserId').val('0');
    $('#requestMonthId').val('0');
    $('#requestYearId').val('0');
    $('#customerNameId').val('');
    $('#addressNameId').val('');
    $('#hostNameId').val('');
    $('#zoneNameId').val('');
    $('#assignedUserNameId').val('');
    $('#connectionMonthId').val('');
    $('#requiredNetInfoId').val('');
    $('#monthlyBillId').val('');
    $('.select2').trigger('change');
}
function onChangeHost() {
    if (dropTrigger == 1) {
        if ($('#hostId').val() != '0') {
            var hostList = []
            $.get('/Host/GetHostPhoneListForDropdown?search=&page=1&selectedId=' + $('#hostId').val(), function (data) {
                hostList = data.results;
                var select = $("#hostPhoneId");

                if (data.results) {
                    $(data.results).each(function (index, item) {
                        select.append($("<option>").val(item.id).text(item.text));
                    });
                }
                dropTrigger = 0;
                $('#hostPhoneId').val($('#hostId').val());
                $('#hostPhoneId').trigger('change')
                dropTrigger = 1;
            });

        }
    }
};

function onChangePhone() {
    if (dropTrigger == 1) {
        if ($('#hostPhoneId').val() != '0') {
            var hostList = []
            $.get('/Host/GetHostListForDropdown?search=&page=1&selectedId=' + $('#hostPhoneId').val(), function (data) {
                hostList = data.results;
                var select = $("#hostId");

                if (data.results) {
                    $(data.results).each(function (index, item) {
                        select.append($("<option>").val(item.id).text(item.text));
                    });
                }
                dropTrigger = 0;
                $('#hostId').val($('#hostPhoneId').val());
                $('#hostId').trigger('change')
                dropTrigger = 1;
            });

        }
    }
}
function onRequestTypeClearUI() {
    $('#updatedMonthlyBillId').val('');
    $('#requestChargeId').val('0');
    $('#requiredNetId').val('');
    $('#remarksId').val('');
    $('#addressId').val('');
    //$('#hostId').val('0');
    //$('#hostPhoneId').val('0');
    $('#zoneId').val('0');
    $('#assignedUserId').val('0');
    $('#requestMonthId').val('0');
    $('#requestYearId').val('0');

    $('#assignedUserId').trigger('change');
    $('#requestMonthId').trigger('change');
    $('#requestYearId').trigger('change');
    $('#zoneId').trigger('change');


    polpulateHostAndHostphone();
    $('#hostId').val('0');
    $('#hostPhoneId').val('0');
    $('#hostId').trigger('change');
    $('#hostPhoneId').trigger('change');
};

function polpulateHostAndHostphone() {
    $('#hostId').select2({
        ajax: {
            url: '/Host/GetHostListForDropdown',
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

    $('#hostPhoneId').select2({
        ajax: {
            url: '/Host/GetHostPhoneListForDropdown',
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

function DeleteCustomerRequest(id) {
    if (confirm("Are you sure want delete the request?") == true) {
        $.get('/InternetCustomerRequest/DeleteCustomerRequest/' + id, function (data) {
            if (data.success) {
                showSuccessMessage(data.message);
                loadCustomerRequestDatatable();

            }
            else if (data.success == false) {
                showErrorMessage(data.message);
            } else {
                showErrorMessage("Something error occured, Refresh the page and try again!");
            }
        }).fail(function (response) {
            showErrorMessage(response.responseText);
        });
    }
}

function loadCustomerRequestDatatable() {
    var table = $('#customerRequestListTableId').DataTable();
    table.destroy();
    $('#customerRequestListTableId').DataTable({
        "ajax":
            {
                "url": '/InternetCustomerRequest/GetCustomerRequestList',
                "type": "POST",
                "datatype": "json"
            },
        "responsive": true,


        //"processing": true,
        //"serverSide": true,
        "columns":
        [
            {
                "data": null,
                'width': '5%',
                "className": "center",
                render: function (data, type, row) {
                    return '<button type="button" onclick = "DeleteCustomerRequest(' + data.requestId + ')" class="edituser btn info"><i class="fa fa-remove"></i></a>'
                }

            },
            { "data": "customerSerial", 'width': '5%' },
            { "data": "customerName", "autoWidth": true },
            { "data": "customerPhone", 'width': '5%' },
            { "data": "requestName", 'width': '5%' },
            { "data": "requestCharge", "autoWidth": true },
            { "data": "requiredNet", "autoWidth": true },
            { "data": "updatedMontlyBill", "autoWidth": true },
            { "data": "requestMonth", 'width': '5%' },
            { "data": "requestYearName", 'width': '5%' },
            { "data": "remarks", "autoWidth": true }

            


        ],
        "serverSide": true,
        "order": [1, "asc"],
        "processing": "true",
        "language": {
            "processing": "processing... please wait"
        },


    });
}