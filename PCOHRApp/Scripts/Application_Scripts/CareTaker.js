
$(document).ready(function () {
    loadCreateUI();
});
$('#joiningDateId').on('changeDate', function (ev) {
    $(this).datepicker('hide');
});
$('#btnListId').click(function () {
    $('#careTakerListId').show();
    $('#messageBoxId').hide();
    $('#careTakerFormId').hide();
    $('#btnClearId').hide();
    $('#btnSaveId').hide();
    $('#btnCreateId').show();
    $('#btnListId').hide();
    var table = $('#careTakerListTableId').DataTable();
    table.destroy();
    $('#careTakerListTableId').DataTable({
        //"ajax": '/Host/GetHostList',
        "ajax":
            {
                "url": '/CareTaker/GetCareTakerList',
                "type": "POST",
                "datatype": "json"
            },
        "responsive": true,

        "columns":
        [
            {
                "data": null,
                'width': '5%',
                "className": "center",
                render: function (data, type, row) {
                    return '<button type="button" onclick = "EditCareTaker(' + data.careTakerId + ')" class="edituser btn info"><i class="fa fa-edit"></i></a>'
                }

            },
            { "data": "careTakerName" },
            //{ "data": "permanentAddress" },
            { "data": "phoneNo" },
            { "data": "email" },
            { "data": "nid" },
            { "data": "joiningDateString" },
            { "data": "isActiveString" }
            

        ],
        "serverSide": true,
        "order": [1, "asc"],
        "processing": "true",
        "language": {
            "processing": "processing... please wait"
        },

    });
})


$('#btnCreateId').click(function () {
    loadCreateUI();
    clearUI();
    $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Save');
})

$('#btnSaveId').click(function () {
    if ($('#careTakerNameId').val() == '') {
        showErrorMessage('Provide CareTaker Name!!');
    }
    else if ($('#phoneNoId').val() == '') {
        showErrorMessage('Provide Phone Number!!');
    }
    else if ($('#permanentAddressId').val() == '') {
        showErrorMessage('Provide Permanent Address!!');
    }
    else {
        var inputData = {
            careTakerId: $('#careTakerId').val(),
            careTakerName: $('#careTakerNameId').val(),
            presentAddress: $('#presentAddressId').val(),
            permanentAddress: $('#permanentAddressId').val(),
            phoneNo: $('#phoneNoId').val(),
            email: $('#emailId').val(),
            nid: $('#nid').val(),
            joiningDateString: $('#joiningDateId input').val(),
            salary: $('#salaryId').val(),
            isActive: $('#isActive').is(":checked")
        }

        $.post('/CareTaker/InsertOrUpdateCareTaker', inputData, function (data) {
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
            } else {
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
    $('#careTakerId').val('0');
    $('#careTakerNameId').val('');
    $('#presentAddressId').val('');
    $('#permanentAddressId').val('');
    $('#phoneNoId').val('');
    $('#emailId').val('');
    $('#nid').val('');
    $('#joiningDateId input').val('');
    $('#salaryId').val('');
    $('#isActive').prop('checked', true);
    $('.switchery').trigger('click');
}
function loadCreateUI() {
    $('#careTakerFormId').show();
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#careTakerListId').hide();
    $('#btnCreateId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
    $('#joiningDateId').datetimepicker({
        useCurrent: false,
        format: 'DD/MM/YYYY'
    });
}

function EditCareTaker(input) {
    $.get('/CareTaker/GetCareTakerById/' + input, function (data) {
        if (data.success) {
            loadCreateUI();
            $('#btnClearId').hide();
            $('#btnCreateId').show();
            $('#careTakerId').val(data.data.careTakerId);
            $('#careTakerNameId').val(data.data.careTakerName);
            $('#presentAddressId').val(data.data.presentAddress);
            $('#permanentAddressId').val(data.data.permanentAddress);
            $('#phoneNoId').val(data.data.phoneNo)
            $('#emailId').val(data.data.email);
            $('#nid').val(data.data.nid);
            $('#joiningDateId input').val(data.data.joiningDateString);
            $('#salaryId').val(data.data.salary);
            $('#isActive').prop('checked', data.data.isActive);
            $('.switchery').trigger('click');
            $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Update');
        }
    })
}