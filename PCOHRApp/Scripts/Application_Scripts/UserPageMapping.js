
$(document).ready(function () {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();
    var users = []
    $.get('/Account/GetUserDropdownList', function (data) {
        users = data.data;
        $('#userId').select2({
            data: users
        });
    });

    $('#userId').change(function () {
        var id = $('#userId').val();
        if (id != '0') {


            table = $('#pageListTableId').DataTable();
            table.destroy();
            table = $('#pageListTableId').DataTable({

                "ajax": '/Account/GetPageListByUserId/' + id,
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
                   { "data": "pageId", "autoWidth": true },
                    { "data": "pageName", "autoWidth": true },
                    { "data": "pageUrl", "autoWidth": true },
                    { "data": "pageType", "autoWidth": true }
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
            console.log(table.rows({ selected: false }).data());
            table.rows().every(function (rowIdx, tableLoop, rowLoop) {
                alert(this)
                if (this.data().isPermitted) {
                    this.select();
                }
            });
        }
    });

    $('#btnSaveId').click(function () {
        var tableData = table.rows({ selected: true }).data();
        console.log(tableData);
        if (tableData.any()) {
            var inputData = [];
            for (var i = 0; i < tableData.length; i++) {
                inputData[i] = tableData[i]
            }
            $.ajax({
                url: '/Account/UserPageMap',
                type: 'POST',
                data: JSON.stringify({ _objs: inputData, userId: $('#userId').val() }),
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data.success == true) {
                        showSuccessMessage(data.message);
                        $('#userId').val('0');
                        
                        $('.select2').trigger('change');
                        table.clear().draw();
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




            //$.post('/Account/UserPageMap', JSON.stringify(inputData), function (data) {
            //    if (data.success == true) {
            //        showSuccessMessage(data.message);
            //        $('#userId').val('0');
            //        table.destroy();
            //    }
            //    else {
            //        showErrorMessage(data.message)
            //    }
            //});
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
});