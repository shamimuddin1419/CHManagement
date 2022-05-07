$(document).ready(function () {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    
    $('#appTypeId').select2({
        placeholder:
        {
            id : -1,
            text : "Select a state"
        }
    })
});
$('#userLoginId').click(function () {
    if ($('#userNameId').val() == '')
    {
       showErrorMessage("Provide UserName")
    }
    else if ($('#passwordId').val() == '') {
        showErrorMessage("Provide Password")
    }
    else if ($('#appTypeId').val() == '0') {
        showErrorMessage("Provide User Type")
    }
    else {
        $.ajax({
            url: '/Account/Login',
            type: 'POST',
            data: JSON.stringify({ userName: $('#userNameId').val(), password: $('#passwordId').val(), userType: $('#appTypeId').val() }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.success == true) {
                    location.reload();
                }
                else {
                    showErrorMessage(data.message)
                }
            },
            error: function (request) {
                // ...
            }
        });
    }
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