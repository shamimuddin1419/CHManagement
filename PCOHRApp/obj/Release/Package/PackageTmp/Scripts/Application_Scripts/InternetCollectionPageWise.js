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


function GenerateRDLC(fileType) {
    var pageNo = $('#pageNoId').val();
    var receivedBy = $('#receivedById').val();
    var yearId = $('#yearId').val();
    var reportType = 'InternetBillCollectionPageWise';
    if (yearId == "0") {
        showErrorMessage('Provide Year!!');
    }
    if (receivedBy == "0") {
        showErrorMessage('Provide Received By!!');
    }
    else {
        var viewURL = '/CableReport/ShowReport?fileType=' + fileType + '&reportType=' + reportType + '&year=' + yearId + "&pageNo=" + pageNo + '&receivedBy=' + receivedBy;

        $.fancybox(
            {
                'title': 'Report Window',
                'type': 'iframe',
                iframe: {
                    preload: false // fixes issue with iframe and IE
                },
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'speedIn': 1000,
                'speedOut': 700,
                autoSize: false,
                closeClick: false,
                afterShow: after_show(fileType),
                'href': viewURL
            });
        $('#messageBoxId').hide();
    }
}
function after_show(fileType) {
    if (fileType != "PDF") {
        setTimeout(function () { $.fancybox.close(); }, 19000);
    }
}

function getPages() {
    $.get('/InternetBillCollection/GetPagesByYear?yearId=' + $('#yearId').val() + '&receivedBy=' + $('#receivedById').val(), function (data) {
        pages = data.data;
        $('#pageNoId').select2({
            data: pages
        });
    })
}

function loadInitialization() {
    $('#messageBoxId').hide();
    $('#errorMessageBoxId').hide();
    $('#successMessageBoxId').hide();

    $('#customerId').select2({
        ajax: {
            url: '/InternetCustomer/GetCustomerListForDropdown',
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
    var years = [];
    $.get('/Dropdown/GetYearList', function (data) {
        years = data.data;
        $('#yearId').select2({
            data: years
        });
    });
    $.get('/Account/GetUserDropdownListForManagement', function (data) {
        users = data.data;
        $('#receivedById').select2({
            data: users
        });
    });
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