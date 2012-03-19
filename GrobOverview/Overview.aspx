<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Overview.aspx.vb" Inherits="GrobOverview.Overview" %>



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
            snap: '#maquinasDroppable',
            revert: true
        });
        $('#maquinasDroppable').droppable({
            drop: chamaEvento
        });
    }

    $(function () {
        $("#selectable").selectable();
        $(".botao").button();
    });
    
    function chamaEvento(event, ui) {
        ChamaWebMethodNoAsp("fazAlgo", [], alerta, alerta2);
        ui.draggable.css("background-color", "#CCC123");
        ui.draggable.draggable("disable");
        ui.draggable.draggable('option', 'revert', false);
        
    }
    function alerta() {
        $('#mais').css("display", "inline-block21");
    }
    
    function alerta2() {
        alert('alerta2');
    }

    function myHelper(event) {
        return '<div class="maquinaDragHelper">Máquina 1</div>';
    }


    function ChamaWebMethodNoAsp(fn, paramArray, successFn, errorFn) {
        var pagePath = window.location.pathname;
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
            dataType: "json",
            success: successFn,
            error: errorFn
        });
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
   &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True"></asp:ScriptManager>
   
        <div id="content">
            <div id="leftContent">
                <div id="seletorTipoMaquina">

                   <ol id="selectable">
                        <li name="tipoMaquina1" class="ui-widget-content">Tipo de Máquina 1</li>
                        <li class="ui-widget-content">Tipo de Máquina 2</li>
                        <li class="ui-widget-content">Tipo de Máquina 3</li>
                        <li class="ui-widget-content">Tipo de Máquina 4</li>                        
                   </ol>

                </div>
                <div id="maquinasPlaceHolder">
                    
                    <div id="maquinaDrag" class="maquinaDrag">Máquina 1</div>

                </div>

                <div id="botoesPlaceHolder">
                    <asp:Button id="btnSalvar" CssClass="botao" Text="Salvar" runat="server"/>
                    <asp:Button id="btnResetar" CssClass="botao" Text="Resetar" runat="server"/>
                </div>
            </div>
            
            <div id="contentCentral">
                <div id="contentMaquinas">
                        <div id="maquinasDroppable"></div>

                        <div id="mais" style="display:none" runat="server" ></div>
                </div>            
            </div>                    
        </div>
    </form>
</body>
</html>
