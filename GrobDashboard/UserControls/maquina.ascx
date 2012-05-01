<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="maquina.ascx.cs" Inherits="GrobDashboard.UserControls.maquina" %>
<%@ Import Namespace="System.ComponentModel" %>

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
<script type="text/javascript">
    function exibeInfo(maquina) {
        $("'div[id^=info"+<%=OMaquina.Id%>+"]'").hide();
        $("#info" + maquina).show();
    }
    
</script>
<div class="containerMaquina" idmaquina="<%=IdMaquina%>">
    <div class="imagemMaquina">   
        <img width="135px" height="135px" src="../Imagens/<%=OMaquina.CorDaMaquina()%>" onclick="abrePopUpInfo(<%=IdMaquina%>)"/>        
    </div>

    <div class="nomeMaquina"><asp:Label id="lblNomeMaquina" runat="server"></asp:Label></div>
</div>

<div id="dialog-maquina<%=IdMaquina%>" class="dialog-maquina" title="<%=NomeMaquina%>">
    <div id="descMaquina" class="descMaquina">
         <asp:Repeater ID="infoMaq" runat="server">
            <ItemTemplate>
            <div id="info<%=OMaquina.Id%>_<%#DataBinder.Eval(Container.DataItem,"NumSeq")%>" style="display: <%# Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex")) == 0 ? "display":"none" %> ;">
                    <b>Projeto:</b> <%#DataBinder.Eval(Container.DataItem,"Projeto")%> <br/>
                    <b>Número Grob:</b> <%#DataBinder.Eval(Container.DataItem,"NumGrob")%> <br/>
                    <b>ID Stamm: </b> <%#DataBinder.Eval(Container.DataItem,"IdStamm")%> <br/>
                    <b>Ordem: </b><%#DataBinder.Eval(Container.DataItem,"Ordem")%><br/>
                    <b>Confirmação:</b> <%#DataBinder.Eval(Container.DataItem,"NumSeq")%><br />
                    <b>Operação: </b><%#DataBinder.Eval(Container.DataItem,"NumOperacao")%><br />
                    <b>Início da Operação:</b> <%#DataBinder.Eval(Container.DataItem,"DataInicio")%> <br />                                     
            </div>
            </ItemTemplate>
        </asp:Repeater>
        
        <br />

        <b>Operações em Aberto:</b>
        <asp:Repeater ID="Maq" runat="server">
            <ItemTemplate>           
                <label style="font-size: 15px;text-decoration: underline; font-weight: bold;color: <%# Convert.ToInt32(DataBinder.Eval(Container, "ItemIndex")) % 2 == 0 ? "black":"blue" %>" value="<%=OMaquina.Id%>_<%#DataBinder.Eval(Container.DataItem,"NumSeq")%>"
                onclick="javascript: exibeInfo('<%=OMaquina.Id%>_<%#DataBinder.Eval(Container.DataItem,"NumSeq")%>')">
                <%#DataBinder.Eval(Container.DataItem,"NumSeq")%></label>
            </ItemTemplate>
        </asp:Repeater>        
    </div>
    
    <div class="graficosMaq">
   
           <div id="chart_div<%=IdMaquina%>" style="float: left;"></div>
           <div id="paradas_div<%=IdMaquina%>"style="float: right;">Não Possui Dados</div>

    <select id="intervaloDatas" onchange="geraGraficos(<%=IdMaquina%>,this.value)">
            <option value="1" selected="selected">24 Horas</option>
            <option value="7" >1 Semana</option>
            <option value="30" >1 Mês</option>
    </select>
    </div>
</div>
