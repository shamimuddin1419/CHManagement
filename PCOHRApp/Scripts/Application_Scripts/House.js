$(document).ready(function () {
    loadCreateUI();
});
function loadCreateUI() {
    $('.houseForm').show();
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#houseListId').hide();
    $('#btnCreateId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
    var projects = [];
    $.get('/Project/GetProjectListForDropdown', function (data) {
        projects = data.data;
        $('#projectId').select2({
            data: projects
        });
    });
}

function loadHouseList() {
    var table = $('#projectBasedHouseTableId').DataTable();
    if ($('#projectId').val() == '0') {
        table.clear().draw();
    }
    table.destroy();
    if ($('#projectId').val() != '0') {
        $('#projectBasedHouseTableId').DataTable({
            "ajax": '/House/GetHousesByProjectId/' + $('#projectId').val(),
            "responsive": true,

            "columns":
            [
                { "data": "houseName", "autoWidth": true },
                { "data": "meterNo", "autoWidth": true },
                { "data": "houseType", "autoWidth": true }

            ]
        });
    }
}
$('#btnCreateId').click(function () {
    loadCreateUI();
    clearUI();
})

$('#btnSaveId').click(function () {
    if ($('#projectId').val() == '0') {
        showErrorMessage('Provide Project!!');
    }
    else if ($('#houseNameId').val() == '') {
        showErrorMessage('Provide House Name!!');
    }
    else if ($('#meterNoId').val() == '') {
        showErrorMessage('Provide Meter Number');
    }
    else if ($('#houseTypeId').val() == '') {
        showErrorMessage('Provide House Type!!');
    }
    else if ($('#monthlyRentId').val() == '') {
        showErrorMessage('Provide House Rent!!');
    }
    else {
        var inputData = {
            houseId: $('#houseId').val(),
            houseName: $('#houseNameId').val(),
            meterNo: $('#meterNoId').val(),
            projectId: $('#projectId').val(),
            monthlyRent: $('#monthlyRentId').val(),
            description: $('#houseDescriptionId').val(),
            houseType: $('#houseTypeId').val()
        }

        $.post('/House/InsertOrUpdateHouse', inputData, function (data) {
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
});
function showErrorMessage(errorText) {
    $('#messageBoxId').show();
    $('#errorMessageBoxId').show();
    $('#successMessageBoxId').hide();
    $('#errorMessageBoxId').html(errorText);
    $('#messageBoxId').focus();
};
function showSuccessMessage(successText) {
    $('#messageBoxId').show();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').show();
    $('#successMessageBoxId').html(successText);
    $('#messageBoxId').focus();
};
$('#btnListId').click(function () {
    $('#houseListId').show();
    $('#messageBoxId').hide();
    $('.houseForm').hide();
    $('#btnClearId').hide();
    $('#btnSaveId').hide();
    $('#btnCreateId').show();
    $('#btnListId').hide();
    var table = $('#houseListTableId').DataTable();
    table.destroy();
    $('#houseListTableId').DataTable({
        "ajax": '/House/GetHousesByProjectId/0',
        "responsive": true,

        "columns":
        [
            {
                "data": null,
                'width': '5%',
                "className": "center",
                render: function (data, type, row) {
                    return '<button type="button" onclick = "EditHouse(' + data.houseId + ')" class="edituser btn info"><i class="fa fa-edit"></i></a>'
                }

            },
            { "data": "projectName", "autoWidth": true },
            { "data": "houseName", "autoWidth": true },
            { "data": "meterNo", "autoWidth": true },
            { "data": "houseType", "autoWidth": true },
            { "data": "monthlyRent", "autoWidth": true }

        ]
     

    });
})
function EditHouse(input) {
    $.get('/House/GetHousesById/' + input, function (data) {
        if (data.success) {
            loadCreateUI();
            $('#btnClearId').hide();
            $('#btnCreateId').show();

            $('#houseId').val(data.data.houseId),
            $('#houseNameId').val(data.data.houseName),
            $('#meterNoId').val(data.data.meterNo),
            $('#houseTypeId').val(data.data.houseType),
            $('#projectId').val(data.data.projectId),
            $('#monthlyRentId').val(data.data.monthlyRent),
            $('#houseDescriptionId').val(data.data.description),
            
            $('.select2').trigger('change');

            $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Update');
        }
    })
}
function clearUI() {

    $('#houseId').val('0');
    $('#houseNameId').val('');
    $('#meterNoId').val('');
    $('#projectId').val('0');
    $('#monthlyRentId').val('0'),
    $('#houseDescriptionId').val(''),
    $('#houseTypeId').val();
    $('.select2').trigger('change');
};
