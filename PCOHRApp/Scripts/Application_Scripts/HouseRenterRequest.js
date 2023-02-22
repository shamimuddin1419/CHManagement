(() => {
    $(document).ready(() => {
        loadInitialization();
    });

    const loadInitialization = () => {
        $('#messageBoxId').hide();
        $('#errorMessageBoxId').hide();
        $('#successMessageBoxId').hide();
        $('#customerRequestListId').hide();
        $('#btnCreateId').hide();
        $('#btnSearchId').hide();
        $('#btnSaveId').show();
        $('#btnClearId').show();
        $('#btnListId').show();
        $('.customerRequestForm').show();
        populateProjectDropDown();
        loadRequestTypes();
        loadYears();
    };

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
    };

    const loadHouses = projectId => {
        let houses = [];
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
                loadCurrentRenterInfo(data.id);
            });
        });
    }
    const clearCurrentRenterInfo = () => {
        $('#renterHouseId').val('');
        $('#houseTypeId').val('');
        $('#monthlyRentId').val('');
        $('#renterNameId').val('');
        $('#renterPhoneNoId').val('');
        $('#caretakerNameId').val('');
        $('#connectionMonthId').val('');
        $('#renterHouseId').val('');
        $('#renterNId').val('');
    }
    const loadCurrentRenterInfo = houseId => {
        try {
            clearCurrentRenterInfo();
            $.get(`/HouseRentBillGenerate/GetCurrentHouseRenterByHouse?houseId=${houseId}`, data => {
                if (data.success) {
                    $('#renterHouseId').val(data.data.renterHouseId);
                    $('#houseTypeId').val(data.data.houseType);
                    $('#monthlyRentId').val(data.data.currentRentAmount);
                    $('#renterNameId').val(data.data.renterName);
                    $('#renterPhoneNoId').val(data.data.enterPhoneNo);
                    $('#caretakerNameId').val(data.data.caretakerName);
                    $('#connectionMonthId').val(data.data.connectionMonth);
                    $('#renterNId').val(data.data.renterNID);
                }
                else {
                    toastr.error(data.message);
                }
            });
        }
        catch (ex) {
            toastr.error('Unknown error occured');
        }
    }
    const loadRequestTypes = () => {
        let requestTypeList = [];
        $.get('/Dropdown/GetCustomerRequestTypeList?requestTypeGroup=House', function (data) {
            requestTypeList = data.data;
            $('#requestTypeId').select2({
                data: requestTypeList
            });
        });
        $('#requestTypeId').on('select2:select', function (e) {
            let text = e.params.data.text;
            if (text == 'Rent Upgrade') {
                $('.updatedMonthlyRentFormGroup').show();
                $('.disconnectRow').hide();
            }
            else {
                $('.updatedMonthlyRentFormGroup').hide();
                $('.disconnectRow').show();
            }
        });

    }
    const loadYears = () => {
        let years = [];
        $.get('/Dropdown/GetYearList', function (data) {
            years = data.data;
            $('#requestYearId').select2({
                data: years
            });
        });
    }
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

    const loadInitHouses = () => {
        let select = $("#houseId");
        select.empty().trigger('change');
        select.append($("<option>").val(0).text("Select House"));
    };
    const clearUI = () => {
        $('#requestMonthId').val('0').trigger('change');
        $('#requestYearId').val('0').trigger('change');
        $('#remarksId').val('');
        $('#projectId').val('0');
        $('#requestTypeId').val('0');
        $('#updatedMonthlyBillId').val('');
        $('#projectId').trigger('change');
        loadInitHouses();
        $('#requestTypeId').trigger('change');
        $('.updatedMonthlyRentFormGroup').hide();
        clearCurrentRenterInfo();

    }

    const loadCustomerRequestDatatable = () => {
        var table = $('#customerRequestListTableId').DataTable();
        table.destroy();
        $('#customerRequestListTableId').DataTable({
            "ajax":
            {
                "url": '/HouseRenterRequest/GetRequestList',
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
                            return `<button type="button"  title ="delete" class="edituser btn info"><i class="fa fa-remove" id="deleteRequest_${data.requestId}" ></i></button>`
                        }

                    },
                    { "data": "houseName", "autoWidth": true  },
                    { "data": "renterName", "autoWidth": true },
                    { "data": "renterNID", "autoWidth": true },
                    { "data": "requestName", "autoWidth": true },
                    { "data": "requestCharge", "autoWidth": true },
                    { "data": "updatedMonthlyRent", "autoWidth": true },
                    { "data": "requestMonth", "autoWidth": true },
                    { "data": "requestYearName", "autoWidth": true},
                    { "data": "remarks", "autoWidth": true }




                ],
            "serverSide": true,
            "order": [1, "asc"],
            "processing": "true",
            "language": {
                "processing": "processing... please wait"
            },
            "initComplete": function (oSettings) {
                $("[id^='deleteRequest']").click(event => {
                    let targetId = event.target.id;
                    let requestId = targetId.substr(targetId.indexOf('_') + 1);
                    deleteRequest(requestId);
                });
            }

        });
    }
    const deleteRequest = id => {
        if (confirm("Are you sure want delete the Request?") == true) {
            $.get('/HouseRenterRequest/DeleteRequest/' + id, function (data) {
                if (data.success) {
                    
                    loadCustomerRequestDatatable();
                    showSuccessMessage(data.message);
                }
                else if (data.success == false) {
                    showErrorMessage(data.message);
                }
                else {
                    showErrorMessage("Something error occured, Refresh the page and try again!");
                }
            }).fail(function (response) {
                showErrorMessage(response.responseText);
            });
        }
    }

    $('#btnSaveId').click(() => {
        let text = $("#requestTypeId option:selected").text();
        if ($('#renterHouseId').val() == '') {
            toastr.error('Selected house does not have any active renter');
        }
        else if ($('#requestTypeId').val() == '0') {
            showErrorMessage('Provide Request Type!!');
        }
        else if (text == 'Discontinue' && $('#requestMonthId').val() == 0) {
            showErrorMessage('Provide Month!!');
        }
        else if (text == 'Discontinue' && $('#requestYearId').val() == 0) {
            showErrorMessage('Provide Year!!');
        }
        else if (text == 'Rent Upgrade' && $('#updatedMonthlyBillId').val() == '') {
            showErrorMessage('Provide New Monthly Rent!!');
        }
        else {
            let inputData = {};
            if (text == 'Discontinue') {
                inputData = {
                    renterHouseId: $('#renterHouseId').val(),
                    requestTypeId: $('#requestTypeId').val(),
                    remarks: $('#remarksId').val(),
                    requestMonth: $('#requestMonthId').val(),
                    requestYear: $('#requestYearId').val()
                }
            }
            else if (text == 'Rent Upgrade') {
                inputData = {
                    renterHouseId: $('#renterHouseId').val(),
                    requestTypeId: $('#requestTypeId').val(),
                    updatedMonthlyRent: $('#updatedMonthlyBillId').val(),
                    remarks: $('#remarksId').val()
                }
            }
            $.post('/HouseRenterRequest/InsertRequest', inputData, (data) => {
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
                }
                else {
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
    $('#btnListId').click( () => {
        $('#customerRequestListId').show();
        $('#messageBoxId').hide();
        $('.customerRequestForm').hide();
        $('#btnClearId').hide();
        $('#btnSaveId').hide();
        $('#btnCreateId').show();
        $('#btnListId').hide();
        loadCustomerRequestDatatable();
    });

    $('#btnCreateId').click(function () {
        loadInitialization();
        clearUI();
    });
})();