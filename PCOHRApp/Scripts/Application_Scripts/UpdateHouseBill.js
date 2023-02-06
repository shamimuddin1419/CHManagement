(() => {
    $(document).ready(() => {
        loadInitialization();
    });

    const loadInitialization = () => {
        $('#messageBoxId').hide();
        $('#errorMessageBoxId').hide();
        $('#successMessageBoxId').hide();
        $('#billListId').hide();
        $('#btnCreateId').hide();
        $('#btnSearchId').hide();
        $('#btnSaveId').show();
        $('#btnClearId').show();
        $('#btnListId').show();
        $('.updateBillCard').show();
        populateProjectDropDown();
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
    }

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
                loadUnupdatedHouseBill(data.id);
            });
        });
    }

    const loadUnupdatedHouseBill = houseId => {
        let bills = []
        $.get(`/HouseRentBillGenerate/GetUnupdatedBillForDropDown?houseId=${houseId}`, data => {
            bills = data.data;
            let select = $("#billDetailId");
            select.empty().trigger('change');
            select.append($("<option>").val(0).text("Select Bill Month"));
            $(data.data).each(function (index, item) {
                select.append($("<option>").val(item.id).text(item.text));
            });
            $('#billDetailId').trigger('change');
            $('#billDetailId').on('select2:select', function (e) {
                let data = e.params.data;
                loadBillDetails(data.id);
            });
        });
    }

    const loadBillDetails = billdetailId => {
        let bill = {};
        $.get(`/HouseRentBillGenerate/GetHouseBillInformation?billDetailId=${billdetailId}`, data => {
            bill = data.data;
            $('#unPaidMonthId').val(bill.unPaidMonth);

            $('#billAmountId').val(bill.billAmount);
            $('#gasChargeId').val(bill.gasCharge);
            $('#electricityChargeId').val(bill.electricityCharge);
            $('#waterChargeId').val(bill.waterCharge);
            $('#serviceChargeId').val(bill.serviceCharge);
            $('#otherChargeId').val(bill.otherCharge);
            $('#houseTypeId').val(bill.houseType);
            $('#houseNameId').val(bill.houseName);
            $('#meterNoId').val(bill.meterNo);
            $('#renterNameId').val(bill.renterName);
            $('#renterNID').val(bill.renterNID);
            $('#renterPhoneId').val(bill.renterPhone);
            $('#renterEmailId').val(bill.renterEmail);
            $('#rentStartFromId').val(bill.rentStartFrom);
            $('#rentEndToId').val(bill.rentEndTo);
            $('#renterEmergencyContactNameId').val(bill.renterEmergencyContactName);
            $('#renterEmergencyContactPhoneId').val(bill.renterEmergencyContactPhone);
        });
    }

    const clearUI = () => {
        $('#unPaidMonthId').val('');
        $('#billAmountId').val('');
        $('#gasChargeId').val('0');
        $('#electricityChargeId').val('0');
        $('#waterChargeId').val('0');
        $('#serviceChargeId').val('0');
        $('#otherChargeId').val('0');
        $('#houseTypeId').val('');
        $('#houseNameId').val('');
        $('#meterNoId').val('');
        $('#renterNameId').val('');
        $('#renterNID').val('');
        $('#renterPhoneId').val('');
        $('#renterEmailId').val('');
        $('#rentStartFromId').val('');
        $('#rentEndToId').val('');
        $('#renterEmergencyContactNameId').val('');
        $('#renterEmergencyContactPhoneId').val('');
        $('#projectId').val('0');
        $('#projectId').trigger('change');
        loadInitHouses();
        loadInitUnpaidBills();
    }

    const loadInitHouses = () => {
        let select = $("#houseId");
        select.empty().trigger('change');
        select.append($("<option>").val(0).text("Select House"));
    };

    const loadInitUnpaidBills = () => {
        let select = $("#billDetailId");
        select.empty().trigger('change');
        select.append($("<option>").val(0).text("Select Bill Month"));
    }

    $('#btnSaveId').click(() => {
        if ($('#billDetailId').val() == 0) {
            showErrorMessage('Please Provide Bill Information');
            return;
        }
        let inputData = {
            billDetailId: $('#billDetailId').val(),
            gasCharge: $('#gasChargeId').val(),
            electricityCharge: $('#electricityChargeId').val(),
            waterCharge: $('#waterChargeId').val(),
            serviceCharge: $('#serviceChargeId').val(),
            otherCharge: $('#otherChargeId').val()
        };
        $.post('/HouseRentBillGenerate/UpdateBillInfo', inputData, data => {
            if (data.success) {
                showSuccessMessage(`Bill Updated Successfully , Can Download Bill Now.`);
                $('#btnDownloadId').show();
            }
            else {
                showErrorMessage(data.message);
            }
        })
    });

    $('#btnDownloadId').click(() => {
        generateRDLC('PDF', $('#billDetailId').val());
        $('#btnDownloadId').hide();
        clearUI();
    });

    $('#btnListId').click(() => {
        $('#billListId').show();
        $('#messageBoxId').hide();
        $('.updateBillCard').hide();
        $('#btnClearId').hide();
        $('#btnSaveId').hide();
        $('#btnCreateId').show();
        $('#btnSearchId').show();

        $('#btnListId').hide();
        var yearList = [];
        $.get('/Dropdown/GetYearList', function (data) {
            yearList = data.data;
            $('#requestYearSearchId').select2({
                data: yearList
            });
        });
    });

    $('#btnSearchId').click(() => {
        if ($('#requestMonthSearchId').val() == '0') {
            showErrorMessage('Provide Month!!');
        }
        else if ($('#requestYearSearchId').val() == '0') {
            showErrorMessage('Provide Year!!');
        }
        else {
            let month = $('#requestMonthSearchId').val();
            let year = $('#requestYearSearchId').val();
            loadBillDatatable(month, year);
        }
    })
    $('#btnCreateId').click(() => {
        loadInitialization();
        clearUI();
    })
    $('#btnClearId').click(() => {
        $('#messageBoxId').hide();
        $('#errorMessageBoxId').hide();
        $('#successMessageBoxId').hide();
        $('#errorMessageBoxId').val('');
        $('#successMessageBoxId').val('');
        clearUI();
    });

    const loadBillDatatable = (month, year) => {
        var table = $('#billListTableId').DataTable();
        table.destroy();
        $('#billListTableId').DataTable({
            "ajax":
            {
                "url": '/HouseRentBillGenerate/GetUpdatedHouseBillInformation?month=' + month + '&year=' + year,
                "type": "POST",
                "datatype": "json"
            },
            "responsive": true,


            //"processing": true,
            //"serverSide": true,
            "columns":
                [
                    {
                        "data": null,
                        'width': '5%',
                        "className": "center",
                        render: function (data, type, row) {
                            return `<button type="button"  title ="delete" class="edituser btn info"><i class="fa fa-remove" id="deletebill_${data.billDetailId}" ></i></button>
                                    <button type="button"  title ="download" class="edituser btn info"><i class="fa fa-cloud-download" id="downloadbill_${data.billDetailId}"></i></button>`

                        }

                    },
                    { "data": "houseName", "autoWidth": true },
                    { "data": "renterName", "autoWidth": true },
                    { "data": "unPaidMonth", "autoWidth": true },
                    { "data": "billAmount", "autoWidth": true },
                    { "data": "gasCharge", "autoWidth": true },
                    { "data": "electricityCharge", "autoWidth": true },
                    { "data": "waterCharge", "autoWidth": true },
                    { "data": "serviceCharge", "autoWidth": true },
                    { "data": "otherCharge", "autoWidth": true }
                ],
            "serverSide": true,
            "order": [1, "asc"],
            "processing": "true",
            "language": {
                "processing": "processing... please wait"
            },
            "initComplete": function (oSettings) {
                $("[id^='deletebill']").click(event => {
                    let targetId = event.target.id;
                    let billDetailId = targetId.substr(targetId.indexOf('_') + 1);
                    deleteBill(billDetailId);
                });
                $("[id^='downloadbill']").click(event => {
                    let targetId = event.target.id;
                    let billDetailId = targetId.substr(targetId.indexOf('_') + 1);
                    downloadBill(billDetailId);
                });
            }


        });
        $('#messageBoxId').hide();
        $('#errorMessageBoxId').hide();
        $('#successMessageBoxId').hide();

    }


    const downloadBill = billDetailId => {
        generateRDLC('PDF', billDetailId);
    }
    const deleteBill = id => {
        if (confirm("Are you sure want delete the Bill?") == true) {
            $.get('/HouseRentBillGenerate/DeleteUpdatedBill/' + id, function (data) {
                if (data.success) {
                    let month = $('#requestMonthSearchId').val();
                    let year = $('#requestYearSearchId').val();
                    loadBillDatatable(month, year);
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
    const generateRDLC = (fileType, billDetailId) => {
        var reportType = 'HouseBillInformation';
        var viewURL = '/HouseReport/ShowReport?fileType=' + fileType + '&billDetailId=' + billDetailId + "&reportType=" + reportType;
        $.fancybox(
            {
                'title': 'Report Window',
                'type': 'iframe',
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'speedIn': 1000,
                'speedOut': 700,
                autoSize: false,
                closeClick: false,
                afterShow: after_show(fileType),
                'href': viewURL
            });
    }
    const after_show = fileType => {
        if (fileType != "PDF") {
            setTimeout(function () { $.fancybox.close(); }, 19000);
        }
    }
    const showSuccessMessage = successText => {
        $('#messageBoxId').show();
        $('#errorMessageBoxId').hide();
        $('#successMessageBoxId').show();
        $('#successMessageBoxId').html(successText);
        $('#messageBoxId').focus();
    }
    const showErrorMessage = errorText => {
        $('#messageBoxId').show();
        $('#errorMessageBoxId').show();
        $('#successMessageBoxId').hide();
        $('#errorMessageBoxId').html(errorText);
        $('#messageBoxId').focus();
    };
})(jQuery);