(function () {

    $(document).ready(() => {
        loadInitialization();
    });


    const loadInitialization = () => {
        $('#messageBoxId').hide();
        $('#errorMessageBoxId').hide();
        $('#successMessageBoxId').hide();
        $('#customerBillListId').hide();
        $('#btnCreateId').hide();
        $('#btnSearchId').hide();
        $('#btnSaveId').show();
        $('#btnClearId').show();
        $('#btnListId').show();
        $('.billGenerateForm').show();
        $('.individualCustomerDiv').hide();
        loadYears();
        loadEmptyIndividualCustomerDiv();
    };
    const loadEmptyIndividualCustomerDiv = () => {
        $('#houseTypeId').val('');
        $('#monthlyRentId').val('');
        $('#renterNameId').val('');
        $('#renterPhoneNoId').val('');
        $('#caretakerNameId').val('');
        $('#connectionMonthId').val('');
    }

    const loadYears = () => {
        let years = [];
        $.get('/Dropdown/GetYearList', function (data) {
            years = data.data;
            $('#yearId').select2({
                data: years
            });
        });
    }


    const populateProjectDropDown = () => {
        let projects = [];
        $.get('/Project/GetProjectListForDropdown', function (data) {
            projects = data.data;
            $('#projectId').select2({
                data: projects
            });

        });
        $('#projectId').on('select2:select', function (e) {
            let data = e.params.data;
            loadHouses(data.id);
        });
    }

    const loadHouses = projectId => {
        $.get(`/House/GetHouseListByProjectForDropdown/${projectId}`, function (data) {
            houses = data.data;
            let select = $("#houseId");

            select.empty().trigger('change');
            select.append($("<option>").val(0).text("Select House"));
            $(data.data).each(function (index, item) {
                select.append($("<option>").val(item.id).text(item.text));
            });
            $('#houseId').trigger('change');
            $('#houseId').on('select2:select', function (e) {
                let data = e.params.data;
                loadHouseRentInfo(data.id);
            });
        });
    }
    
    const loadHouseRentInfo = houseId => {
        loadEmptyIndividualCustomerDiv();
        if ($('#monthId').val() == '0' || $('#yearId').val() == '0') {
            toastr.error('Please provide Effective Month and Year and try again');
            return;
        }
        $.get(`/HouseRentBillGenerate/GetCurrentHouseRenterByHouse?houseId=${houseId}&effectiveMonth=${$('#monthId').val()}&effectiveYear=${$('#yearId').val()}`, data => {
            if (data.success) {
                $('#houseTypeId').val(data.data.houseType);
                $('#monthlyRentId').val(data.data.currentRentAmount);
                $('#renterNameId').val(data.data.renterName);
                $('#renterPhoneNoId').val(data.data.enterPhoneNo);
                $('#caretakerNameId').val(data.data.caretakerName);
                $('#connectionMonthId').val(data.data.connectionMonth);
                $('#renterHouseId').val(data.data.renterHouseId);
            }
            else {
                toastr.error(data.message);
            }
            
        });
    }


    const clearCustomerInfo = () => {
        $('#houseTypeId').val('');
        $('#monthlyBillId').val('');
        $('#renterNameId').val('');
        $('#renterPhoneNoId').val('');
        $('#renterNID').val('');
        $('#connectionMonthId').val('');
    }
    $('#isBatch').change(function () {
        if (this.checked) {
            $('.individualCustomerDiv').hide();
        }
        else {
            $('.individualCustomerDiv').show();
            populateProjectDropDown();
            //$('#projectId').val('0').trigger('change');
            clearCustomerInfo();
        }
    });

    $('#btnSaveId').click( () => {

        if ($('#monthId').val() == '0') {
            showErrorMessage('Provide Month!!');
        }
        else if ($('#yearId').val() == '0') {
            showErrorMessage('Provide Year!!');
        }
        else {
            if ($('#isBatch').is(":checked") == false && $('#houseId').val() == '0') {
                showErrorMessage('Provide House!!');
            }
            else {
                var inputData = {
                    isBatch: $('#isBatch').is(":checked"),
                    month: $('#monthId').val(),
                    year: $('#yearId').val(),
                    remarks: $('#remarksId').val()
                };
                if ($('#isBatch').is(":checked") == false) {
                    inputData.renterHouseId = $('#renterHouseId').val();
                }
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

                $.post('/HouseRentBillGenerate/InsertBillGenerate', inputData, function (data) {


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
                        showErrorMessage(data.message)
                    }
                    else {
                        showErrorMessage("Something error occured, Refresh the page and try again!");
                    }
                    $.unblockUI();
                }).fail(function (response) {
                    showErrorMessage(response.responseText);
                });
            }
        }
    });

    const showErrorMessage = errorText => {
        $('#messageBoxId').show();
        $('#errorMessageBoxId').show();
        $('#successMessageBoxId').hide();
        $('#errorMessageBoxId').html(errorText);
        $('#messageBoxId').focus();
    };

    const showSuccessMessage = successText => {
        $('#messageBoxId').show();
        $('#errorMessageBoxId').hide();
        $('#successMessageBoxId').show();
        $('#successMessageBoxId').html(successText);
        $('#messageBoxId').focus();
    }

})(jQuery);


