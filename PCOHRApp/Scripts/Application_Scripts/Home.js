$(document).ready(function () {
    loadInitialization();
});

function loadInitialization() {
    $.get('/Home/GetDishDashboarddata', function (data) {
        $('#totalActiveUserId').text(data.data.totalActiveUsers);
        $('#totalInactiveUserId').text(data.data.totalInactiveUser);
        $('#discontinuedUserThisMonthId').text(data.data.discontinuedUserThisMonth);
        $('#generatedBillThisMonthId').text(data.data.generatedBillThisMonth);
        $('#collectedThisMonthId').text(data.data.collectedThisMonth);
        $('#todaysCollectedAmountId').text(data.data.todaysCollectedAmount);

       
    });

    $.get('/Home/GetInternetDashboarddata', function (data) {
        $('#totalInternetActiveUserId').text(data.data.totalActiveUsers);
        $('#totalInternetInactiveUserId').text(data.data.totalInactiveUser);
        $('#discontinuedInternetUserThisMonthId').text(data.data.discontinuedUserThisMonth);
        $('#generatedInternetBillThisMonthId').text(data.data.generatedBillThisMonth);
        $('#collectedInternetThisMonthId').text(data.data.collectedThisMonth);
        $('#todaysInternetCollectedAmountId').text(data.data.todaysCollectedAmount);


    });
}