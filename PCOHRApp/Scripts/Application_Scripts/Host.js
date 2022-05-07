
$(document).ready(function () {
    loadCreateUI();
});

$('#btnListId').click(function () {
    $('#hostListId').show();
    $('#messageBoxId').hide();
    $('#hostFormId').hide();
    $('#btnClearId').hide();
    $('#btnSaveId').hide();
    $('#btnCreateId').show();
    $('#btnListId').hide();
    var table = $('#hostListTableId').DataTable();
    table.destroy();
    $('#hostListTableId').DataTable({
        //"ajax": '/Host/GetHostList',
        "ajax":
            {
                "url": '/Host/GetHostList',
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
                    return '<button type="button" onclick = "EditHost(' + data.hostId + ')" class="edituser btn info"><i class="fa fa-edit"></i></a>'
                }

            },
            { "data": "hostName", 'width': '20%' },
            { "data": "hostPhone", 'width': '20%' },
            { "data": "hostAddress", 'width': '50%' },
            { "data": "isActiveString", 'width': '5%' }

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
    $('#hostId').val('0'),
    $('#hostNameId').val(''),
    $('#hostPhoneNumberId').val(''),
    $('#hostAddressId').val(''),
    $('#isActive').prop('checked', true),
    $('.switchery').trigger('click')
    $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Save');
})

$('#btnSaveId').click(function () {
    if ($('#hostNameId').val() == '') {
        showErrorMessage('Provide Host Name!!');
    }
    else if ($('#hostPhoneNumberId').val() == '') {
        showErrorMessage('Provide Phone Number!!');
    }
    else if ($('#hostAddressId').val() == '') {
        showErrorMessage('Provide Address!!');
    }
    else {
        var inputData = {
            hostId: $('#hostId').val(),
            hostName: $('#hostNameId').val(),
            hostPhone: $('#hostPhoneNumberId').val(),
            hostAddress: $('#hostAddressId').val(),
            isActive: $('#isActive').is(":checked"),
        }

        $.post('/Host/InsertOrUpdateHost', inputData, function (data) {
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

    $('#hostId').val("0");
    $('#hostNameId').val("");
    $('#hostPhoneNumberId').val('');
    $('#hostAddressId').val('');
    $('#isActive').prop('checked', true);
    $('.switchery').trigger('click')
}
function loadCreateUI() {
    $('#hostFormId').show();
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#hostListId').hide();
    $('#btnCreateId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
}

function EditHost(input) {
    $.get('/Host/GetHostById/' + input, function (data) {
        if (data.success) {
            loadCreateUI();
            $('#btnClearId').hide();
            $('#btnCreateId').show();
            $('#hostId').val(data.data.hostId),
            $('#hostNameId').val(data.data.hostName),
            $('#hostPhoneNumberId').val(data.data.hostPhone),
            $('#hostAddressId').val(data.data.hostAddress),
            $('#isActive').prop('checked', data.data.isActive),
            $('.switchery').trigger('click');

            $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Update');
        }
    })
}