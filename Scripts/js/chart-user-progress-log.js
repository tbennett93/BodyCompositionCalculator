$(document).ready(function () {


    $("#buttonDisplayGraphAll").click(function () {
        // redraw chart with stanza data
        generateChartData("/api/UserProgressAll/");
    });

    $("#buttonDiplayGraphRelevant").click(function () {
        // redraw chart with stanza data
        generateChartData("/api/UserProgressRelevant/");
    });



    function BuildChart(progressWeight, progressBodyFat, goalWeight, goalBodyFat, dates, maxBodyFat, weightUnit) {
        var weightColour = "rgba(247, 108, 108, 1)";
        var bodyFatColour = "rgba(55, 71, 133,1)";
        var weightGoalColour = "rgba(247, 108, 108, 0.5)";
        var bodyFatGoalColour = "rgba(55, 71, 133,0.5)";


        var weightLabel = "Weight (" + weightUnit[0] + ")";

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
                    pointHitRadius: 10,
                    pointBorderWidth: 11,
                    yAxisID: 'y-axis-1',
                    borderWidth:10, 
                    spanGaps: true
                },
                {
                    data: goalWeight,
                    label: "Goal Weight",
                    lineTension: 0.4,
                    fill: false,
                    backgroundColor: weightGoalColour,
                    borderColor: weightGoalColour,
                    pointRadius: 10,
                    pointBackgroundColor: weightGoalColour,
                    pointBorderColor: weightGoalColour,
                    pointHoverRadius: 2,
                    pointHoverBackgroundColor: weightGoalColour,
                    pointHoverBorderColor: weightGoalColour,
                    pointHitRadius: 15,
                    pointBorderWidth: 1.6,
                    yAxisID: 'y-axis-1',
                    spanGaps: true,
                    pointStyle: 'star',
                    borderWidth: 7,
                    borderDash: [20]

                },
                {
                    data: progressBodyFat,
                    label: "Body Fat %",
                    lineTension: 0.4,
                    fill: false,
                    backgroundColor: bodyFatColour,
                    borderColor: bodyFatColour,
                    pointRadius: 5,
                    pointBackgroundColor: bodyFatColour,
                    pointBorderColor: bodyFatColour,
                    pointHoverRadius: 15,
                    pointHoverBackgroundColor: bodyFatColour,
                    pointHoverBorderColor: bodyFatColour,
                    pointHitRadius: 5,
                    pointBorderWidth: 11,
                    yAxisID: 'y-axis-2',
                    borderWidth: 10,
                    spanGaps: true
                },
                
                {
                    data: goalBodyFat,
                    label: "Goal BodyFat",
                    lineTension: 0.4,
                    fill: false,
                    backgroundColor: bodyFatGoalColour,
                    borderColor: bodyFatGoalColour,
                    pointRadius: 10,
                    pointBackgroundColor: bodyFatGoalColour,
                    pointBorderColor: bodyFatGoalColour,
                    pointHoverRadius: 2,
                    pointHoverBackgroundColor: bodyFatGoalColour,
                    pointHoverBorderColor: bodyFatGoalColour,
                    pointHitRadius: 15,
                    pointBorderWidth: 1.6,
                    yAxisID: 'y-axis-2',
                    spanGaps: true,
                    pointStyle: 'star',
                    borderWidth: 7,
                    borderDash: [20]
                }
            ],
        };


        var ctx = document.getElementById("progressGraphData").getContext("2d");
        var progressDataAll = new Chart(ctx,
            {
                type: "line",
                data: data,
                options: {
                    legend: {
                        display: true
                    },
                    responsive: true, 
                    maintainAspectRatio: false, // Add to prevent default behavior of full-width/height 
                    scales: {
                        xAxes: [
                            {
                                scaleLabel: {
                                    display: true,
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
                                    labelString: weightLabel
                                },
                                ticks: {
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
                                scaleLabel: {
                                    display: true,
                                    labelString: "Body Fat %"
                                },
                                ticks: {
                                    //min: minBodyFat,
                                    //max: maxBodyFat
                                    //suggestedMin: minBodyFat,
                                    suggestedMax: maxBodyFat
                                }
                            },
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
                    weightUnit); // Pass in data and call the chart
            }
        };

        xhttp.open("GET", apiString, false);
        xhttp.send();
    }

    //Auto load the relevant goal data
    generateChartData("/api/UserProgressRelevant/");


});
//https://www.chartjs.org/docs/
//Show picture when clicked node - getElementAtEvent