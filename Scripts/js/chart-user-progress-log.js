$(document).ready(function () {


    var toDisplayBf = false;
    var toHideBf = !toDisplayBf;

    var progressDataAll;
    var ctx;



    $("#buttonDisplayGraphAll").click(function () {
        generateChartData("/api/UserProgressAll/");
    });

    $("#buttonDiplayGraphRelevant").click(function () {
        generateChartData("/api/UserProgressRelevant/");
    });




    // var url = ... make link with data from activePoint


    //$(document).ready(function () {


        //$("#buttonToggleBodyFat").on("change",
        //    function () {
        //    });

        //if (trackBodyFat2 == "True") {
        //    console.log("here");
        //};


        //$("#buttonToggleBodyFat").prop('checked', false);
        //console.log($("#buttonToggleBodyFat").prop('checked'));
        //$("#buttonToggleBodyFat").prop('checked', true);
        //console.log($("#buttonToggleBodyFat").prop('checked'));


    //});





    $("#buttonToggleBodyFat").click(function () {
  
        if (this.checked) $("#buttonToggleBodyFatText").text("Hide Body Fat %");
        else $("#buttonToggleBodyFatText").text("Show Body Fat %");
        toDisplayBf = this.checked;


        progressDataAll.destroy();


        if ($('#buttonDiplayGraphRelevant').is(':checked')) {
            generateChartData("/api/UserProgressRelevant/");
            
        } else {
            generateChartData("/api/UserProgressAll/");
       
        }

        //progressDataAll.data.labels.pop();
        //progressDataAll.data.datasets.forEach((dataset) => {
        //    dataset.data.pop();
        //});


    });




    function BuildChart(progressWeight, progressBodyFat, goalWeight, goalBodyFat, dates, maxBodyFat, weightUnit, checkInId) {
        var bodyFatColour = "rgba(247, 108, 108, 1)";
        var weightColour = "rgba(55, 71, 133,1)";
        var bodyFatGoalColour = "rgba(247, 108, 108, 0.3)";
        var weightGoalColour = "rgba(55, 71, 133,0.3)";
        var todayLineColour = "#A8D0E6";
        var weightLabel = "Weight (" + weightUnit[0] + ")";
        var toHideBf = !toDisplayBf;
        var data = {
            labels: dates,

            datasets: [
                {
                    data: progressWeight,
                    label: weightLabel,
                    lineTension: 0.3,
                    fill: false,
                    backgroundColor: weightColour,
                    borderColor: weightColour,
                    pointRadius: 3,
                    pointBackgroundColor: weightColour,
                    pointBorderColor: weightColour,
                    pointHoverRadius: 15,
                    pointHoverBackgroundColor: weightColour,
                    pointHoverBorderColor: weightColour,
                    pointHitRadius: 5,
                    pointBorderWidth: 5,
                    yAxisID: "y-axis-1",
                    borderWidth: 3,
                    spanGaps: true,

                },
                {
                    data: goalWeight,
                    label: "Goal Weight",
                    lineTension: 0.4,
                    fill: false,
                    backgroundColor: weightGoalColour,
                    borderColor: weightGoalColour,
                    pointRadius: 7,
                    pointBackgroundColor: weightColour,
                    pointBorderColor: weightColour,
                    pointHoverRadius: 2,
                    pointHoverBackgroundColor: weightGoalColour,
                    pointHoverBorderColor: weightGoalColour,
                    pointHitRadius: 5,
                    spanGaps: true,
                    pointStyle: "star",
                    borderWidth: 3,
                    yAxisID: "y-axis-1",
                    borderDash: [20]
                },
                {
                    data: progressBodyFat,
                    label: "Body Fat %",
                    lineTension: 0.4,
                    fill: false,
                    backgroundColor: bodyFatColour,
                    borderColor: bodyFatColour,
                    pointRadius: 3,
                    pointBackgroundColor: bodyFatColour,
                    pointBorderColor: bodyFatColour,
                    pointHoverRadius: 15,
                    pointHoverBackgroundColor: bodyFatColour,
                    pointHoverBorderColor: bodyFatColour,
                    pointRadius: 4,
                    pointBorderWidth: 5,
                    yAxisID: "y-axis-2",
                    spanGaps: true,
                    hidden: toHideBf

                    

                },
                {
                    data: goalBodyFat,
                    label: "Goal Body Fat %",
                    lineTension: 0.5,
                    fill: false,
                    backgroundColor: bodyFatGoalColour,
                    borderColor: bodyFatGoalColour,
                    pointRadius: 4,
                    pointBackgroundColor: bodyFatColour,
                    pointBorderColor: bodyFatColour,
                    pointHoverRadius: 2,
                    pointHoverBackgroundColor: bodyFatGoalColour,
                    pointHoverBorderColor: bodyFatGoalColour,
                    pointHitRadius: 0,
                    yAxisID: "y-axis-2",
                    spanGaps: true,
                    pointStyle: "star",
                    borderDash: [20],
                    hidden: toHideBf
                },
                {
                    data: checkInId,
                    label: '',
                    lineTension: 0.3,
                    fill: false,
                    backgroundColor: weightColour,
                    borderColor: weightColour,
                    pointRadius: 3,
                    pointBackgroundColor: weightColour,
                    pointBorderColor: weightColour,
                    pointHoverRadius: 15,
                    pointHoverBackgroundColor: weightColour,
                    pointHoverBorderColor: weightColour,
                    pointHitRadius: 5,
                    pointBorderWidth: 5,
                    borderWidth: 3,
                    spanGaps: true,
                    hidden: true,
                    legend: {
                        display: false,
                    }


                }
            ]
        };

        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();
        today = mm + '/' + dd + '/' + yyyy;
        ctx = document.getElementById("progressGraphData");


        progressDataAll = new Chart(ctx,
            {
                type: "line",
                data: data,
                options: {
                    legend: {
                        display: true,
                        labels: {
                            filter: function (item, chart) {
                                if (!toDisplayBf) { return !item.text.includes("Body Fat"); }
                                return true;

                                // Logic to remove a particular legend item goes here
                            },
                            filter: function (item, chart) {
                                return item.datasetIndex !== 4 ;
                            }
                        },

                        

                    },
                    //events:[],

                    annotation: {
                        annotations: [
                            {
                                type: "line",
                                mode: "vertical",
                                scaleID: "x-axis-0",
                                value: (moment(new Date(today))),
                                borderColor: todayLineColour,
                                borderWidth: 4,
                                borderDash: [5, 2],
                                borderDashOffset: 10,
                                label: {
                                    content: "Today",
                                    enabled: true,
                                    position: "center",
                                    rotation: 0,
                                    xAdjust: 30,
                                    fontSize: 12,
                                    backgroundColor: todayLineColour,
                                    yAdjust: -60,
                                    cornerRadius: 60
                                }
                            }
                        ]
                    },
                    responsive: true, 
                    maintainAspectRatio: false, // Add to prevent default behavior of full-width/height 
                    scales: {

                        xAxes: [
                            {
                                scaleLabel: {
                                    display: true,
                                    fontFamily: "Helvetica"

                                },
                                type: 'time',
                                time: {
                                    displayFormats: {
                                        'millisecond': 'MMM DD',
                                        'second': 'MMM DD',
                                        'minute': 'MMM DD',
                                        'hour': 'MMM DD',
                                        'day': 'MMM DD',
                                        'week': 'MMM DD',
                                        'month': 'MMM DD',
                                        'quarter': 'MMM DD',
                                        'year': 'MMM DD'
                                    }
                                }
                            }
                        ],
                        yAxes: [

                            {
                                id: 'y-axis-1',
                                type: 'linear',
                                position: 'left',
                                scaleLabel: {
                                    display: true,
                                    labelString: weightLabel,
                                    fontFamily: "Helvetica"
                                },
                                ticks: {
                                    fontFamily: "Helvetica"
                                    //min: minWeight,
                                    //max: maxWeight
                                    //suggestedMin: minWeight,
                                    //suggestedMax: maxWeight
                                
                                }
                            },
                            {
                                id: 'y-axis-2',
                                type: 'linear',
                                position: 'right',
                                display: toDisplayBf,
                                scaleLabel: {
                                    display: toDisplayBf,
                                    labelString: "Body Fat %",
                                    fontFamily: "Helvetica"
                                },
                                ticks: {
                                    display: toDisplayBf,

                                    //min: minBodyFat,
                                    //max: maxBodyFat
                                    max: 100,
                                    fontFamily: "Helvetica"
                                }
                            },
                            {
                                id: 'y-axis-3',
                                type: 'linear',
                                position: 'right',
                                display: false,
                                legend: {
                                    display: false
                                },
                                scaleLabel: {
                                    display: false,
               
                                },
                                ticks: {
                                    display: false,

                                    //min: minBodyFat,
                                    //max: maxBodyFat
                                    max: 100,
                                    fontFamily: "Helvetica"
                                }
                            }
                        ]
                    },
                }
            });

       
        return progressDataAll;
    }



    var generateChartData = function (apiString) {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var json = JSON.parse(this.response);
                // Map JSON labels  back to values array
                var progressWeight = json.map(function (e) {
                     return (e.WeightInKg );
                });
                var progressBodyFat = json.map(function (e) {
                    return e.BodyFat;
                });
                var goalWeight = json.map(function (e) {
                    return (e.GoalWeight );
                });
                var goalBodyFat = json.map(function (e) {
                    return e.GoalBodyFat;
                });
                var weightUnit = json.map(function (e) {
                    return e.WeightUnit;
                });
                var checkInId = json.map(function (e) {
                    return e.CheckInId;
                });

                // Map JSON values back to values array
                var dates = json.map(function (e) {
                    //return (new Date(e.Date)); // Divide to billions in units of ten
                    //return (e.Date); // Divide to billions in units of ten
                    //return (new Date(e.Date)); // Divide to billions in units of ten
                    //return (moment(new Date(e.Date)).format("DD MMM YYYY")); // Divide to billions in units of ten
                    return (moment(new Date(e.Date))); // Divide to billions in units of ten
                });

                //Set the minimum weight in the Y axis
                var maxWeightInArray = Math.max.apply(Math, progressWeight); // 1
                var minWeightInArray = Math.min.apply(Math, progressWeight); // 1
                var minWeight = minWeightInArray - ((maxWeightInArray - minWeightInArray) * 0.1); // 1
                var maxWeight = maxWeightInArray + ((maxWeightInArray - minWeightInArray) * 0.1); // 1

                var minBodyFatInArray = Math.min.apply(Math, progressBodyFat);
                var maxBodyFatInArray = Math.max.apply(Math, progressBodyFat);
                var minBodyFat = minBodyFatInArray - ((maxBodyFatInArray - minBodyFatInArray) * 0.1); // 1
                var maxBodyFat = maxBodyFatInArray + ((maxBodyFatInArray - minBodyFatInArray) * 0.1); // 1


            

                BuildChart(progressWeight,
                    progressBodyFat,
                    goalWeight,
                    goalBodyFat,
                    dates,
                    maxBodyFat,
                    weightUnit,
                    checkInId); // Pass in data and call the chart

            }
        };

        xhttp.open("GET", apiString, false);
        xhttp.send();
    };


    //Auto load the relevant goal data
    generateChartData("/api/UserProgressRelevant/");


    ctx.onclick = function (evt) {
        var activePoints = progressDataAll.getElementsAtEvent(evt);
        if (activePoints[0]) {
            var chartData = activePoints[0]['_chart'].config.data;
            var idx = activePoints[0]['_index'];
            var value = chartData.datasets[4].data[idx];
            var url = "/home/newcheckinform/" + value;

            //console.log(url);
            //console.log(chartData.datasets[4].data[idx]);
            //console.log(chartData);
            window.location.href = url;
        }
    };




});
//https://www.chartjs.org/docs/
//Show picture when clicked node - getElementAtEvent