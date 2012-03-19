/*
    #instanciaObjetos.

    Feito assim para sempre que for necessário recarregar a página, quando
    novos elementos forem inseridos é só chamar ese métodol
*/
$(instanciaObjetos);

function instanciaObjetos() {
    $('.maquinaDrag').draggable({
        cursor: 'move',
        containment: 'document',
        helper: objetoQueMostraEnquantoDragging,
        snap: '.maquinasDroppable',
        revert: 'invalid'
    });

    $('.maquinasDroppable').droppable({
        accept: '.maquinaDrag , .containerMaquina',
        drop: dropMaquina
    });

    $('.dialog-maquina').dialog({
        autoOpen: false,
        height: 250,
        width: 1250,
        position: ({ my: 'left', at: 'left bottom', of: $('#contentCentral') })
    });
    
    $('.containerMaquina').draggable({
        cursor: 'move',
        containment: 'document',
        snap: '.maquinasDroppable',
        revert: true
    });

    $("#selectable").selectable({
        stop: function () {
            var id = Math.floor(Math.random() * 101);
            $("#maquinasPlaceHolder").prepend('<div id="' + id + '" class="maquinaDrag">Máquina ' + id + '</div>');
            $(instanciaObjetos);
        }
    });
    $(".botao").button();
}
// ############################################################################


/* * *
    Eventos Drag and DROP
*/
function dropMaquina(event, ui) {
    if ($(ui.draggable).hasClass('maquinaDrag')) {
        var responsew = GeradorDeMaquina("RetriveWebControl", ["id", ui.draggable.attr('id')]);

        $(this).droppable('disable');
        $(this).removeAttr('style');
        $(this).addClass('maquinaSemDroppable');
        $(this).append(responsew);
        ui.draggable.css("background-color", "#CCC123");
        ui.draggable.draggable('option', 'revert', false);

        $(instanciaObjetos);
    } else if ($(ui.draggable).hasClass('containerMaquina')) {
        var responsew = GeradorDeMaquina("RetriveWebControl", ["id", ui.draggable.attr('idmaquina')]);

        var idmaq = ui.draggable.attr('idmaquina');
        $('[idmaquina]=' + idmaq).parent().droppable('enable');
        $('[idmaquina]=' + idmaq).parent().removeClass('maquinaSemDroppable');
        $('[idmaquina]=' + idmaq).parent().addClass('maquinasDroppable');
        
        
        $(this).droppable('disable');
        $(this).removeAttr('style');
        $(this).addClass('maquinaSemDroppable');
        $(this).append(responsew);
        ui.draggable.css("background-color", "#CCC123");
        ui.draggable.draggable('option', 'revert', false);
        ui.draggable.remove();
       
        $(instanciaObjetos);
    }
}

function objetoQueMostraEnquantoDragging(event) {
    return '<div class="maquinaDragHelper">Máquina ' + $(this).attr("id") + '</div>';
}


/*
    Chamadas dos WebMethods no Asp.Net

    A Comunicação acontece por meio do JSON.

    Eu chamo um WebMethod que está no código do .Net
    e dependendo do resultado faço algo.
*/
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

//Chama WebMethod no asp, e retorna um HTML.
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

// * * *
/*
    ##########################################################################
*/

function abrePopUpInfo(id) {
    drawChart(id);
    drawVisualization(id);
    $('#dialog-maquina' + id).dialog('open');
}


/* ###################################################################
    Gráficos.

   ###################################################################
*/
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

    var chart = new google.visualization.Gauge(document.getElementById('chart_div' + id));
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
        legend: { position: 'right', textStyle: { fontSize: 10} },
        heigth: 300, width: 330,
        chartArea: { width: "90%", height: "80%" },
        fontSize: 16
    };


    // Create and draw the visualization.
    new google.visualization.PieChart(document.getElementById('paradas_div' + id)).
            draw(data, options);
    
// ######################################################
}