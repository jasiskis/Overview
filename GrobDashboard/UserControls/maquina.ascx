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
<div class="containerMaquina" idmaquina="<%=IdMaquina%>">
    <div class="imagemMaquina">   
        <img width="135px" height="135px" src="../Imagens/MaquinaVerde.png" onclick="abrePopUpInfo(<%=IdMaquina%>)"/>        
    </div>

    <div class="nomeMaquina"><asp:Label id="lblNomeMaquina" runat="server"></asp:Label></div>
</div>
<div id="dialog-maquina<%=IdMaquina%>" class="dialog-maquina" title="<%=NomeMaquina%>">
    <div class="descMaquina">
        <b>Informações:</b><br/>
        Projeto: Operando <br/>
        Cliente: 12345<br/>
        ID Stamm: Teste <br/>
        Ordem: 01/01/1990 <br/>
        Confirmação: Manutenção <br />
        Confirmação: Operação <br />
        Início da Operação: 12/12/2012 12:12 <br />
    </div>
    <div class="graficosMaq">
           <div id="chart_div<%=IdMaquina%>" style="float: left;"></div>
           <div id="paradas_div<%=IdMaquina%>"style="float: right;"></div>
    </div>
</div>
