<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="GrobDashboard.Overview" %>
<%@ Register tagPrefix="uc" tagName="maquina" src="UserControls/maquina.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script src="JQuery/js/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="JQuery/js/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
<link href="JQuery/css/south-street/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
<link href="Styles/Overview.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">

    $(init);
    function init() {
        $('.maquinaDrag').draggable({
            cursor: 'move',
            containment: 'document',
            helper: myHelper,
            snap: '.maquinasDroppable',
            revert: 'invalid'
        });
        
        $('.maquinasDroppable').droppable({
            accept: '.maquinaDrag',
            drop: dropMaquina
        });

        $('.dialog-maquina').dialog({
                autoOpen: false,
                height: 250,
                width: 1250,
                position: ({my:'left',at:'left bottom', of: $('#contentCentral') })
            });
}

    $(function () {
        $("#selectable").selectable({
            stop: function () {
                var id = Math.floor(Math.random() * 101);
                $("#maquinasPlaceHolder").prepend('<div id="'+id+'" class="maquinaDrag">Máquina '+id+'</div>');
                $(init);
            }
        });
        $(".botao").button();
    });

    function dropMaquina(event, ui) {
        var responsew = GeradorDeMaquina("RetriveWebControl", ["id",ui.draggable.attr('id')]);

        $(this).droppable('disable');
        $(this).removeAttr('style');
        $(this).addClass('maquinaSemDroppable');
        $(this).append(responsew);
        ui.draggable.css("background-color", "#CCC123");
        ui.draggable.draggable('option', 'revert', false);

        $(init);
    }

    function myHelper(event) {
        return '<div class="maquinaDragHelper">Máquina '+$(this).attr("id")+'</div>';
    }


    function ChamaWebMethodNoAsp(fn, paramArray, successFn, errorFn) {
        var pagePath = window.location.pathname;
        var htmlGeradoDaMaquina;
        //Create list of parameters in the form:
        //{"paramName1":"paramValue1","paramName2":"paramValue2"}
        var paramList = '';
        if (paramArray.length > 0) {
            for (var i = 0; i < paramArray.length; i += 2) {
                if (paramList.length > 0) paramList += ',';
                paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
            }
        }
        paramList = '{' + paramList + '}';
        //Call the page method
        $.ajax({
            type: "POST",
            url: pagePath + "/" + fn,
            contentType: "application/json; charset=utf-8",
            data: paramList,
            async: false,
            dataType: "json",
            success: successFn,
            error: errorFn
        });
    }

    function GeradorDeMaquina(fn, paramArray) {
        var pagePath = window.location.pathname;
        var htmlGeradoDaMaquina;
        //Create list of parameters in the form:
        //{"paramName1":"paramValue1","paramName2":"paramValue2"}
        var paramList = '';
        if (paramArray.length > 0) {
            for (var i = 0; i < paramArray.length; i += 2) {
                if (paramList.length > 0) paramList += ',';
                paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
            }
        }
        paramList = '{' + paramList + '}';
        //Call the page method
        $.ajax({
            type: "POST",
            url: pagePath + "/" + fn,
            contentType: "application/json; charset=utf-8",
            data: paramList,
            async: false,
            dataType: "json",
            success: function (response) {
                htmlGeradoDaMaquina = response.d;
            }
        });
        return htmlGeradoDaMaquina;
    }
    
    function abrePopUpInfo(id) {
        drawChart(id);
        drawVisualization(id);
        $('#dialog-maquina'+id).dialog('open');
    }


    google.load('visualization', '1', { packages: ['gauge'] });
    google.load('visualization', '1', { packages: ['corechart'] });
    google.setOnLoadCallback(drawChart);

    function drawChart(id) {
        var porcDisponibilidade = Math.floor(Math.random() * 101);
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Label');
        data.addColumn('number', 'Value');
        data.addRows([
          ['Disp.', porcDisponibilidade]
        ]);

        var options = {
            width: 200, height: 200,
            redFrom: 30, redTo: 60,
            yellowFrom: 61, yellowTo: 85,
            greenFrom: 86, greenTo: 100,
            minorTicks: 5
        };

        var chart = new google.visualization.Gauge(document.getElementById('chart_div'+id));
        chart.draw(data, options);
    }

    function drawVisualization(id) {
        // Create and populate the data table.
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Parada');
        data.addColumn('number', 'Minutos');
        data.addRows(5);
        data.setValue(0, 0, 'Manutenção');
        data.setValue(0, 1, 200);
        data.setValue(1, 0, 'Falha');
        data.setValue(1, 1, 120);
        data.setValue(2, 0, 'Problema');
        data.setValue(2, 1, 80);
        data.setValue(3, 0, 'Projeto');
        data.setValue(3, 1, 40);
        data.setValue(4, 0, 'Almoço');
        data.setValue(4, 1, 30);

        var options = {
            title: 'Top 5 Paradas',
            is3D: true,
            backgroundColor: '#F5F3E5',
            legend: { position: 'right',textStyle:{fontSize: 10} },
            heigth: 300, width: 330,
            chartArea: { width: "90%", height: "80%" },
            fontSize: 16
        };
        
        
        // Create and draw the visualization.
        new google.visualization.PieChart(document.getElementById('paradas_div'+id)).
            draw(data, options);
    }
    </script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True"></asp:ScriptManager>
   
        <div id="content">
             <div id="leftContent">
                <div id="seletorTipoMaquina">

                   <ul id="selectable">
                        <li id="tipoMaquina1" class="ui-widget-content">Tipo de Máquina 1</li>
                        <li class="ui-widget-content">Tipo de Máquina 2</li>
                        <li class="ui-widget-content">Tipo de Máquina 3</li>
                        <li class="ui-widget-content">Tipo de Máquina 4</li>                        
                   </ul>

                </div>
                <div id="maquinasPlaceHolder">                   
                 
                </div>

                <div id="botoesPlaceHolder">
                    <asp:Button id="btnSalvar" CssClass="botao" Text="Salvar" runat="server"/>
                    <asp:Button id="btnResetar" CssClass="botao" Text="Resetar" runat="server"/>                    
                </div>
            </div>
            
            <div id="contentCentral">
                <div id="contentMaquinas">
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>
                        <div class="maquinasDroppable"></div>                                                              
                </div>            
           </div>                           
        </div>
    </form>
</body>
</html>
