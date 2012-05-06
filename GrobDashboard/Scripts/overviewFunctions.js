/*
    #instanciaObjetos.

    Feito assim para sempre que for necessário recarregar a página, quando
    novos elementos forem inseridos é só chamar ese métodol
*/
$(instanciaObjetos);

function carregarMaquinasDosCookies() {
    var maquinas = $.cookie('maquinas').split(";");

    for (var i = 0; i < maquinas.length; i++) {
        var dados = maquinas[i].split(":");
        if (dados[0] != "" && dados[1] != "") {
            carregaMaquinaPorIdMaquinaEPosicao(dados[0], dados[1]);
        }
    }
}

function carregaMaquinaPorIdMaquinaEPosicao(pos, maq) {
    var responsew = GeradorDeMaquina("RetriveWebControl", ["id", maq]);

    $("[pos = '" + pos + "']").droppable('disable');
    $("[pos = '" + pos + "']").removeAttr('style');
    $("[pos = '" + pos + "']").removeClass('maquinasDroppable');
    $("[pos = '" + pos + "']").addClass('maquinaSemDroppable');
    $("[pos = '" + pos + "']").attr('maq', maq);
    $("[pos = '" + pos + "']").append(responsew);
    $(instanciaObjetos);
}

function instanciaObjetos() {


    $('.maquinaDrag').draggable({
            cursor: 'move',
            containment: 'document',
            helper: objetoQueMostraEnquantoDragging,
            snap: '.maquinasDroppable .trashCanDroppable',
            revert: 'invalid'
        });

    $('.maquinasDroppable').droppable({
            accept: '.maquinaDrag , .containerMaquina',
            drop: dropMaquina
        });

    $('.trashCanDroppable').droppable({
        accept: '.maquinaDrag , .containerMaquina',
        drop: deletaMaquina
    });

    $('.dialog-maquina').dialog({
            autoOpen: false,
            height: 300,
            width: 1250,
            position: ({ my: 'left', at: 'left bottom', of: $('#contentCentral') })
        });

    $('.containerMaquina').draggable({
            cursor: 'move',
            containment: 'document',
            snap: '.maquinasDroppable',
            revert: true
        });

    $("#selectable").selectable();
    $("#selectable").bind("selectablestop", function(event) {
        var result = "";
        $(".ui-selected", this).each(function() {
            result += this.getAttribute("idtipomaquina") + ";";
        });

        CarregaMaquinasDoTipo("CarregaTiposMaquina", ["ids", result]);
    });

    $(".botao").button();
}

// ############################################################################


/* * *
    Eventos Drag and DROP
*/

function dropMaquina(event, ui) {

    //se for um container de droppable sem nenhuma máquina entra aqui.
    if ($(ui.draggable).hasClass('maquinaDrag')) {
        //Checa se Máquina não existe no dashboard.
        var checkMaquina = ui.draggable.attr('id');

        var maquinaExistente = $("[maq=" + checkMaquina + "]");

        //Se não existe adiciona máquina
        if (maquinaExistente.attr("maq") == undefined) {

            var responsew = GeradorDeMaquina("RetriveWebControl", ["id", ui.draggable.attr('id')]);

            $(this).droppable('disable');
            $(this).removeAttr('style');
            $(this).addClass('maquinaSemDroppable');
            $(this).removeClass('maquinasDroppable');
            $(this).attr('maq', ui.draggable.attr('id'));
            $(this).append(responsew);
            ui.draggable.css("background-color", "#CCC123");
            ui.draggable.draggable('option', 'revert', false);

            $(instanciaObjetos);

        } else {
            $("#textWarning").html("");
            $("#textWarning").append("Máquina já está no Dashboard.");
            $("#warningmessage").click();
            return false;
        }
    } //se for uma máquina que já ta na tela pra outro lugar entra aqui. 
    else if ($(ui.draggable).hasClass('containerMaquina')) {
        var responsew = GeradorDeMaquina("RetriveWebControl", ["id", ui.draggable.attr('idmaquina')]);

        var idmaq = ui.draggable.attr('idmaquina');

        $('[maq=' + idmaq + ']').removeAttr('style');
        $('[maq=' + idmaq + ']').addClass('maquinasDroppable');
        $('[maq=' + idmaq + ']').droppable('enable');
        $("[maq=" + idmaq + "]").attr("maq", " ");

        $(this).droppable('disable');
        $(this).removeAttr('style');
        $(this).removeClass('maquinasDroppable');
        $(this).addClass('maquinaSemDroppable');
        $(this).attr('maq', ui.draggable.attr('idmaquina'));
        $(this).append(responsew);
        ui.draggable.css("background-color", "#CCC123");
        ui.draggable.draggable('option', 'revert', false);
        ui.draggable.remove();

        $(instanciaObjetos);
    }

}
function deletaMaquina(event, ui) {
    var idmaq = ui.draggable.attr('idmaquina');
    $('[maq=' + idmaq + ']').droppable('enable');
    $('[maq=' + idmaq + ']').removeClass('maquinaSemDroppable');
    $('[maq=' + idmaq + ']').addClass('maquinasDroppable');
    $("[maq=" + idmaq + "]").attr("maq", "");

    ui.draggable.draggable('option', 'revert', false);
    ui.draggable.remove();
    
    $("#"+idmaq).css("background-color", "#006A00");
}

function objetoQueMostraEnquantoDragging(event) {
    return '<div class="maquinaDragHelper"><div class="middle"><div class="texto">' + $(this).attr("descricao") + '</div></div></div>';
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

function CarregaMaquinasDoTipo(fn, paramArray) {
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
            $("#maquinasPlaceHolder").empty();
            for (var i = 0; i < response.d.length; i++) {
                var maquinaExistente = $("div [maq=" + response.d[i].Id + "]");

                //Se não tem adiciona com a cor normal
                if (maquinaExistente.attr("maq") == undefined) {
                    $("#maquinasPlaceHolder").prepend('<div id="' + response.d[i].Id + '" descricao="' + response.d[i].Desc + '" class="maquinaDrag">' + response.d[i].Desc + '</div>');
                } else {//Altera a Cor
                    $("#maquinasPlaceHolder").prepend('<div id="' + response.d[i].Id +
                        '" descricao="' + response.d[i].Desc + '" class="maquinaDrag" style="background-color: #CCC123;">'
                        + response.d[i].Desc + '</div>');
                }
            }
            $(instanciaObjetos);
        }
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
            success: function(response) {
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
    geraGraficosDisponibilidade(id, 1);
    geraGraficosParadas(id, 1);
    $('.dialog-maquina').dialog('close');
    $('#dialog-maquina' + id).dialog('open');
}


/* ###################################################################
    Gráficos.

   ###################################################################
*/

function geraGraficos(maq, dias) {

    geraGraficosDisponibilidade(maq, dias);
    geraGraficosParadas(maq, dias);
}

function geraGraficosParadas(maq, dias) {

    //*******************************
    //Pega Valores das paradas de máquina do WebMethod na Overview.aspx.cs
    //*******************************
    var pagePath = window.location.pathname;
    var dadosGrafico;
    //Create list of parameters in the form:
    //{"paramName1":"paramValue1","paramName2":"paramValue2"}
    var paramList = '';
    paramList = '{"id":"' + maq + '","dias":"' + dias + '"}';
    //Call the page method
    $.ajax({
            type: "POST",
            url: pagePath + "/RetornaValoresGraficoParadas",
            contentType: "application/json; charset=utf-8",
            data: paramList,
            async: false,
            dataType: "json",
            success: function(response) {
                dadosGrafico = response.d;
            }
        });
    //Chama Função que tem os comando do google para gerar o gráfico
    populaGraficoDeParadas(maq, dadosGrafico);
}

function geraGraficosDisponibilidade(maq, dias) {

    //*******************************
    //Pega Valores da disponibilidade  de máquina do WebMethod na Overview.aspx.cs
    //*******************************
    var pagePath = window.location.pathname;
    var dadosGrafico;
    //Create list of parameters in the form:
    //{"paramName1":"paramValue1","paramName2":"paramValue2"}
    var paramList = '';
    paramList = '{"id":"' + maq + '","dias":"' + dias + '"}';
    //Call the page method
    $.ajax({
            type: "POST",
            url: pagePath + "/RetornaDadosDisponibilidade",
            contentType: "application/json; charset=utf-8",
            data: paramList,
            async: false,
            dataType: "json",
            success: function(response) {
                dadosGrafico = response.d;
            }
        });
    //Chama Função que tem os comando do google para gerar o gráfico
    populaGaugeDiponibilidade(maq, dadosGrafico);
}

google.load('visualization', '1', { packages: ['gauge'] });
google.load('visualization', '1', { packages: ['corechart'] });
google.setOnLoadCallback(populaGaugeDiponibilidade);

function populaGaugeDiponibilidade(id, dadosGrafico) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Label');
    data.addColumn('number', 'Value');
    data.addRows([
            ['Disp.', parseInt(dadosGrafico)]
        ]);

    var options = {
        width: 200,
        height: 200,
        redFrom: 0,
        redTo: 49,
        yellowFrom: 50,
        yellowTo: 70,
        greenFrom: 71,
        greenTo: 100,
        minorTicks: 5
    };

    var chart = new google.visualization.Gauge(document.getElementById('chart_div' + id));
    chart.draw(data, options);
}

function populaGraficoDeParadas(id, dadosGrafico) {
    // Create and populate the data table.
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Parada');
    data.addColumn('number', 'Minutos');

    var split = dadosGrafico.split(";");

    if (!(split.length == 1 && split[0] == "")) {
        data.addRows(split.length);
        for (var i = 0; i < split.length; i++) {
            var dados = split[i].split(":");
            data.setValue(i, 0, dados[0]);
            data.setValue(i, 1, parseFloat(dados[1]));
        }
    } else {
        data.addRows(1);
        data.setValue(0, 0, "Sem Dados");
        data.setValue(0, 1, 1);
    }

    var options = {
        title: 'Top 5 Paradas (Minutos)',
        is3D: true,
        labels: 'name',
        backgroundColor: '#F5F3E5',
        legend: { position: 'right', textStyle: { fontSize: 10 } },
        heigth: 300,
        width: 330,
        chartArea: { width: "90%", height: "80%" },
        fontSize: 16
    };


    // Create and draw the visualization.
    new google.visualization.PieChart(document.getElementById('paradas_div' + id)).
        draw(data, options);

    // ######################################################
}

//Funções para salvar as escolhas de máquina em Cache

function gravaMaquinasNosCookies() {
    var posMaquinas = "";
    for (var j = 1; j <= 24; j++) {
        var maquina = $("[pos = '" + j + "']").attr("maq");
        posMaquinas = posMaquinas + j + ":" + maquina + ";";
    }

    $.cookie('maquinas', posMaquinas, { expires: 1000 });
    showMessage('sucess');
    showMessage('sucess');
}

function resetarCookies() {
    $.cookie('maquinas', "", { expires: 1000 });
}