//-------------
//- PIE CHART -
//-------------
// Get context with jQuery - using jQuery's .get() method.
var pieChartCanvas = $('#pieChart').get(0).getContext('2d')
var pieChart = new Chart(pieChartCanvas)
var PieData = [
    {
        value: parseInt($('#DbVal2').text()),
        color: '#f39c12',
        highlight: '#f39c12',
        label: 'Fresh Orders'
    },
    {
        value: parseInt($('#DbVal3').text()),
        color: '#ffff00',
        highlight: '#ffff00',
        label: 'In Progress'
    },
    {
        value: parseInt($('#DbVal4').text()),
        color: '#3c8dbc',
        highlight: '#3c8dbc',
        label: 'Out For Delivery'
    },
    {
        value: parseInt($('#DbVal5').text()),
        color: '#00a65a',
        highlight: '#00a65a',
        label: 'Delivered'
    },
    {
        value: parseInt($('#DbVal6').text()),
        color: '#f56954',
        highlight: '#f56954',
        label: 'Returned'
    }
]
var pieOptions = {
    //Boolean - Whether we should show a stroke on each segment
    segmentShowStroke: true,
    //String - The colour of each segment stroke
    segmentStrokeColor: '#fff',
    //Number - The width of each segment stroke
    segmentStrokeWidth: 2,
    //Number - The percentage of the chart that we cut out of the middle
    percentageInnerCutout: 50, // This is 0 for Pie charts
    //Number - Amount of animation steps
    animationSteps: 100,
    //String - Animation easing effect
    animationEasing: 'easeOutBounce',
    //Boolean - Whether we animate the rotation of the Doughnut
    animateRotate: true,
    //Boolean - Whether we animate scaling the Doughnut from the centre
    animateScale: true,
    //Boolean - whether to make the chart responsive to window resizing
    responsive: true,
    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    maintainAspectRatio: true,
    //String - A legend template
    legendTemplate: '<ul class="<%=name.toLowerCase()%>-legend"><% for (var i=0; i<segments.length; i++){%><li><span style="background-color:<%=segments[i].fillColor%>"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>'
}
//Create pie or douhnut chart
// You can switch between pie and douhnut using the method below.
pieChart.Doughnut(PieData, pieOptions)
