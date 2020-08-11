$(document).ready(function () {

    console.log("here");
    //generateChartData("/api/UserProgressAll/");

    $("#buttonDisplayGraphAll").click(function () {
        // redraw chart with stanza data
        generateChartData("/api/UserProgressAll/");
    });

    $("#buttonDiplayGraphRelevant").click(function () {
        // redraw chart with stanza data
        generateChartData("/api/UserProgressRelevant/");
    });



    function BuildChart(progressWeight, progressBodyFat, goalWeight, goalBodyFat, dates, maxBodyFat) {
        var weightColour = "rgba(230, 57, 70, 1)";
        var bodyFatColour = "rgba(29, 53, 87, 1)";
        var weightGoalColour = "rgba(230, 57, 70, 0.5)";
        var bodyFatGoalColour = "rgba(29, 53, 87, 0.5)";

        var data = {
            labels: dates,
            datasets: [
                {
                    data: progressWeight,
                    label: "Weight",
                    lineTension: 0.3,
                    fill: false,
                    backgroundColor: weightColour,
                    borderColor: weightColour,
                    pointRadius: 3,
                    pointBackgroundColor: weightColour,
                    pointBorderColor: weightColour,
                    pointHoverRadius: 3,
                    pointHoverBackgroundColor: weightColour,
                    pointHoverBorderColor: weightColour,
                    pointHitRadius: 10,
                    pointBorderWidth: 2,
                    yAxisID: 'y-axis-1',
                    spanGaps: true
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
                    pointHoverRadius: 2,
                    pointHoverBackgroundColor: bodyFatColour,
                    pointHoverBorderColor: bodyFatColour,
                    pointHitRadius: 5,
                    pointBorderWidth: 1.6,
                    yAxisID: 'y-axis-2',
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
                    borderDash: [10]

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
                    borderDash: [10]
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
                    responsive: true, // Instruct chart JS to respond nicely.
                    maintainAspectRatio: false, // Add to prevent default behavior of full-width/height 
                    scales: {
                        xAxes: [
                            {
                                scaleLabel: {
                                    display: true,
                                    labelString: "Date"
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
                                //type: "date",
                                //type: "time",
                                //time: {
                                //    displayFormats: {
                                //        //'millisecond': 'MMM DD',
                                //        //'second': 'MMM DD',
                                //        //'minute': 'MMM DD',
                                //        //'hour': 'MMM DD',
                                //        'day': 'MMM DD',
                                //        //'week': 'MMM DD',
                                //        //'month': 'MMM DD',
                                //        //'quarter': 'MMM DD',
                                //        //'year': 'MMM DD'
                                //    }
                                //}
                            }
                        ],
                        yAxes: [
                            {
                                id: 'y-axis-1',
                                type: 'linear',
                                position: 'left',
                                scaleLabel: {
                                    display: true,
                                    labelString: "Weight"
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
                            //{
                            //    id: 'y-axis-3',
                            //    type: 'linear',

                            //    ticks: {
                            //        //min: minBodyFat,
                            //        //max: maxBodyFat
                            //        //suggestedMin: minBodyFat,
                            //        //suggestedMax: maxBodyFat
                            //    }
                            //},
                            //{
                            //    id: 'y-axis-4',
                            //    type: 'linear',

                            //    ticks: {
                            //        //min: minBodyFat,
                            //        //max: maxBodyFat
                            //        //suggestedMin: minBodyFat,
                            //        //suggestedMax: maxBodyFat
                            //    }
                            //}
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
                    return e.WeightInKg;
                });
                var progressBodyFat = json.map(function (e) {
                    return e.BodyFat;
                });
                var goalWeight = json.map(function (e) {
                    return e.GoalWeight;
                });
                var goalBodyFat = json.map(function (e) {
                    return e.GoalBodyFat;
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
                    maxBodyFat); // Pass in data and call the chart
            }
        };

        xhttp.open("GET", apiString, false);
        xhttp.send();
    }
    generateChartData("/api/UserProgressAll/");


});
//https://www.chartjs.org/docs/
//Show picture when clicked node - getElementAtEvent