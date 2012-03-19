<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="maquina.ascx.cs" Inherits="GrobDashboard.UserControls.maquina" %>
<style>
.containerMaquina{height: 170px;width: 170px; background-color: #333333;border:solid 3px white; border-radius: 20px; display: inline-table; margin: 5px;}

.imagemMaquina{text-align: center;}
.imagemMaquina img 
{
   margin-top:2px;
   background-color: White;
  -moz-box-shadow: 0 0 .5em rgba(255, 255, 255, .8);
  -webkit-box-shadow: 0 0 .5em rgba(255, 255, 255, .8);
  box-shadow: 0 0 .5em rgba(0, 0, 0, .8);
  border-radius: 20px;}

.nomeMaquina{color: White;text-align: center; margin:3px; font:20px Arial bold; font-family: Arial;}
.descMaquina{width: 50%;float: left;border-right:dotted 2px black; display:inline;}
.graficosMaq{width: 45%;height:100%;float: right;displau: inline;}
</style>
<div class="containerMaquina">
    <div class="imagemMaquina">   
        <img width="135px" height="135px" src="../Imagens/MaquinaVerde.png" onclick="abrePopUpInfo(<%=IdMauqina%>)"/>        
    </div>

    <div class="nomeMaquina"><asp:Label id="lblNomeMaquina" runat="server"></asp:Label></div>
</div>
<div id="dialog-maquina<%=IdMauqina%>" class="dialog-maquina" title="Máquina <%=IdMauqina%>">
    <div class="descMaquina">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi vestibulum molestie risus, <br/>
        eget tincidunt est iaculis id. Phasellus sed tempor mauris. Donec quis leo tortor. <br/>
        Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. <br/>
        Sed a nisi magna. Fusce nibh sem, ultrices et facilisis quis, luctus sit amet neque. <br/>
        In fringilla risus elit, nec fermentum nibh.<br/>
    </div>
    <div class="graficosMaq">
           <div id="chart_div<%=IdMauqina%>" style="float: left;"></div>
           <div id="paradas_div<%=IdMauqina%>"style="float: right;"></div>
    </div>
</div>
