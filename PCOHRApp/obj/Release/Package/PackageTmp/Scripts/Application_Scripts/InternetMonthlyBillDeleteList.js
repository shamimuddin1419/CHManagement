
$(document).ready(function () {
    loadMonthlyBillDeleteList();
   
});


function loadMonthlyBillDeleteList() {
    var table = $('#hostListTableId').DataTable();
    table.destroy();
    $('#hostListTableId').DataTable({
        "ajax":
            {
                "url": '/InternetMonthlyBillDeleteList/GetMonthlyBillDeleteList',
                "type": "POST",
                "datatype": "json"
            },
        "responsive": true,

        "columns":
        [
            { "data": "CustomerSerial", 'width': '5%' },
            { "data": "CustomerName", 'width': '10%' },
            { "data": "CustomerPhone", 'width': '5%' },
            { "data": "YearName", 'width': '5%' },
             { "data": "Month", 'width': '5%' },
           { "data": "BillAmount", 'width': '5%' },
            { "data": "DeletedBy", 'width': '5%' },
            { "data": "DeletedDate", 'width': '5%' }

        ],
        "serverSide": true,
        "order": [1, "asc"],
        "processing": "true",
        "language": {
            "processing": "processing... please wait"
        },

    });
}



