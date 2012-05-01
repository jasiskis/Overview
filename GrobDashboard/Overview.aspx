<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="GrobDashboard.Overview" %>
<%@ Register tagPrefix="uc" tagName="maquina" src="UserControls/maquina.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script src="JQuery/js/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="JQuery/js/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
<link href="JQuery/css/south-street/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
<link href="Styles/Overview.css" rel="stylesheet" type="text/css" />
<script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
<script src="Scripts/overviewFunctions.js" type="text/javascript"></script>
<link href="Styles/Message.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Messages.js" type="text/javascript"></script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body onload="carregarMaquinasDosCookies()">
 <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True"></asp:ScriptManager>
   
   
        <div id="content">
               <div id="leftContent">
                <div id="seletorTipoMaquina">

                   <ul id="selectable">              
                        <asp:Repeater ID="tiposMaquina" runat="server">
                            <ItemTemplate>                          
                                <li idtipomaquina="<%#DataBinder.Eval(Container.DataItem,"Id")%>" 
                                class="ui-widget-content"><%#DataBinder.Eval(Container.DataItem,"Desc")%></li>                           
                            </ItemTemplate>
                        </asp:Repeater>     
                   </ul>

                </div>
                <div id="maquinasPlaceHolder">                  
                      
                </div>

                <div id="botoesPlaceHolder">
                    <input type="button" id="btnSalvar" class="botao trigger success-trigger" value="Salvar" onclick="gravaMaquinasNosCookies();"></input>
                    <input type="button" id="btnResetar" class="botao trigger info-trigger" Value="Resetar" onclick="resetarCookies()"/>                    
                </div>
            </div>
            
            <div id="contentCentral">
                <div id="contentMaquinas">
                        <div pos="1" maq="" class="maquinasDroppable"></div>
                        <div pos="2" maq="" class="maquinasDroppable"></div>
                        <div pos="3" maq="" class="maquinasDroppable"></div>
                        <div pos="4" maq="" class="maquinasDroppable"></div>
                        <div pos="5" maq="" class="maquinasDroppable"></div>
                        <div pos="6" maq="" class="maquinasDroppable"></div>
                        <div pos="7" maq="" class="maquinasDroppable"></div>
                        <div pos="8" maq="" class="maquinasDroppable"></div>
                        <div pos="9" maq="" class="maquinasDroppable"></div>
                        <div pos="10" maq="" class="maquinasDroppable"></div>
                        <div pos="11" maq="" class="maquinasDroppable"></div>
                        <div pos="12" maq="" class="maquinasDroppable"></div>
                        <div pos="13" maq="" class="maquinasDroppable"></div>
                        <div pos="14" maq="" class="maquinasDroppable"></div>
                        <div pos="15" maq="" class="maquinasDroppable"></div>
                        <div pos="16" maq="" class="maquinasDroppable"></div>
                        <div pos="17" maq="" class="maquinasDroppable"></div>
                        <div pos="18" maq="" class="maquinasDroppable"></div>
                        <div pos="19" maq="" class="maquinasDroppable"></div>
                        <div pos="20" maq="" class="maquinasDroppable"></div>
                        <div pos="21" maq="" class="maquinasDroppable"></div>
                        <div pos="22" maq="" class="maquinasDroppable"></div>
                        <div pos="23" maq="" class="maquinasDroppable"></div>
                        <div class="trashCanDroppable"></div>                                                              
                </div>            
           </div>                           
        </div>
    </form>

    <div class="info message">
                 <h3>Configurações Resetadas</h3>
                 <p>Suas configurações foram resetadas. Atualize a página para não ter mais máquinas selecionadas.</p>
    </div>
    <input type="button" id="warningmessage" class="triger warning-trigger" style="display: none"/>
    <div class="error message">
                     <h3>Erro!</h3>
                     <p>This is just an error notification message.</p>
    </div>

    <div class="warning message">
                     <h3>Atenção</h3>
                     <p id="textWarning"></p>
    </div>

    <div class="success message">
                     <h3>Configurações Salvas!!!</h3>
                     <p>A partir de Agora toda vez que o Overview for aberto sua configuração aparecerá,
                        caso você não queira mais essa configuração de visualização aperte o Botão resetar.
                     </p>
    </div>
</body>
</html>
