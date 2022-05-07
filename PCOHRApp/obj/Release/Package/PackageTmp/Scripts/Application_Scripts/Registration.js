
$(document).ready(function () {
    loadCreateUI();
});

$('#btnListId').click(function () {
    $('#userListId').show();
    $('#messageBoxId').hide();
    $('#registrationFormId').hide();
    $('#btnClearId').hide();
    $('#btnSaveId').hide();
    $('#btnCreateId').show();
    $('#btnListId').hide();
    var table = $('#userListTableId').DataTable();
    table.destroy();
    $('#userListTableId').DataTable({
        "ajax": '/Account/GetUserList',
        "responsive": true,

        "columns":
        [
            {
                "data": null,
                'width': '5%',
                "className": "center",
                render: function (data, type, row) {
                    return '<button type="button" onclick = "EditUser(' + data.userId + ')" class="edituser btn info"><i class="fa fa-edit"></i></a>'
                }

            },
            { "data": "userFullName", "autoWidth": true },
            { "data": "userName", "autoWidth": true },
            { "data": "designationName", "autoWidth": true },
            { "data": "phoneNumber", "autoWidth": true },
            { "data": "email", "autoWidth": true },
            { "data": "isAdmin", "autoWidth": true },
            { "data": "isCableUser", "autoWidth": true },
            { "data": "isHouseRentUser", "autoWidth": true },
            { "data": "isActive", "autoWidth": true }

        ],

    });

})


$('#btnCreateId').click(function () {
    loadCreateUI();
    $('#userId').val('0'),
    $('#passwordId').val(''),
    $('#userNameId').val(''),
    $('#userEmailId').val(''),
    $('#contactNumberId').val(''),
    $('#addressId').val(''),
    $('#isAdminId').prop('checked', false),
    $('#isCableUser').prop('checked', false),
    $('#isHouseUser').prop('checked', false),
    $('#isActive').prop('checked', true),
    $('.switchery').trigger('click');

    $('#userDesignationId').val('0'),
    $('#userFullNameId').val('');
    $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Save');
})

$('#btnSaveId').click(function () {
    if ($('#userFullNameId').val() == '') {
        showErrorMessage('Provide User Full Name!!');
    }
    else if ($('#userDesignationId').val() == null || $('#userDesignationId').val()=='0') {
        showErrorMessage('Provide Designation!!');
    }
    else if ($('#userNameId').val() == '') {
        showErrorMessage('Provide User Name!!');
    }
    else if ($('#passwordId').val() == '') {
        showErrorMessage('Provide Password!!');
    }
    else if (!$('#isCableUser').is(":checked") && !$('#isHouseUser').is(":checked")) {
        showErrorMessage('You have to register an User as Cable Operator User or House Rent User!!');
    }
    else {
        var inputData = {
            userId: $('#userId').val(),
            password: $('#passwordId').val(),
            userName: $('#userNameId').val(),
            email: $('#userEmailId').val(),
            phoneNumber: $('#contactNumberId').val(),
            address: $('#addressId').val(),
            isAdmin: $('#isAdminId').is(":checked"),
            isCableUser: $('#isCableUser').is(":checked"),
            isHouseRentUser: $('#isHouseUser').is(":checked"),
            designationId: $('#userDesignationId').val(),
            userFullName: $('#userFullNameId').val(),
            isActive: $('#isActive').is(":checked"),
        }

        $.post('/Account/InsertOrUpdateUser', inputData, function (data) {
            if (data.success == true) {
                showSuccessMessage(data.message);
                if ($('#btnSaveId').text().indexOf('Update') >= 0)
                {
                    $('#btnCreateId').click();
                    $('#messageBoxId').show();
                    $('#successMessageBoxId').show();
                }
                else
                {
                    clearUI();
                }
                
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

    $('#userId').val("0");
    $('#passwordId').val("");
    $('#userNameId').val(''),
    $('#userEmailId').val(''),
    $('#contactNumberId').val(''),
    $('#addressId').val(''),
    $('#isAdminId').val("off"),
    $('#isCableUser').val("off"),
    $('#isHouseUser').val("off"),
    $('#userDesignationId').val('0'),
    $('#userFullNameId').val('')
    $('.select2').trigger('change');
    $('#isAdminId').prop('checked', false),
    $('#isCableUser').prop('checked', false),
    $('#isHouseUser').prop('checked', false),
    $('#isActive').prop('checked', true),
    $('.switchery').trigger('click');
}
function loadCreateUI() {
    $('#registrationFormId').show();
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#userListId').hide();
    $('#btnCreateId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    $('#btnListId').show();
    var designations = []
    $.get('/Designation/GetDesignationList', function (data) {
        designations = data.data;
        $('#userDesignationId').select2({
            data: designations
        });
    });
}

function EditUser(input) {
    $.get('/Account/GetUserById/' + input, function (data) {
        if (data.success) {
            loadCreateUI();
            $('#btnClearId').hide();
            $('#btnCreateId').show();
            $('#userId').val(data.data.userId),
            $('#passwordId').val(data.data.password),
            $('#userNameId').val(data.data.userName),
            $('#userEmailId').val(data.data.email),
            $('#contactNumberId').val(data.data.phoneNumber),
            $('#addressId').val(data.data.address),
            $('#isAdminId').prop('checked', data.data.isAdmin),
            $('#isCableUser').prop('checked', data.data.isCableUser),
            $('#isHouseUser').prop('checked', data.data.isHouseRentUser),
            $('#isActive').prop('checked', data.data.isActive),
            $('.switchery').trigger('click');

            $('#userDesignationId').val(data.data.designationId),
            $('.select2').trigger('change')
            $('#userFullNameId').val(data.data.userFullName);
            $('#btnSaveId').html('<i class="fa fa-check-square-o"></i> Update');
        }
    })
}