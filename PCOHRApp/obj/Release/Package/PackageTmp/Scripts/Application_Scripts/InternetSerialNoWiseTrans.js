$(document).ready(function () {
    loadInitialization();
});

$('#btnSaveId').click(function () {
    LoadBillDatatable();
});


function LoadBillDatatable() {

    var pageNo = $('#pageNoId').val();
    var serialNo = $('#SerialNoId').val();
    var yearID = $('#yearId').val();

    var table = $('#customerBillListTableId').DataTable();
    table.destroy();
    $('#customerBillListTableId').DataTable({
        scrollX: true,
        "ajax":
            {
                url: '/InternetBillCollection/GetBillCollectionListBySerial',
                type: "POST",
                data: { pageNo: pageNo, serialNo: serialNo, yearID: yearID },
                datatype: "json"
            },
        //"responsive": true,
        //"processing": true,
        //"serverSide": true,
        "columns":
        [
            
            {
                "data": null,
                "className": "center",
                render: function (data, type, row) {
                    return 'SL-' + data.pageNo + '/' + data.customerSL;
                }

            },

            { "data": "customerSerial", "autoWidth": true },
            { "data": "customerName", "autoWidth": true },
            { "data": "connFee", "autoWidth": true },
            { "data": "collectedDateString", "autoWidth": true },
            { "data": "fromMonthYear", "autoWidth": true },
            { "data": "toMonthYear", "autoWidth": true },
            { "data": "monthlyFee", "autoWidth": true },
            { "data": "shiftingCharge", "autoWidth": true },
            { "data": "netAmount", "autoWidth": true },
            { "data": "rcvAmount", "autoWidth": true },
            { "data": "reConnFee", "autoWidth": true },
             { "data": "collectedByString", "autoWidth": true },
              { "data": "receivedByString", "autoWidth": true },
               { "data": "createdByString", "autoWidth": true },
        ],
        "serverSide": true,
        "order": [1, "asc"],
        "processing": "true",
        "language": {
            "processing": "processing... please wait"
        },


    });
  

}



$('#chckpageNoId').change(function () {
    if (this.checked) {
        $('#pageNoId').val('NA');
    } else {
        $('#pageNoId').val('');
    }
   
});

$('#chckSerialNo').change(function () {
    if (this.checked) {
        $('#SerialNoId').val('NA');
    } else {
        $('#SerialNoId').val('');
    }

});


function loadInitialization() {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();

  
    var years = [];
    $.get('/Dropdown/GetYearList', function (data) {
        years = data.data;
        $('#yearId').select2({
            data: years
        });
    });
   
}
