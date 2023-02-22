(function () {
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
        $('.individualCustomerDiv').show();
        populateProjectDropDown();
        $('#collectionDateId').datetimepicker({
            useCurrent: false,
            format: 'DD/MM/YYYY'
        });

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
                loadUnpaidBill(data.id);
            });
        });
    }

    const loadUnpaidBill = houseId => {
        $.get(`/HouseBillCollection/GetUnPaidBillForDropDown?houseId=${houseId}`, function (data) {
            bills = data.data;
            let select = $("#billDetailId");

            select.empty().trigger('change');
            select.append($("<option>").val(0).text("Select Month"));
            $(data.data).each(function (index, item) {
                select.append($("<option>").val(item.id).text(item.text));
            });
            $('#billDetailId').trigger('change');
            $('#billDetailId').on('select2:select', function (e) {
                let data = e.params.data;
                loadBillInfoByBillId(data.id)
            });
        });
    }
    const loadBillInfoByBillId = billDetailId => {
        $.get(`/HouseBillCollection/GetBillByBillId/${billDetailId}`, function (data) {
            if (data.success) {
                $('.billInfoCardId').show();
                let bill = data.data;
                $('#houseTypeId').val(bill.houseType);
                $('#houseNameId').val(bill.houseName);
                $('#renterNameId').val(bill.renterName);
                $('#renternid').val(bill.renterNID);
                $('#renterPhoneId').val(bill.renterPhone);
                $('#advanceAmountId').val(bill.advanceAmount);
                $('#payableAmountId').val(bill.payableAmount);
                $('#rcvAmountId').val(bill.payableAmount);
                $('#rentAmountId').text(bill.rentAmount);
                $('#gasChargeId').text(bill.gasCharge);
                $('#electricityChargeId').text(bill.electricityCharge);
                $('#waterChargeId').text(bill.waterCharge);
                $('#serviceChargeId').text(bill.serviceCharge);
                $('#otherChargeId').text(bill.otherCharge);
                $('#billAmountId').text(bill.payableAmount);
            }

        })
    }

    const calculateAmount = () => {
        let payableAmount = parseFloat( $('#payableAmountId').val());
        payableAmount = payableAmount - (parseFloat($('#adjustAmountid').val()) + parseFloat( $('#discountId').val()));
        $('#rcvAmountId').val(payableAmount);
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

    const clearUI = () => {
        $('#houseTypeId').val('');
        $('#houseNameId').val('');
        $('#renterNameId').val('');
        $('#renternid').val('');
        $('#renterPhoneId').val('');
        $('#advanceAmountId').val('');
        $('#payableAmountId').val('');
        $('#rcvAmountId').val('');
        $('#rentAmountId').text('');
        $('#gasChargeId').text('');
        $('#electricityChargeId').text('');
        $('#waterChargeId').text('');
        $('#serviceChargeId').text('');
        $('#otherChargeId').text('');
        $('#billAmountId').text('');
        $('#discountId').val('0');
        $('#adjustAmountid').val('0');
        $('#collectionDateId input').val('');
        $('.billInfoCardId').hide();
        $('#btnDownloadId').hide();
        $('#projectId').val('0').trigger('change');
        loadInitHouses();
        loadInitUnpaidBill();
    }

    

    const loadInitHouses = () => {
        let select = $("#houseId");
        select.empty().trigger('change');
        select.append($("<option>").val(0).text("Select House"));
    };

    const loadInitUnpaidBill = () => {
        let select = $("#billDetailId");
        select.empty().trigger('change');
        select.append($("<option>").val(0).text("Select Month"));
    };


    const loadBillDatatable = () => {
        var table = $('#billListTableId').DataTable();
        table.destroy();
        $('#billListTableId').DataTable({
            "ajax":
            {
                "url": '/HouseBillCollection/GetBillCollectionList',
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
                            return `<button type="button"  title ="delete" class="edituser btn info"><i class="fa fa-remove" id="deletebill_${data.collectionId}" ></i></button>
                                    <button type="button"  title ="download" class="edituser btn info"><i class="fa fa-cloud-download" id="downloadbill_${data.billDetailId}"></i></button>`

                        }

                    },
                    { "data": "paymentRef", "autoWidth": true },
                    { "data": "houseName", "autoWidth": true },
                    { "data": "renterName", "autoWidth": true },
                    
                    { "data": "billMonth", "autoWidth": true },
                    { "data": "rcvAmount", "autoWidth": true },
                    { "data": "collectedDateString", "autoWidth": true },
                    
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
            $.get('/HouseBillCollection/DeleteCollection/' + id, function (data) {
                if (data.success) {
                   
                    loadBillDatatable();
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
        var reportType = 'HouseReceipt';
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
    $('#btnSaveId').click(() => {

        
        if (parseFloat($('#adjustAmountid').val()) > parseFloat($('#advanceAmountId').val())   ) {
            showErrorMessage('Cannot adjust more than advance ');
            return;
        }
        if ($('#collectionDateId input').val() == '') {
            showErrorMessage("Provide Collection Date!");
            return;
        }
       
          
        let inputData = {
            billDetailId : $('#billDetailId').val(),
            rcvAmount: $('#rcvAmountId').val(),
            discount : $('#discountId').val(),
            adjustAdvance: $('#adjustAmountid').val(),
            description : $('#descriptionId').val(),
            collectedDateString: $('#collectionDateId input').val()
        };
               
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

        $.post('/HouseBillCollection/InsertBillCollection', inputData, data => {
            if (data.success == true) {
                showSuccessMessage(data.message);
                //clearUI();
                $('#btnDownloadId').show();
            }
            else if (data.success == false) {
                showErrorMessage(data.message)
            }
            else
            {
                showErrorMessage("Something error occured, Refresh the page and try again!");
            }
            $.unblockUI();
        }).fail(function (response) {
            showErrorMessage(response.responseText);
        });
    });

    $('#adjustAmountid,#discountId').change(function () {
        calculateAmount();
    });

    $('#btnClearId').click(() => {
        $('#messageBoxId').hide();
        $('#errorMessageBoxId').hide();
        $('#successMessageBoxId').hide();
        $('#errorMessageBoxId').val('');
        $('#successMessageBoxId').val('');
        clearUI();
    });

    $('#collectionDateId').on('changeDate', ev => {
        $(this).datepicker('hide');
    });
    $('#btnDownloadId').click(() => {
        generateRDLC('PDF', $('#billDetailId').val());
        //$('#btnDownloadId').hide();
        //clearUI();
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
        $('.individualCustomerDiv').hide();
        loadBillDatatable();

    });

    $('#btnCreateId').click(() => {
        loadInitialization();
        clearUI();
    })
})();