$(document).ready(function () {
    loadInitialization();
});
function loadInitialization() {

    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#btnSearchId').hide();
    $('#btnSaveId').show();
    
   
}
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

$('#btnSaveId').click(function () {
    debugger;
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

    $.post('/BackUp/TakeBackup', function (data) {
        if (data.success == true) {
            showSuccessMessage(data.message);
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
});

