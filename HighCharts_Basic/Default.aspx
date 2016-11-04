<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HighCharts_Basic.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

       
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <div id="container"></div>

    </form>
</body>
<script src="/scripts/jquery.js"></script>
<script src="/scripts/highcharts.js"></script>
<script src="/scripts/exporting.js"></script>
<script type="text/javascript">
    $(function(){
        $.ajax({
            type: "POST",
            url: "Default.aspx/getInformationControl",
            data: "{}",
            contentType: "application/json",
            dataType: "json",
            success: function (Result){
                Result = Result.d;
                var data = [];
                
                var result = Result.split(";");
                for (var i = 0; i < result.length; i++) {
                    var datas = result[i].split(",");
                    if (datas[0].length > 1) {
                        var serie = new Array(datas[0], parseInt(datas[1]));
                        data.push(serie);
                    }
                }
                generarGrafico(data);
                
            },
            error: function (Result) {
                alert("There was an error");
            }
        });
    });


    function generarGrafico(series) {
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
                    data: series
                }]
            });

        });
    }

</script>

<%--<script src="scripts/Grafico.js"></script>--%>
    <%--<script>
        $(function () {

            Highcharts.setOptions({
                colors: ['#4DE510', '#FBFF00', '#FFB600', '#E51A10', '#7F3D91']
            });


            $('#container').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 1,//null,
                    plotShadow: false
                },
                title: {
                    text: 'Estado de controles asignados'
                },
                subtitle:{
                    text: 'Gráfico 1'
                },
                tooltip: {
                    pointFormat: '{series.name}: {point.y} (<b>{point.percentage:.1f}%</b>)'
                },
                plotOptions: {
                    series: {
                        cursor: 'pointer',
                        events: {
                            click: function () {
                                alert('Direccionar a solo esos controles');
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
            // #7F3D91 morado / rojo #E51A10 / verde #4DE510  / amarillo #FBFF00 / anaranjado #FFB600
        });

    </script>--%>
</html>
