$(document).ready(function () {
    loadInitialization();
});

$('#btnSaveId').click(function () {
    GenerateRDLC("PDF");
});
$('#btnExcelId').click(function () {
    GenerateRDLC('Excel');
});
$('#btnWordId').click(function () {
    GenerateRDLC('Word');
});

function loadInitialization() {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();


    $.get('/Zone/GetZoneListForDropdown', function (data) {
        zones = data.data;
        $('#zoneId').select2({
            data: zones
        });
    });

    var customerSerials = []
    $.get('/Dropdown/GetCustomerSerialList', function (data) {
        customerSerials = data.data;
        $('#customerSerialPrefixId').select2({
            data: customerSerials
        });
    });

    var assignedUsers = []
    $.get('/Account/GetUserDropdownList', function (data) {
        assignedUsers = data.data;
        $('#assignedUserId').select2({
            data: assignedUsers
        });
    });

    var hosts = [];
    $('#hostId').select2({
        ajax: {
            url: '/Host/GetHostListForDropdown',
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


function GenerateRDLC(fileType) {
    var zoneId = $('#zoneId').val();
    var custSerialPrefixId = $('#customerSerialPrefixId').val();
    var assignedUserId = $('#assignedUserId').val();
    var hostId = $('#hostId').val();
    var activeStatus = 1;
    var dueReportStatus = $('#statusId').val();
    var reportType = 'InternetBillDue';
    // var viewURL = '/CableReport/ShowReport?fileType=' + fileType + '&zoneId=' + zoneId + "&reportType=" + reportType;
    var viewURL = '/CableReport/ShowReport?fileType=' + fileType + '&zoneId=' + zoneId + '&custSerialPrefixId=' + custSerialPrefixId + '&assignedUserId=' + assignedUserId + '&hostId=' + hostId + "&reportType=" + reportType + '&activeStatus=' + activeStatus + "&dueReportStatus=" + dueReportStatus;
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
function after_show(fileType) {
    if (fileType != "PDF") {
        setTimeout(function () { $.fancybox.close(); }, 19000);
    }
}
