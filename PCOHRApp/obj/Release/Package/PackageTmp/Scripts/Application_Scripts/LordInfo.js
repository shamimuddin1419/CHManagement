
$(document).ready(function () {
    loadCreateUI();
});

$('#btnListId').click(function () {
    $('#lordInfoListId').show();
    $('#messageBoxId').hide();
    $('#lordInfoFormId').hide();
    $('#btnClearId').hide();
    $('#btnSaveId').hide();
    $('#btnCreateId').show();
    $('#btnListId').hide();
    var table = $('#lordInfoListTableId').DataTable();
    table.destroy();
    $('#lordInfoListTableId').DataTable({
        //"ajax": '/Host/GetHostList',
        "ajax":
            {
                "url": '/LordInfo/GetLordInfoList',
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
                    return '<button type="button" onclick = "EditLordInfo(' + data.lordId + ')" class="edituser btn info"><i class="fa fa-edit"></i></a>'
                }

            },
            { "data": "ownerName" },
            { "data": "companyName"},
            { "data": "companyAddress"},
            { "data": "phoneNo" },
            { "data": "email" },
            { "data": "nid" },

        ],
        //"serverSide": true,
        //"order": [1, "asc"],
        //"processing": "true",
        //"language": {
        //    "processing": "processing... please wait"
        //},

    });
})


$('#btnCreateId').click(function () {
    loadCreateUI();
    clearUI();
    $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Save');
})

$('#btnSaveId').click(function () {
    if ($('#ownerNameId').val() == '') {
        showErrorMessage('Provide Owner Name!!');
    }
    else if ($('#companyNameId').val() == '') {
        showErrorMessage('Provide Company Name!!');
    }
    else if ($('#companyAddressId').val() == '') {
        showErrorMessage('Provide Company Address!!');
    }
    else {
        var inputData = {
            lordId: $('#lordId').val(),
            ownerName: $('#ownerNameId').val(),
            companyName: $('#companyNameId').val(),
            companyAddress: $('#companyAddressId').val(),
            phoneNo: $('#phoneNumberId').val(),
            email: $('#emailId').val(),
            nid: $('#nid').val()
        }

        $.post('/LordInfo/InsertOrUpdateLordInfo', inputData, function (data) {
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
    $('#lordId').val('0');
    $('#ownerNameId').val('');
    $('#companyNameId').val('');
    $('#companyAddressId').val('');
    $('#phoneNumberId').val('');
    $('#emailId').val('');
    $('#nid').val('');
}
function loadCreateUI() {
    $('#lordInfoFormId').show();
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#lordInfoListId').hide();
    $('#btnCreateId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
}

function EditLordInfo(input) {
    $.get('/LordInfo/GetLordInfoById/' + input, function (data) {
        if (data.success) {
            loadCreateUI();
            $('#btnClearId').hide();
            $('#btnCreateId').show();
            $('#lordId').val(data.data.lordId);
            $('#ownerNameId').val(data.data.ownerName);
            $('#companyNameId').val(data.data.companyName);
            $('#companyAddressId').val(data.data.companyAddress);
            $('#phoneNumberId').val(data.data.phoneNo)
            $('#emailId').val(data.data.email);
            $('#nid').val(data.data.nid);

            $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Update');
        }
    })
}