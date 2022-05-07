
$(document).ready(function () {
    loadCreateUI();
});

$('#btnListId').click(function () {
    $('#zoneListId').show();
    $('#messageBoxId').hide();
    $('#zoneFormId').hide();
    $('#btnClearId').hide();
    $('#btnSaveId').hide();
    $('#btnCreateId').show();
    $('#btnListId').hide();
    var table = $('#zoneListTableId').DataTable();
    table.destroy();
    $('#zoneListTableId').DataTable({
        "ajax": '/Zone/GetZoneList',
        "responsive": true,

        "columns":
        [
            {
                "data": null,
                'width': '5%',
                "className": "center",
                render: function (data, type, row) {
                    return '<button type="button" onclick = "EditZone(' + data.zoneId + ')" class="edituser btn info"><i class="fa fa-edit"></i></a>'
                }

            },
            { "data": "zoneName", "autoWidth": true },
            { "data": "isActive", "autoWidth": true }

        ],
        columnDefs:
            [
                    {
                        "targets": 2,
                        "orderable": false,
                        "width": "10%",
                        checkboxes: {
                            selectRow: true
                        }
                    },
            ]

    });
})


$('#btnCreateId').click(function () {
    loadCreateUI();
    $('#zoneId').val('0'),
    $('#zoneNameId').val(''),
    $('#zonePhoneNumberId').val(''),
    $('#zoneAddressId').val(''),
    $('#isActive').prop('checked', true),
    $('.switchery').trigger('click')
    $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Save');
})

$('#btnSaveId').click(function () {
    if ($('#zoneNameId').val() == '') {
        showErrorMessage('Provide Zone Name!!');
    }
    else if ($('#zonePhoneNumberId').val() == '') {
        showErrorMessage('Provide Phone Number!!');
    }
    else if ($('#zoneAddressId').val() == '') {
        showErrorMessage('Provide Address!!');
    }
    else {
        var inputData = {
            zoneId: $('#zoneId').val(),
            zoneName: $('#zoneNameId').val(),
            zonePhone: $('#zonePhoneNumberId').val(),
            zoneAddress: $('#zoneAddressId').val(),
            isActive: $('#isActive').is(":checked"),
        }

        $.post('/Zone/InsertOrUpdateZone', inputData, function (data) {
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

    $('#zoneId').val("0");
    $('#zoneNameId').val("");
    $('#zonePhoneNumberId').val('');
    $('#zoneAddressId').val('');
    $('#isActive').prop('checked', true);
    $('.switchery').trigger('click')
}
function loadCreateUI() {
    $('#zoneFormId').show();
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#zoneListId').hide();
    $('#btnCreateId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
}

function EditZone(input) {
    $.get('/Zone/GetZoneById/' + input, function (data) {
        if (data.success) {
            loadCreateUI();
            $('#btnClearId').hide();
            $('#btnCreateId').show();
            $('#zoneId').val(data.data.zoneId),
            $('#zoneNameId').val(data.data.zoneName),
            $('#zonePhoneNumberId').val(data.data.zonePhone),
            $('#zoneAddressId').val(data.data.zoneAddress),
            $('#isActive').prop('checked', data.data.isActive),
            $('.switchery').trigger('click');

            $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Update');
        }
    })
}