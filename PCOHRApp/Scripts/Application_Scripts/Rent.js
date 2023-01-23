$(document).ready(function () {
    loadCreateUI();
});

let loadCreateUI = () => {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#btnCreateId').hide();
    $('#btnSaveId').show();
    $('#btnClearId').show();
    loadRenter();
    loadProjects();
    loadYears();
}
let loadRenter = () => {
    $('#renterId').select2({
        ajax: {
            url: '/Renter/GetRenterListForDropdown',
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
let loadYears = () => {
    var connYears = [];
    $.get('/Dropdown/GetYearList', function (data) {
        connYears = data.data;
        $('#rentFromYearId').select2({
            data: connYears
        });
    });
}
let loadProjects = () => {
    let projects = [];
    $.get('/Project/GetProjectListForDropdown', function (data) {
        projects = data.data;
        $('#projectId').select2({
            data: projects
        });
        
    });
}
let loadAvailableHouseList = (projectId) => {
    let unAvailableList = [];
    $('#houseId').html('<option value ="0">Select House</option>');
    $.get(`/Rent/GetAvailableRentForDropdown?projectId=${projectId.value}`, function (data) {
        unAvailableList = data.data;
        $('#houseId').select2({
            data: unAvailableList
        });
        $('#houseAvailableMsgId').text('');
    });
}
let checkHouseAvailability = (houseId) => {
    $.get(`/Rent/GetAvailabilityInfo?houseId=${houseId.value}`).
        then((data) => {
            if (data.results.currentAvailability == 'Available') {
                $('#houseAvailableMsgId').removeClass('errorLabel').addClass('successLabel');
            }
            else {
                $('#houseAvailableMsgId').removeClass('successLabel').addClass('errorLabel');
            }
            $('#houseAvailableMsgId').text(data.results.currentAvailability);
            $('#rentAmountId').val(data.results.monthlyRent);
        });
}

$('#btnSaveId').click(() => {
    let inputData = {
        renterId: $('#renterId').val(),
        houseId: $('#houseId').val(),
        rentFromMonth: $('#rentFromMonthId').val(),
        rentFromYear: $('#rentFromYearId').val(),
        currentRentAmount: $('#rentAmountId').val(),
        advanceAmount: $('#advanceAmountId').val()
    };
    $.post('/Rent/InsertRentHouse', inputData, (data) => {
        if (data.success == true) {
            showSuccessMessage(data.message);
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
});
$('#btnClearId').click(() => {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    $('#errorMessageBoxId').val('');
    $('#successMessageBoxId').val('');
    clearUI();
});

let showErrorMessage = (errorText) =>{
    $('#messageBoxId').show();
    $('#errorMessageBoxId').show();
    $('#successMessageBoxId').hide();
    $('#errorMessageBoxId').html(errorText);
    $('#messageBoxId').focus();
};
let  showSuccessMessage = (successText)=> {
    $('#messageBoxId').show();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').show();
    $('#successMessageBoxId').html(successText);
    $('#messageBoxId').focus();
};

let clearUI = () => {
    $('#renterId').val('');
    $('#houseId').val('');
    $('#rentFromMonthId').val('0');
    $('#rentFromYearId').val('0');
    $('#rentAmountId').val('');
    $('.select2').trigger('change');
}