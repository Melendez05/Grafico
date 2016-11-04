

$(function () {

    

    Highcharts.setOptions({
        colors: ['#4DE510', '#FBFF00', '#FFB600', '#E51A10', '#7F3D91']
    });


    $('#container').highcharts({
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: 1,
            plotShadow: false
        },
        title: {
            text: 'Estado de controles asignados'
        },
        subtitle: {
            text: 'Gráfico 1'
        },
        tooltip: {
            pointFormat: '{series.name}: {point.y} (<b>{point.percentage:.1f}%</b>)',
            
        },
        plotOptions: {
            series: {
                cursor: 'pointer',
                events: {
                    click: function () {                        
                        //window.location = "http://www.cristalab.com";
                    }
                }
            },
            pie: {

                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.y}',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    }

                }
            }
        },
        series: [{
            type: 'pie',
            name: 'Cantidad',
            data: [

                ['Asignados recientes', 8],
                ['Estado 75%', 5],
                ['Estado 90%', 2],
                ['Estado 100%', 7],
                {
                    name: 'Estado mayor al 100%',
                    y: 10,
                    sliced: true,
                    selected: true
                }
            ]
        }]
    });
    
});