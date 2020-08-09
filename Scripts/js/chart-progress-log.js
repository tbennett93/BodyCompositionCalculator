function BuildChart(progressWeight, progressBodyFat, goalWeight, goalBodyFat, dates) {
    var data = {
        labels: dates,
        datasets: [{

            data: progressWeight,
            label: "Weight",
            lineTension: 0.3,
            fill: false,
            backgroundColor: "rgba(78, 115, 223, 1)",
            borderColor: "rgba(78, 115, 223, 1)",
            pointRadius: 3,
            pointBackgroundColor: "rgba(78, 115, 223, 1)",
            pointBorderColor: "rgba(78, 115, 223, 1)",
            pointHoverRadius: 3,
            pointHoverBackgroundColor: "rgba(78, 115, 223, 1)",
            pointHoverBorderColor: "rgba(78, 115, 223, 1)",
            pointHitRadius: 10,
            pointBorderWidth: 2,
            yAxisID: 'y-axis-1',
            spanGaps:true
        },
        {
            data: progressBodyFat,
            label: "Body Fat %",
            lineTension: 0.4,
            fill:false,
            backgroundColor: "rgba(255, 3, 7, 1)",
            borderColor: "rgba(255, 3, 7, 1)",
            pointRadius: 5,
            pointBackgroundColor: "rgba(255, 3, 7, 1)",
            pointBorderColor: "rgba(255, 3, 7, 1)",
            pointHoverRadius: 2,
            pointHoverBackgroundColor: "rgba(68, 105, 213, 1)",
            pointHoverBorderColor: "rgba(255, 3, 7, 1)",
            pointHitRadius: 5,
            pointBorderWidth: 1.6,
            yAxisID: 'y-axis-2',
            spanGaps: true
        },
        {
            data: goalWeight,
            label: "Goal Weight",
            lineTension: 0.4,
            fill:true,
            backgroundColor: "rgba(0, 3, 7, 1)",
            borderColor: "rgba(0, 3, 7, 1)",
            pointRadius: 5,
            pointBackgroundColor: "rgba(0, 3, 7, 1)",
            pointBorderColor: "rgba(0, 3, 7, 1)",
            pointHoverRadius: 2,
            pointHoverBackgroundColor: "rgba(0, 105, 213, 1)",
            pointHoverBorderColor: "rgba(0, 3, 7, 1)",
            pointHitRadius: 15,
            pointBorderWidth: 1.6,
            yAxisID: 'y-axis-1',
            spanGaps: false
        },
        {
            data: goalBodyFat,
            label: "Goal BodyFat",
            lineTension: 0.4,
            fill:true,
            backgroundColor: "rgba(150, 150, 150, 1)",
            borderColor: "rgba(150, 150, 150, 1)",
            pointRadius: 5,
            pointBackgroundColor: "rgba(150, 150, 150, 1)",
            pointBorderColor: "rgba(150, 150, 150, 1)",
            pointHoverRadius: 2,
            pointHoverBackgroundColor: "rgba(150, 150, 150, 1)",
            pointHoverBorderColor: "rgba(150, 150, 150, 1)",
            pointHitRadius: 15,
            pointBorderWidth: 1.6,
            yAxisID: 'y-axis-2',
            spanGaps: false
        }],
    };
    var ctx = document.getElementById("myAreaChart").getContext("2d");
    var myAreaChart = new Chart(ctx, {
        type: "line",
        data: data,
        options: {
            responsive: true,
            legend: {
                display: true
            },
            responsive: true, // Instruct chart JS to respond nicely.
            maintainAspectRatio: false, // Add to prevent default behavior of full-width/height 
            scales: {
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: "Date"
                    },
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
                }],
                yAxes: [{
                    id: 'y-axis-1',
                    type: 'linear',
                    position:'left',
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
                        //suggestedMax: maxBodyFat
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
    return myAreaChart;
}

var xhttp = new XMLHttpRequest();
xhttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        var json = JSON.parse(this.response);
        // Map JSON labels  back to values array
        var progressWeight = json.map(function (e) {
            return e.WeightInKg;
        });
        var progressBodyFat = json.map(function (e) {
            return e.BodyFat ;
        });
        var goalWeight = json.map(function (e) {
            return e.GoalWeight ;
        });
        var goalBodyFat = json.map(function (e) {
            return e.GoalBodyFat ;
        });

        // Map JSON values back to values array
        var dates = json.map(function (e) {
            //return (new Date(e.Date)); // Divide to billions in units of ten
            //return (e.Date); // Divide to billions in units of ten
            //return (new Date(e.Date)); // Divide to billions in units of ten
            return (moment(new Date(e.Date)).format("DD MMM YYYY")); // Divide to billions in units of ten
        });

        //Set the minimum weight in the Y axis
        var maxWeightInArray = Math.max.apply(Math, progressWeight); // 1
        var minWeightInArray = Math.min.apply(Math, progressWeight); // 1
        var minWeight = minWeightInArray - ((maxWeightInArray - minWeightInArray) * 0.1); // 1
        var maxWeight = maxWeightInArray + ((maxWeightInArray - minWeightInArray) * 0.1); // 1

        var minBodyFatInArray = Math.min.apply(Math, progressBodyFat);
        var maxBodyFatInArray = Math.max.apply(Math, progressBodyFat);
        var minBodyFat = minBodyFatInArray - ((maxBodyFatInArray - minBodyFatInArray) * 0.1); // 1
        var maxBodyFat = maxBodyFatInArray + ((maxBodyFatInArray - minBodyFatInArray)*0.1); // 1


        BuildChart(progressWeight, progressBodyFat, goalWeight, goalBodyFat, dates); // Pass in data and call the chart
    }
};
//xhttp.open("GET", "https://forbes400.herokuapp.com/api/forbes400?limit=10", false);
//xhttp.open("GET", "/api/UserProgressLog/", false);
xhttp.open("GET", "/api/UserProgressLogwithgoal/", false);
xhttp.send();

//TODO - order GET request
//TODO - get current user's log
//TODO add overlaying body fat % chart
//TODO highlight notes with photo or display photos above each node that holds a picture