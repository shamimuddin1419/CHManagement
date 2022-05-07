$(document).ready(function () {
    loadInitialization();
});
var month = 0;
var year = 0;
$('#isBatch').change(function () {
    if (this.checked) {
        $('.individualCustomerDiv').hide();
    }
    else {
        $('.individualCustomerDiv').show();
        polulateCustomerDropDown();
        $('#customerId').val('0').trigger('change');
        clearCustomerInfo();
    }
})

$('#btnCreateId').click(function () {
    loadInitialization();
    clearUI();
})



$('#btnListId').click(function () {
    $('#customerBillListId').show();
    $('#messageBoxId').hide();
    $('.billGenerateForm').hide();
    $('#btnClearId').hide();
    $('#btnSaveId').hide();
    $('#btnCreateId').show();
    $('#btnSearchId').show();

    $('#btnListId').hide();
    month = 0;
    year = 0;
    var yearList = [];
    $.get('/Dropdown/GetYearList', function (data) {
        yearList = data.data;
        $('#requestYearSearchId').select2({
            data: yearList
        });
    });
    $('#requestMonthSearchId').val('0').trigger('change');
    $('#requestYearSearchId').val('0').trigger('change');
    $('#customerBillListTableId').DataTable().clear().destroy();
    
    
});

$('#btnSaveId').click(function () {
   
    if ($('#monthId').val() == '0') {
        showErrorMessage('Provide Month!!');
    }
    else if ($('#yearId').val() == '0') {
        showErrorMessage('Provide Year!!');
    }
    else
        {
        if ($('#isBatch').is(":checked") == false && $('#customerId').val() == '0') {
            showErrorMessage('Provide Customer!!');
        }
        else {
            var inputData = {
                isBatch: $('#isBatch').is(":checked"),
                month: $('#monthId').val(),
                year: $('#yearId').val(),
                remarks: $('#remarksId').val()
            };
            if ($('#isBatch').is(":checked") == false) {
                inputData.cid = $('#customerId').val();
            }
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

            $.post('/InternetBillGenerate/InsertBillGenerate', inputData, function (data) {
                

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
                $.unblockUI();
            }).fail(function (response) {
                showErrorMessage(response.responseText);
                $.unblockUI();
            });
        }
    }
});

$('#btnClearId').click(function () {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#errorMessageBoxId').val('');
    $('#successMessageBoxId').val('');
    clearUI();
});

$('#btnSearchId').click(function () {
    if ($('#requestMonthSearchId').val() == '0') {
        showErrorMessage('Provide Month!!');
    }
    else if ($('#requestYearSearchId').val() == '0') {
        showErrorMessage('Provide Year!!');
    }
    else {
        month = $('#requestMonthSearchId').val();
        year = $('#requestYearSearchId').val();
        LoadBillDatatable();
    }
})

function LoadBillDatatable() {
    var table = $('#customerBillListTableId').DataTable();
    table.destroy();
    $('#customerBillListTableId').DataTable({
        "ajax":
            {
                "url": '/InternetBillGenerate/GetBillGenerateList?month='+month+'&year='+year,
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
                    return '<button type="button" onclick = "DeleteBill(' + data.billDetailId + ')" class="edituser btn info"><i class="fa fa-remove"></i></a>'
                }

            },
            { "data": "customerSerial", "autoWidth": true },
            { "data": "customerName", "autoWidth": true },
            { "data": "month", "autoWidth": true },
            { "data": "yearName", "autoWidth": true },
            { "data": "billAmount", "autoWidth": true }
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
    $('.billGenerateForm').show();
    $('.individualCustomerDiv').hide();



    var years = [];
    $.get('/Dropdown/GetYearList', function (data) {
        years = data.data;
        $('#yearId').select2({
            data: years
        });
    });

};

function polulateCustomerDropDown() {
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
}
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
function clearCustomerInfo() {
    $('#customerNameId').val('');
    $('#addressNameId').val('');
    $('#hostNameId').val('');
    $('#zoneNameId').val('');
    $('#assignedUserNameId').val('');
    $('#requiredNetInfoId').val('');
    $('#monthlyBillId').val('');
    $('#connectionMonthId').val('');
}
function clearUI() {
    $('#monthId').val('0').trigger('change');
    $('#yearId').val('0').trigger('change');
    $('#remarks').val('0').trigger('change');
    $('#customerId').val('0').trigger('change');
    $('.individualCustomerDiv').hide();
    $('#isBatch').prop('checked', true);
    $('.switchery').trigger('click');
    clearCustomerInfo();

}

function DeleteBill(id) {
    if (confirm("Are you sure want delete the Bill?") == true) {
        $.get('/InternetBillGenerate/DeleteBill/' + id, function (data) {
            if (data.success) {
                LoadBillDatatable();
                showSuccessMessage(data.message);
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