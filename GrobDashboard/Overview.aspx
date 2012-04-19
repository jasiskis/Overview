<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="GrobDashboard.Overview" %>
<%@ Register tagPrefix="uc" tagName="maquina" src="UserControls/maquina.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script src="JQuery/js/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="JQuery/js/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
<link href="JQuery/css/south-street/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
<link href="Styles/Overview.css" rel="stylesheet" type="text/css" />
<script src="Scripts/overviewFunctions.js" type="text/javascript"></script>

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
