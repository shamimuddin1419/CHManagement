$(document).ready(function () {
    loadCreateUI();
});

$('#btnSaveId').click(function () {
    if ($('#projectNameId').val() == '') {
        showErrorMessage('Provide Project Name!!');
    }
    else if ($('#projectTypeId').val() == '0') {
        showErrorMessage('Provide Project Type!!');
    }
    

    else {
        var inputData = {
            projectId: $('#projectId').val(),
            projectName: $('#projectNameId').val(),
            projectType: $('#projectTypeId').val(),
            projectAddress: $('#projectAddressId').val(),
            projectDescription: $('#projectDescriptionId').val(),
        }

        var careTakers = getLoadedCareTakers();


        inputData.careTakerIds = careTakers;

        //if ($('#btnSaveId').text().indexOf('Save') >= 0) {
        //    inputData.insertFlag = 1;
        //}
        //else {
        //    inputData.insertFlag = 2;
        //}
        jQuery.ajaxSettings.traditional = true;
        $.post('/Project/InsertOrUpdateProject', inputData, function (data) {
            if (data.success == true) {
                if ($('#btnSaveId').text().indexOf('Save') >= 0) {
                    showSuccessMessage("Data Saved");
                }
                else {
                    showSuccessMessage("Data Updated");
                }

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

$('#btnListId').click(function () {
    $('#projectListId').show();
    $('#messageBoxId').hide();
    $('.projectForm').hide();
    $('#btnClearId').hide();
    $('#btnSaveId').hide();
    $('#btnCreateId').show();
    $('#btnListId').hide();
    var table = $('#projectListTableId').DataTable();
    table.destroy();
    $('#projectListTableId').DataTable({
        "ajax":
            {
                "url": '/Project/GetProjectList',
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
                    return '<button type="button" onclick = "EditProject(' + data.projectId + ')" class="edituser btn info"><i class="fa fa-edit"></i></a>'
                }

            },
            { "data": "projectName", "autoWidth": true },
            { "data": "projectAddress", "autoWidth": true },
            { "data": "projectType", "autoWidth": true },
            { "data": "apartmentBuildingType", "autoWidth": true },
        ],
        "serverSide": true,
        "order": [1, "asc"],
        "processing": "true",
        "language": {
            "processing": "processing... please wait"
        },


    });
})


function loadCreateUI() {

    $('.projectForm').show();
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#projectListId').hide();
    $('#btnCreateId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
    $('#apartmentTypeGroupId').hide();
    $('#careTakerId').select2({
        ajax: {
            url: '/CareTaker/GetCareTakerListForDropdown',
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

$('#careTakerAddId').click(function () {
    if ($('#careTakerId').val() == '0') {
        showErrorMessage('Provide Care Taker!!')
    }
    
    else
    {
        var careTakers = getLoadedCareTakers();
        if (careTakers.indexOf($('#careTakerId').val()) != -1) {
            showErrorMessage('This Care Taker is already added!!')
        }
        else {
            $.get('/CareTaker/GetCareTakerById/' + $('#careTakerId').val(), function (data) {
                if (data.success) {
                    var tag = '<tr><td class = "careTakerTdId" hidden>' + data.data.careTakerId + '</td><td>' + data.data.careTakerName + '</td><td>' + data.data.phoneNo + '</td><td>' + data.data.permanentAddress + '<td><button type="button" onclick=onDelete(this) class="edituser btn info"><i class="fa fa-remove"></i></button>'
                    $('#careTakerBodyId').append(tag);

                }
            });
            $('#careTakerId').val('0');
            $('.select2').trigger('change');
        }
    }
})

$('#btnCreateId').click(function () {
    loadCreateUI();
    clearUI();
    $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Save');
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

function onDelete(element) {
    element.parentElement.parentElement.remove();
}
function clearUI() {
    $('#projectId').val('0');
    $('#projectNameId').val('');
    $('#projectTypeId').val('0');
    $('#apartmentTypeId').val('0');
    $('#projectAddressId').val('');
    $('#projectDescriptionId').val('');
    $('.select2').trigger('change');
    $('#apartmentTypeGroupId').hide();
    $('#careTakerBodyId').empty();
}

function EditProject(id) {
    $.get('/Project/GetProjectById/' + id, function (data) {
        if (data.success) {
            loadCreateUI();
            $('#btnClearId').hide();
            $('#btnCreateId').show();
            $('#projectId').val(data.data.projectId);

            $('#projectNameId').val(data.data.projectName);
            $('#projectAddressId').val(data.data.projectAddress);
            $('#projectDescriptionId').val(data.data.projectDescription);
            $('#projectTypeId').val(data.data.projectType);
            if ($('#projectTypeId').val() == 'Apartment') {
                $('#apartmentTypeId').val(data.data.apartmentBuildingType);
                $('#apartmentTypeGroupId').show();
            }
            else {
                $('#apartmentTypeGroupId').hide();
            }
            $('.select2').trigger('change');
            $('#careTakerBodyId').empty();
            data.data.careTakers.forEach(function (item, index, arr) {
                var tag = '<tr><td class = "careTakerTdId" hidden>' + item.careTakerId + '</td><td>' + item.careTakerName + '</td><td>' + item.phoneNo + '</td><td>' + item.permanentAddress + '<td><button type="button" onclick=onDelete(this) class="edituser btn info"><i class="fa fa-remove"></i></button>'
                $('#careTakerBodyId').append(tag)
            })

            $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Update');
        }
    });
}

function getLoadedCareTakers() {
    var careTakers = [];
    var i = 0;
    $('.careTakerTdId').each(function () {
        careTakers[i] = $(this).text();
        i++;
    });
    return careTakers;
}
//comment
