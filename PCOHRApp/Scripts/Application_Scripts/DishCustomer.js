
$(document).ready(function () {
    loadCreateUI();
    loadCustomerList();
    $('.customerSerialPrefixDivId').show();
    $('#customerSerialDivId').hide();

    $('#EntryDateDateId').datetimepicker({
        useCurrent: false,
        format: 'DD/MM/YYYY'
    });
});
var dropTrigger = 1;
function loadCustomerList() {
    //$('#customerListId').show();
    //$('#messageBoxId').hide();
    //$('#customerFormId').hide();
    //$('#btnClearId').hide();
    //$('#btnSaveId').hide();
    //$('#btnCreateId').show();
    //$('#btnListId').hide();
    var table = $('#customerListTableId').DataTable();
    table.destroy();
    $('#customerListTableId').DataTable({
        
        "lengthMenu": [[5,10, 25, 50, -1], [5,10, 25, 50, "All"]],
        "ajax":
            {
                "url": '/DishCustomer/GetCustomerList',
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
                    return '<button type="button" onclick = "EditCustomer(' + data.id + ')" class="edituser btn info"><i class="fa fa-edit"></i></a>'
                }

            },
            { "data": "customerSerial", "autoWidth": true },
            { "data": "customerName", "autoWidth": true },
            { "data": "customerPhone", "autoWidth": true },
            { "data": "hostName", "autoWidth": true },
            { "data": "zoneName", "autoWidth": true },
            { "data": "assignedUserName", "autoWidth": true },
            { "data": "isActiveString", "autoWidth": true }

        ],
        "serverSide": true,
        "order": [1, "asc"],
        "processing": "true",
        "language": {
            "processing": "processing... please wait"
        },


    });
}

$('#customerId').change(function () {
    $.get('/DishCustomer/GetDuplicateByIdAndSerialPrefix?id=' + $('#customerId').val() + '&serialPrefixId=' + $('#customerSerialPrefixId').val(), function (data) {
        if (data.success == true) {
            if (data.isDuplicate == true) {
                toastr.error("Customer with this serial already exists!!");
            }
            else {
                toastr.success("Serial Number is Available!!");
            }
        }
    })
})

$('#btnCreateId').click(function () {
    loadCreateUI();
    $('.customerSerialPrefixDivId').show();
    $('#customerSerialDivId').hide();
    $('#customerId').val('0');
    $('#customerSerialPrefixId').val('0');
    $('#customerSerialId').val('');

    $('#customerNameId').val('');
    $('#phoneNumberId').val('');
    $('#addressId').val('');
    $('#hostId').val('0');
    $('#hostPhoneId').val('0');

    $('#zoneId').val('0');
    $('#assignedUserId').val('0');
    $('#connFeeId').val('0');
    $('#connFeeId').prop('readonly', false);
    $('#monthBillId').val('0');
    $('#monthBillId').prop('readonly', false);
    $('#othersAmountId').val('0');
    $('#nidId').val('');
    $("#EntryDateDateId").data("DateTimePicker").date(null);
    $('#connMonthId').val('0');
    $('#connYearId').val('0');
    $('.select2').trigger('change');
    $('#isActive').prop('checked', true);
    $('.switchery').trigger('click');
    $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Save');
})

$('#btnSaveId').click(function () {
    if ($('#customerSerialPrefixId').val() == null || $('#customerSerialPrefixId').val() == '0') {
        toastr.error('Provide Serial Prefix!!');
    }
    else if ($('#customerId').val() == '') {
        toastr.error('Provide Serial!!');
    }
    else if ($('#customerNameId').val() == '') {
        toastr.error('Provide Name!!');
    }
    else if ($('#phoneNumberId').val() == '') {
        toastr.error('Provide Phone Number!!');
    }
    else if ($('#hostId').val() == '0') {
        toastr.error('Provide Host!!');
    }
    else if ($('#hostPhoneId').val() == '0') {
        toastr.error('Provide Host Phone Number!!');
    }
    else if ($('#zoneId').val() == '0') {
        toastr.error('Provide Zone!!');
    }
    else if ($('#connMonthId').val() == '0') {
        toastr.error('Provide Connection Month!!');
    }
    else if ($('#connYearId').val() == '0') {
        toastr.error('Provide Connection Year!!');
    }
    //else if ($('#nidId').val() == '') {
    //    toastr.error('Provide NID!!');
    //}
    
    else {
        var inputData = {
            customerId: $('#customerId').val(),
            customerSerialId: $('#customerSerialPrefixId').val(),
            customerName: $('#customerNameId').val(),
            customerPhone: $('#phoneNumberId').val(),
            customerAddress: $('#addressId').val(),
            hostId: $('#hostId').val(),
            zoneId: $('#zoneId').val(),
            assignedUserId: $('#assignedUserId').val(),
            connFee: $('#connFeeId').val(),
            monthBill: $('#monthBillId').val(),
            othersAmount: $('#othersAmountId').val(),
            //description: $('#descriptionId').val(),
            EntryDateString: $('#EntryDateDateId input').val(),
            connMonth: $('#connMonthId').val(),
            connYear: $('#connYearId').val(),
            isActive: true,
            nid : $('#nidId').val()
        }
        if ($('#btnSaveId').text().indexOf('Save') >= 0) {
            inputData.insertFlag = 1;
        }
        else {
            inputData.insertFlag = 2;
        }

        $.post('/DishCustomer/InsertOrUpdateCustomer', inputData, function (data) {
            if (data.success == true) {
                
                if ($('#btnSaveId').text().indexOf('Save') >= 0) {
                    //toastr.success("Data Saved");
                    clearUI();
                   $('#successMessageBoxId').text('Data Saved');
                }
                else 
                {
                    //toastr.success("Data Updated");
                    
                    $('#btnCreateId').click();
                    $('#successMessageBoxId').text('Data Updated');
                }
                
                //if ($('#btnSaveId').text().indexOf('Update') >= 0) {
                //    $('#btnCreateId').click();
                //    $('#messageBoxId').show();
                //    $('#successMessageBoxId').show();
                //}
                $('#messageBoxId').show();
                $('#successMessageBoxId').show()

                //else
                //    clearUI();
                loadCustomerList();

            }
            else if(data.success == false) {
                toastr.error(data.message)
            }
            else {
                toastr.error("Something error occured, Refresh the page and try again!");
            }
        }).fail(function (response) {
            toastr.error(response.responseText);
        });
    }
})
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
    $('#customerSerialPrefixId').val('0');
    $('#customerSerialId').val('');

    $('#customerNameId').val('');
    $('#phoneNumberId').val('');
    $('#addressId').val('');
    $('#hostId').val('0');
    $('#hostPhoneId').val('0');
    
    $('#zoneId').val('0');
    $('#assignedUserId').val('0');
    $('#connFeeId').val('0');
    $('#monthBillId').val('0');
    $('#othersAmountId').val('0');
    $("#EntryDateDateId").data("DateTimePicker").date(null);
    $('#connMonthId').val('0');
    $('#connYearId').val('0');
    $('#nidId').val('');
    $('.select2').trigger('change');
    $('#isActive').prop('checked', true);
    $('.switchery').trigger('click');
}
function loadCreateUI() {
    $('#customerFormId').show();
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    //$('#customerListId').hide();
    $('#btnCreateId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
    $('#connYearId').select2("enable");
    $('#connMonthId').select2("enable");
    var customerSerials = []
    $.get('/Dropdown/GetCustomerSerialList', function (data) {
        customerSerials = data.data;
        $('#customerSerialPrefixId').select2({
            data: customerSerials
        });
    });
    var hosts = [];
    //$.get('/Host/GetHostListForDropdown', function (data) {
    //    hosts = data.data;
    //    $('#hostId').select2({
    //        //data: hosts
    //        data: function (params) {
    //            var query = {
    //                search: params.term,
    //                page: params.page || 1
    //            }
    //        }
    //    });
    //});

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
                    selectedId : 0
                }
                return query
            }


        }
    });

    $("#addressId").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/DishCustomer/GetCustomerAddresses',
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

 
  
    //var hostPhones = [];
    //$.get('/Host/GetHostPhoneListForDropdown', function (data) {
    //    hostPhones = data.data;
    //    $('#hostPhoneId').select2({
    //        data: hostPhones
    //    });
    //});
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
    var connYears = [];
    $.get('/Dropdown/GetYearList', function (data) {
        connYears = data.data;
        $('#connYearId').select2({
            data: connYears
        });
    });
}

function loadHostIdForEdit(id) {
    var hostList = [];
    $.get('/Host/GetHostListForDropdown?search=&page=1&selectedId=' + id, function (data) {
        hostList = data.results;
        var select = $("#hostId");

        if (data.results) {
            $(data.results).each(function (index, item) {
                select.append($("<option>").val(item.id).text(item.text));
            });
        }
        //dropTrigger = 0;
        $('#hostId').val(id);
        $('#hostId').trigger('change')
        //dropTrigger = 1;
    });
}
function EditCustomer(id) {

    $.get('/DishCustomer/GetCustomerById/' + id, function (data) {
        if (data.success) {
            loadCreateUI();
            $('.customerSerialPrefixDivId').hide();
            $('#customerSerialDivId').show();
            $('#btnClearId').hide();
            $('#btnCreateId').show();
            $('#customerId').val(data.data.customerId);
            $('#customerSerialPrefixId').val(data.data.customerSerialId);
            $('#customerSerialId').val(data.data.customerSerial);
            
            $('#customerNameId').val(data.data.customerName);
            $('#phoneNumberId').val(data.data.customerPhone);
            $('#addressId').val(data.data.customerAddress);
            $('#hostId').val(data.data.hostId);
            $('#zoneId').val(data.data.zoneId);
            $('#assignedUserId').val(data.data.assignedUserId);
            $('#nidId').val(data.data.nid);
            $('#connFeeId').val(data.data.connFee);
            $('#connFeeId').prop('readonly', true);
            $('#monthBillId').val(data.data.monthBill);
            $('#monthBillId').prop('readonly', true);
            $('#othersAmountId').val(data.data.othersAmount);
            $('#othersAmountId').prop('readonly', true);
                       
            if (data.data.EntryDateString != "") {
                $("#EntryDateDateId").data("DateTimePicker").date(null);
                $('#EntryDateDateId').data('DateTimePicker').defaultDate(data.data.EntryDateString);
            } else {
                
                $("#EntryDateDateId").data("DateTimePicker").date(null);
            }
            
            $('#connMonthId').val(data.data.connMonth);
            $('#connMonthId').select2("enable", false);
            $('#connYearId').val(data.data.connYear);
            $('#connYearId').select2("enable", false);
            $('#isActive').prop('checked', data.data.isActive);
            $('.switchery').trigger('click');
            loadHostIdForEdit(data.data.hostId);
            $('.select2').trigger('change')
            $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Update');

            debugger;
        }
    })
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
                $('.select2').trigger('change')
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
                $('.select2').trigger('change')
                dropTrigger = 1;
            });

        }
    }
}
