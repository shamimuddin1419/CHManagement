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
    loadCustomerDropdown();
    loadCustomerInfoList();
};
function loadCustomerDropdown() {
    $('#customerId').select2({
        ajax: {
            url: '/DishCustomer/GetCustomerCardInfoListForDropdown',
            data: function (params) {
                var query = {
                    search: params.term,
                    page: params.page || 1,
                    selectedId: 0,
                    searchBy : $('input[name="inlineRadioOptions"]:checked').val()
                }
                return query
            }
        }
    });
}

function changeCustomer() {
    if ($('#customerId').val() != 0) {
        $.get('/DishCustomer/GetCustomerById/' + $('#customerId').val(), function (data) {
            $('#customerNameId').val(data.data.customerName);
            $('#phoneNumberId').val(data.data.customerPhone);
            $('#addressId').val(data.data.customerAddress);
            $('#insertFlagId').val('1');
            $('#customerInfoId').val(data.data.id);
        });

    }
}

$('#btnSaveId').click(function () {
    debugger;
    if ($('#customerInfoId').val() == '0' || $('#customerInfoId').val() == '') {
        toastr.error("গ্রাহক সিলেক্ট করুন");
    }
    else if ($('#customerNameId').val() == '0') {
        toastr.error("গ্রাহকের নাম দিন");
    }
    else if ($('#phoneNumberId').val() == '0') {
        toastr.error("গ্রাহকের নাম্বার দিন");
    }
    
    else {
        var inputData = {
            customerId: $('#customerInfoId').val(),
            customerLocality: $('#customerLocalityId').val(),
            customerName: $('#customerNameId').val(),
            customerPhone: $('#phoneNumberId').val(),
            ownerName: $('#ownerNameId').val(),
            ownerPhone: $('#ownerPhoneNumberId').val(),
            insertFlag: $('#insertFlagId').val(),
            customerAddress: $('#addressId').val()
            
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

        $.post('/DishCustomer/InsertOrUpdateCustomerCardInfo', inputData, function (data) {
            if (data.success == true) {
                showSuccessMessage(data.message);
                loadCustomerDropdown();
                loadCustomerInfoList();
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

function clearUI() {
    $('#customerId').val('0');
    $('#customerId').trigger('change');
    $('#customerLocalityId').val('');
    $('#customerNameId').val('');
    $('#phoneNumberId').val('');
    $('#ownerNameId').val('');
    $('#ownerPhoneNumberId').val('');
    $('#addressId').val('');
    $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Save Card');
    $('#customerInfoId').val('');
}

$('#btnClearId').click(function () {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#errorMessageBoxId').val('');
    $('#successMessageBoxId').val('');
    clearUI();
});

function loadCustomerInfoList() {

    var table = $('#customerCardListTableId').DataTable();
    table.destroy();
    $('#customerCardListTableId').DataTable({

        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        "ajax":
        {
            "url": '/DishCustomer/GetCustomerCardInfoList',
            "type": "POST",
            "datatype": "json"
        },
        //"responsive": true,

        "scrollX": true,
        //"processing": true,
        //"serverSide": true,
        "columns":
            [
                {
                    "data": null,
                    'width': '5%',
                    "className": "center",
                    render: function (data, type, row) {
                        return '<button type="button" onclick = "EditCustomerCardInfo(' + data.customerId + ')" class="edituser btn info"><i class="fa fa-edit"></i></a>'
                    }

                },
                { "data": "customerSerial", "autoWidth": true },
                { "data": "customerName", "autoWidth": true },
                { "data": "customerPhone", "autoWidth": true },
                { "data": "ownerName", "autoWidth": true },
                { "data": "ownerPhone", "autoWidth": true }

            ],
        "serverSide": true,
        "order": [1, "asc"],
        "processing": "true",
        "language": {
            "processing": "processing... please wait"
        },


    });
}
function EditCustomerCardInfo(id) {
    debugger;
    $.get('/DishCustomer/GetCustomerCardInfoById/' + id, function (data) {
        debugger;
        if (data.success) {
            //loadCreateUI();
            $('#customerInfoId').val(data.data.customerId);
            $('#customerNameId').val(data.data.customerName);
            $('#phoneNumberId').val(data.data.customerPhone);
            $('#addressId').val(data.data.customerAddress);
            $('#customerLocalityId').val(data.data.customerLocality);
            $('#ownerNameId').val(data.data.ownerName);
            $('#ownerPhoneNumberId').val(data.data.ownerPhone);
            $('#insertFlagId').val('0');
            $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Update');

            debugger;
        }
    })
}