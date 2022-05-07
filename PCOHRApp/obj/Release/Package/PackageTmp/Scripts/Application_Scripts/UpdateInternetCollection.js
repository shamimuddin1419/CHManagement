$(document).ready(function () {
    loadInitialization();
});
$('#fromDateId #toDateId').on('changeDate', function (ev) {
    $(this).datepicker('hide');
});
$('#btnSearchId').click(function () {
    if ($('#fromDateId input').val() == '') {
        showErrorMessage('Provide From Date!!');
    }
    else if ($('#toDateId input').val() == '') {
        showErrorMessage('Provide To Date!!');
    }
    else if ($('#collectedById').val() == 0) {
        showErrorMessage('Provide Collected By!!');
    }
    else {
        $('#messageBoxId').hide();
        $('#errorMessageBoxId').hide();
        $('#successMessageBoxId').hide();
        loadDataTable();
    }
});


$('#btnSaveId').click(function () {
    if ($('#receivedById').val() == 0) {
        showErrorMessage('Provide Received By!!');
    }
    else {
        var tableData = table.rows({ selected: true }).data();
        var indexes = table.rows({ selected: true }).indexes();
        console.log(tableData);
        if (tableData.any()) {
            var inputData = [];
            for (var i = 0; i < tableData.length; i++) {
                inputData[i] = {};
                inputData[i].collectionId = tableData[i].collectionId;
                var currentIndex = indexes[i];
                inputData[i].pageNo = table.cell(currentIndex, 9).nodes().to$().find('input').val();
                inputData[i].customerSL = table.cell(currentIndex, 10).nodes().to$().find('input').val();
            }

            $.ajax({
                url: '/InternetBillCollection/UpdateCollectionStatus',
                type: 'POST',
                data: JSON.stringify({ objs: inputData, userId: $('#receivedById').val() }),
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data.success == true) {
                        showSuccessMessage(data.message);

                        loadDataTable();
                    }
                    else if (data.success == false) {
                        showErrorMessage(data.message);
                    } else {
                        showErrorMessage("Something error occured, Refresh the page and try again!");
                    }
                },
                error: function (request) {
                    // ...
                }
            });

        }
        else {
            showErrorMessage("Select Row to Update Data!");
        }
    }
});

function loadDataTable() {
    table = $('#customerBillListTableId').DataTable();
    table.destroy();
    table = $('#customerBillListTableId').DataTable({

        "ajax": '/InternetBillCollection/GetBillUnCollectionList?fromDate=' + $('#fromDateId input').val() + '&toDate=' + $('#toDateId input').val() + '&receivedBy=' + $('#collectedById').val(),
        "responsive": true,
        "select": true,
        "paging": false,
        "rowCallback": function (row, data) {
            if (data.isPermitted) {
                table.row(row).select();
                $('td:eq(0)', row).html("<input type='checkbox' checked class = 'styled'/>");
            }
        },
        "columns":
        [
           { "data": null, render: function (data, type, row) { return "<input type='checkbox' class = 'customCheck'/>"; } },
           { "data": "voucherNo", "autoWidth": true },
            { "data": "customerSerial", "autoWidth": true },
            { "data": "customerName", "autoWidth": true },
             { "data": "customerPhone", "autoWidth": true },
              { "data": "fromMonthYear", "autoWidth": true },
               { "data": "toMonthYear", "autoWidth": true },
            { "data": "rcvAmount", "autoWidth": true },
            { "data": "stringCreatedDate", "autoWidth": true },
               { "data": null, render: function (data, type, row) { return '<input type="text" class = "form-control" value=' + row.pageNo + '>'; } },
            { "data": null, render: function (data, type, row) { return '<input type="text" class = "form-control" value=' + row.customerSL + '>'; } },

        ],
        columnDefs: [
            {
                "targets": 0,
                "orderable": false,
                "width": "5%",
                checkboxes: {
                    selectRow: true
                }
            },
            {
                "targets": 1,
                "orderable": false,
                "searchable": false,
                "visible": false

            }
        ],
        select: {
            style: 'multi',
            selector: 'td:first-child input'
        },
        order: [[1, 'asc']]
    })

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

function loadInitialization() {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();

    $('#fromDateId').datetimepicker({
        useCurrent: false,
        format: 'DD/MM/YYYY'
    });
    $('#toDateId').datetimepicker({
        useCurrent: false,
        format: 'DD/MM/YYYY',
    });
    $.get('/Account/GetUserDropdownList', function (data) {
        users = data.data;
        $('#collectedById').select2({
            data: users
        });
    });
    $.get('/Account/GetUserDropdownListForManagement', function (data) {
        users = data.data;
        $('#receivedById').select2({
            data: users
        });
    });
}

