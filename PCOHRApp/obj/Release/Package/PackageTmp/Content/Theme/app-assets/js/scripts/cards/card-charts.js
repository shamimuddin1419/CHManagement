/*=========================================================================================
    File Name: advance-cards.js
    Description: intialize advance cards
    ----------------------------------------------------------------------------------------
    Item Name: Stack - Responsive Admin Theme
    Version: 3.2
    Author: Pixinvent
    Author URL: hhttp://www.themeforest.net/user/pixinvent
==========================================================================================*/
(function(window, document, $) {
    'use strict';

    /****************************************************
    *               Employee Satisfaction               *
    ****************************************************/
    //Get the context of the Chart canvas element we want to select
    var ctx1 = document.getElementById("emp-satisfaction").getContext("2d");

    // Create Linear Gradient
    var white_gradient = ctx1.createLinearGradient(0, 0, 0,400);
    white_gradient.addColorStop(0, 'rgba(255,255,255,0.5)');
    white_gradient.addColorStop(1, 'rgba(255,255,255,0)');

    // Chart Options
    var empSatOptions = {
        responsive: true,
        maintainAspectRatio: false,
        datasetStrokeWidth : 3,
        pointDotStrokeWidth : 4,
        tooltipFillColor: "rgba(0,0,0,0.8)",
        legend: {
            display: false,
        },
        hover: {
            mode: 'label'
        },
        scales: {
            xAxes: [{
                display: false,
            }],
            yAxes: [{
                display: false,
                ticks: {
                    min: 0,
                    max: 85
                },
            }]
        },
        title: {
            display: false,
            fontColor: "#FFF",
            fullWidth: false,
            fontSize: 40,
            text: '82%'
        }
    };

    // Chart Data
    var empSatData = {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [{
            label: "Employees",
            data: [28, 35, 36, 48, 46, 42, 60],
            backgroundColor: white_gradient,
            borderColor: "rgba(255,255,255,1)",
            borderWidth: 2,
            strokeColor : "#ff6c23",
            pointColor : "#fff",
            pointBorderColor: "rgba(255,255,255,1)",
            pointBackgroundColor: "#2DCEE3",
            pointBorderWidth: 2,
            pointHoverBorderWidth: 2,
            pointRadius: 5,
        }]
    };

    var empSatconfig = {
        type: 'line',

        // Chart Options
        options : empSatOptions,

        // Chart Data
        data : empSatData
    };

    // Create the chart
    var areaChart = new Chart(ctx1, empSatconfig);



    /***********************************************************
    *               New User - Page Visist Stats               *
    ***********************************************************/
    //Get the context of the Chart canvas element we want to select
    var ctx2 = document.getElementById("line-stacked-area").getContext("2d");

    // Chart Options
    var userPageVisitOptions = {
        responsive: true,
        maintainAspectRatio: false,
        pointDotStrokeWidth : 4,
        legend: {
            display: false,
            labels: {
                fontColor: '#FFF',
                boxWidth: 10,
            },
            position: 'top',
        },
        hover: {
            mode: 'label'
        },
        scales: {
            xAxes: [{
                display: true,
                gridLines: {
                    color: "rgba(255,255,255, 0.3)",
                    drawTicks: true,
                    drawBorder: false,
                    zeroLineColor:'#FFF'
                },
                ticks: {
                    display: true,
                },
            }],
            yAxes: [{
                display: true,
                gridLines: {
                    color: "rgba(0,0,0, 0.07)",
                    drawTicks: false,
                    drawBorder: false,
                    drawOnChartArea: true
                },
                ticks: {
                    display: false,
                    maxTicksLimit: 5
                },
            }]
        },
        title: {
            display: false,
            text: 'Chart.js Line Chart - Legend'
        },
    };

    // Chart Data
    var userPageVisitData = {
        labels: ["2010", "2011", "2012", "2013", "2014", "2015", "2016","2017"],
        datasets: [{
            label: "iOS",
            data: [0, 5, 22, 14, 28, 12, 24, 0],
            backgroundColor: "rgba(255,117,136, 0.7)",
            borderColor: "transparent",
            pointBorderColor: "transparent",
            pointBackgroundColor: "transparent",
            pointRadius: 2,
            pointBorderWidth: 2,
            pointHoverBorderWidth: 2,
        },{
            label: "Windows",
            data: [0, 8, 30, 15, 12, 21, 14, 0],
            backgroundColor: "rgba(255,168,125,0.9)",
            borderColor: "transparent",
            pointBorderColor: "transparent",
            pointBackgroundColor: "transparent",
            pointRadius: 2,
            pointBorderWidth: 2,
            pointHoverBorderWidth: 2,
        }, {
            label: "Android",
            data: [0, 20, 10, 45, 20, 36, 21, 0],
            backgroundColor: "rgba(22,211,154,0.7)",
            borderColor: "transparent",
            pointBorderColor: "transparent",
            pointBackgroundColor: "transparent",
            pointRadius: 2,
            pointBorderWidth: 2,
            pointHoverBorderWidth: 2,
        }]
    };

    var userPageVisitConfig = {
        type: 'line',

        // Chart Options
        options : userPageVisitOptions,

        // Chart Data
        data : userPageVisitData
    };

    // Create the chart
    var stackedAreaChart = new Chart(ctx2, userPageVisitConfig);


    /*********************************************
    *               Total Earnings               *
    **********************************************/
    //Get the context of the Chart canvas element we want to select
    var ctx3 = document.getElementById("earning-chart").getContext("2d");

    // Chart Options
    var earningOptions = {
        responsive: true,
        maintainAspectRatio: false,
        datasetStrokeWidth : 3,
        pointDotStrokeWidth : 4,
        tooltipFillColor: "rgba(0,0,0,0.8)",
        legend: {
            display: false,
            position: 'bottom',
        },
        hover: {
            mode: 'label'
        },
        scales: {
            xAxes: [{
                display: false,
            }],
            yAxes: [{
                display: false,
                ticks: {
                    min: 0,
                    max: 70
                },
            }]
        },
        title: {
            display: false,
            fontColor: "#FFF",
            fullWidth: false,
            fontSize: 40,
            text: '82%'
        }
    };

    // Chart Data
    var earningData = {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [{
            label: "My First dataset",
            data: [28, 35, 36, 48, 46, 42, 60],
            backgroundColor: 'rgba(255,117,136,0.1)',
            borderColor: "transparent",
            borderWidth: 0,
            strokeColor : "#ff6c23",
            capBezierPoints: true,
            pointColor : "#fff",
            pointBorderColor: "rgba(255,117,136,1)",
            pointBackgroundColor: "#FFF",
            pointBorderWidth: 2,
            pointRadius: 4,
        }]
    };

    var earningConfig = {
        type: 'line',

        // Chart Options
        options : earningOptions,

        // Chart Data
        data : earningData
    };

    // Create the chart
    var earningChart = new Chart(ctx3, earningConfig);


    /*************************************************
    *               Posts Visits Ratio               *
    *************************************************/
    //Get the context of the Chart canvas element we want to select
    var ctx4 = $("#posts-visits");

    // Chart Options
    var PostsVisitsOptions = {
        responsive: true,
        maintainAspectRatio: false,
        legend: {
            position: 'top',
            labels: {
                boxWidth: 10,
                fontSize: 14
            },
        },
        hover: {
            mode: 'label'
        },
        scales: {
            xAxes: [{
                display: true,
                gridLines: {
                    lineWidth: 2,
                    color: "rgba(0, 0, 0, 0.05)",
                    zeroLineWidth: 2,
                    zeroLineColor: "rgba(0, 0, 0, 0.05)",
                    drawTicks: false,
                },
                ticks: {
                    fontSize: 14,
                }
            }],
            yAxes: [{
                display: false,
                ticks: {
                    min: 0,
                    max: 100
                }
            }]
        },
        title: {
            display: false,
            text: 'Chart.js Line Chart - Legend'
        }
    };

    // Chart Data
    var postsVisitsData = {
        labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
        datasets: [{
            label: "Visits",
            data: [32, 25, 45, 30, 60, 40, 72, 52, 80, 60, 92, 70],
            lineTension: 0,
            fill: false,
            // borderDash: [5, 5],
            borderColor: "#16D39A",
            pointBorderColor: "#16D39A",
            pointBackgroundColor: "#FFF",
            pointBorderWidth: 3,
            pointRadius: 6,
        }, {
            label: "Posts",
            data: [12, 10, 25, 15, 35, 22, 42, 28, 50, 32, 58, 28],
            lineTension: 0,
            fill: false,
            borderColor: "#FF7588",
            pointBorderColor: "#FF7588",
            pointBackgroundColor: "#FFF",
            pointBorderWidth: 3,
            pointRadius: 6,
        }]
    };

    var postsVisitsConfig = {
        type: 'line',

        // Chart Options
        options : PostsVisitsOptions,

        data : postsVisitsData
    };

    // Create the chart
    var postsVisitsChart = new Chart(ctx4, postsVisitsConfig);



    /************************************************
    *               Sales Growth Rate               *
    ************************************************/
    Morris.Area({
        element: 'sales-growth-chart',
        data: [{y: '2010', a: 28, }, {y: '2011', a: 40 }, {y: '2012', a: 36 }, {y: '2013', a: 48 }, {y: '2014', a: 32 }, {y: '2015', a: 42 }, {y: '2016', a: 30 }],
        xkey: 'y',
        ykeys: ['a'],
        labels: ['Sales'],
        behaveLikeLine: true,
        ymax: 60,
        resize: true,
        pointSize: 0,
        smooth: true,
        gridTextColor: '#bfbfbf',
        gridLineColor: '#c3c3c3',
        numLines: 6,
        gridtextSize: 14,
        lineWidth: 2,
        fillOpacity: 0.6,
        lineColors: ['#FFA87D'],
        hideHover: 'auto',
    });


    /*******************************************
    *               Mobile Sales               *
    ********************************************/
    Morris.Bar({
        element: 'mobile-sales',
        data: [{device: 'iPhone 7', sales: 1835 }, {device: 'Note 7', sales: 2356 }, {device: 'Mi5', sales: 1459 }, {device: 'Moto Z', sales: 1289 }, {device: 'Lenovo X3', sales: 1647 }, {device: 'OnePlus 3', sales: 2156 }],
        xkey: 'device',
        ykeys: ['sales'],
        labels: ['Sales'],
        barGap: 6,
        barSizeRatio: 0.3,
        gridTextColor: '#FFF',
        gridLineColor: '#FFF',
        goalLineColors: '#000',
        numLines: 4,
        gridtextSize: 14,
        resize: true,
        barColors: ['#FFF'],
        xLabelAngle: 35,
        hideHover: 'auto',
    });


    /********************************************
    *               Monthly Sales               *
    ********************************************/
    Morris.Bar({
        element: 'monthly-sales',
        data: [{month: 'Jan', sales: 1835 }, {month: 'Feb', sales: 2356 }, {month: 'Mar', sales: 1459 }, {month: 'Apr', sales: 1289 }, {month: 'May', sales: 1647 }, {month: 'Jun', sales: 2156 }, {month: 'Jul', sales: 1835 }, {month: 'Aug', sales: 2356 }, {month: 'Sep', sales: 1459 }, {month: 'Oct', sales: 1289 }, {month: 'Nov', sales: 1647 }, {month: 'Dec', sales: 2156 }],
        xkey: 'month',
        ykeys: ['sales'],
        labels: ['Sales'],
        barGap: 4,
        barSizeRatio: 0.3,
        gridTextColor: '#bfbfbf',
        gridLineColor: '#e3e3e3',
        numLines: 5,
        gridtextSize: 14,
        resize: true,
        barColors: ['#00B5B8'],
        hideHover: 'auto',
    });



    /*****************************************************
    *               Advertisement Expenses               *
    *****************************************************/
    //Get the context of the Chart canvas element we want to select
    var ctx7 = $("#advertisement-expense");

    // Chart Options
    var chartOptions = {
        responsive: true,
        maintainAspectRatio: false,
        responsiveAnimationDuration:500,
        legend: {
            display: false,
            position: 'bottom',
        },
        hover: {
            mode: 'label'
        },
        scales: {
            xAxes: [{
                display: false,
                scaleLabel: {
                    display: true,
                    labelString: 'Month'
                }
            }],
            yAxes: [{
                display: true,
            }]
        },
        title: {
            display: false,
            text: 'Radar Chart'
        }
    };

    // Chart Data
    var chartData = {
        labels: ["Radio", "TV", "Movie", "Show", "Banner", "Internet", "Newspaper"],
        datasets: [{
            label: "Samsung Galaxy S7",
            borderColor: "rgba(22,211,154,1)",
            backgroundColor: "rgba(22,211,154,.7)",
            pointBackgroundColor: "rgba(22,211,154,1)",
            data: [NaN, 59, 80, 81, 56, 55, 40],
        }, {
            label: "iPhone 7",
            data: [45, 25, NaN, 36, 67, 18, 76],
            borderColor: "rgba(255,117,136,1)",
            backgroundColor: "rgba(255,117,136,.7)",
            pointBackgroundColor: "rgba(255,117,136,1)",
            hoverPointBackgroundColor: "#fff",
            pointHighlightStroke: "rgba(255,117,136,1)",
        }, {
            label: "One Plus 3",
            data: [28, 48, 40, 19, 86, 27, NaN],
            borderColor: "rgba(255,168,125,1)",
            backgroundColor: "rgba(255,168,125,.7)",
            pointBackgroundColor: "rgba(255,168,125,1)",
            hoverPointBackgroundColor: "#fff",
            pointHighlightStroke: "rgba(255,168,125,1)",
        },]
    };

    var config = {
        type: 'radar',

        // Chart Options
        options: chartOptions,

        data: chartData
    };

    // Create the chart
    var lineChart = new Chart(ctx7, config);


    /*************************************************
    *               Cost Revenue Stats               *
    *************************************************/
    new Chartist.Line('#cost-revenue', {
        labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 12, 14, 15, 16, 17, 18, 19, 20],
        series: [
            [
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 3},
                {meta:'Revenue', value: 4},
                {meta:'Revenue', value: 3},
                {meta:'Revenue', value: 6},
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 8},
                {meta:'Revenue', value: 12},
                {meta:'Revenue', value: 7},
                {meta:'Revenue', value: 10},
                {meta:'Revenue', value: 12},
                {meta:'Revenue', value: 10},
                {meta:'Revenue', value: 11},
                {meta:'Revenue', value: 9},
                {meta:'Revenue', value: 11},
                {meta:'Revenue', value: 5},
                {meta:'Revenue', value: 10},
                {meta:'Revenue', value: 9},
                {meta:'Revenue', value: 14},
                {meta:'Revenue', value: 10}
            ]
        ]
    }, {
        low: 0,
        high: 18,
        fullWidth: true,
        showArea: true,
        showPoint: true,
        showLabel: false,
        axisX: {
            showGrid: false,
            showLabel: false,
            offset: 0
        },
        axisY: {
            showGrid: false,
            showLabel: false,
            offset: 0
        },
        chartPadding: 0,
        plugins: [
            Chartist.plugins.tooltip()
        ]
    }).on('draw', function(data) {
        if (data.type === 'area') {
            data.element.attr({
                'style': 'fill: #FFF; fill-opacity: 0.3'
            });
        }
        if (data.type === 'line') {
            data.element.attr({
                'style': 'fill: transparent; stroke: #FFF; stroke-opacity: 0.4; stroke-width: 2px;'
            });
        }
        if (data.type === 'point') {
            data.element.attr({
                'style': 'stroke: #FFF; stroke-opacity: 0.4; stroke-width: 6px;'
            });
        }
    });


})(window, document, jQuery);