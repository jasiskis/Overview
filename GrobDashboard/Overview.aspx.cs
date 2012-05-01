using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Services;
using GrobDashboard.Context;
using GrobDashboard.Domain;
using GrobDashboard.UserControls;

namespace GrobDashboard
{
    public partial class Overview : System.Web.UI.Page
    {

        private static GrobContext db = new GrobContext();

        [WebMethod]
        public static string RetornaValoresGraficoParadas(int id, int dias)
        {
            DateTime dateTime = DateTime.Now.AddDays(-dias);

            return GeraDadosGrafico(id, dateTime);
        }

        // ***********************************************
        // Método que retorna os dados do gráfico concatenados em uma string
        // só mandar o id da maquina, a data inicial de pesquisa e o context.
        private static string GeraDadosGrafico(int id, DateTime dateTime )
        {
            GrobContext db = new GrobContext();

            List<IGrouping<string, ParadasMaquina>> paradasMaquinas =
                db.ParadasMaquinas.Where(m => m.IdMaquina == id &&
                    (m.DataFim > dateTime || m.DataInicio > dateTime || m.DataFim == null)).GroupBy(m => m.Motivo1.Descricao).ToList();

            List<DadosGrafico> dadosGraficos = new List<DadosGrafico>();

            foreach (var paradasMaquina in paradasMaquinas)
            {
                TimeSpan? intervaloTempo = TimeSpan.Zero;
                foreach (var maquina in paradasMaquina)
                {
                    AcetaListaDeDatas(dateTime, maquina);
                    intervaloTempo = intervaloTempo + (maquina.DataFim - maquina.DataInicio);
                }
                dadosGraficos.Add(new DadosGrafico(paradasMaquina.Key, intervaloTempo));
            }
            dadosGraficos = dadosGraficos.OrderByDescending(d => d.IntervaloTempo).ToList();

            String valoresGrafico = String.Empty;
            foreach (var dadosGrafico in dadosGraficos)
            {
                String desc = dadosGrafico.Descricao;
                if (desc == null)
                {
                    desc = "Sem Motivo";
                }

                if(dadosGrafico.IntervaloTempo == null)
                {
                    dadosGrafico.IntervaloTempo = TimeSpan.Zero;
                }

                valoresGrafico = valoresGrafico + desc + ":" + dadosGrafico.IntervaloTempo.Value.TotalMinutes + ';';
            }
            db.Dispose();

            return valoresGrafico;
        }

        // ******************************************************************
        //**********************************************************************
        [WebMethod]
        public static string RetornaDadosDisponibilidade(int id, int dias)
        {
            DateTime dateTime = DateTime.Now.AddDays(-dias);

            int horasTotais = dias*24;

            return GeraDadosDisponiblidade(id, horasTotais, dateTime).ToString();
        }

        /* Método que implementa o cálculo 
        Fórmula Disponibilidade:
        (24 - Todas as paradas - paradas gerais)*100 /24. */
        private static int GeraDadosDisponiblidade(int id, int horasTotais, DateTime dateTime)
        {
            GrobContext db = new GrobContext();

            List<ParadasMaquina> paradasMaquinas =
                db.ParadasMaquinas.Where(m => m.IdMaquina == id &&
                                              (m.DataFim > dateTime || m.DataInicio > dateTime || m.DataFim == null)).ToList();

            TimeSpan? intervaloTempo = TimeSpan.Zero;
            foreach (var paradasMaquina in paradasMaquinas)
            {
                AcetaListaDeDatas(dateTime, paradasMaquina);
                intervaloTempo = intervaloTempo + (paradasMaquina.DataFim - paradasMaquina.DataInicio);
            }

            //Faz o Calcúlo para saber quais das paradas são gerais.
            List<ParadasMaquina> maquinas = paradasMaquinas.Where(p => p.IdMotivo1 == 39).ToList();
            TimeSpan? intervaloTempoGeral = TimeSpan.Zero;
            foreach (var paradasMaquina in maquinas)
            {
               AcetaListaDeDatas(dateTime, paradasMaquina);
                intervaloTempoGeral = intervaloTempoGeral + (paradasMaquina.DataFim - paradasMaquina.DataInicio);
            }

            if (intervaloTempo == null)
            {
                intervaloTempo = TimeSpan.Zero;
            }
            if (intervaloTempoGeral == null)
            {
                intervaloTempoGeral = TimeSpan.Zero;
            }

            double totalHoras = intervaloTempo.Value.TotalHours;
            double totalHorasGerais = intervaloTempoGeral.Value.TotalHours;

            db.Dispose();
            //Cálculo para saber a disponibilidade.
            return (int) (((horasTotais - (totalHoras - totalHorasGerais))*100)/horasTotais);
        }

        private static void AcetaListaDeDatas(DateTime dateTime, ParadasMaquina paradasMaquina)
        {
            if (paradasMaquina.DataInicio < dateTime)
            {
                paradasMaquina.DataInicio = dateTime;
            }
            if (paradasMaquina.DataFim == null)
            {
                paradasMaquina.DataFim = DateTime.Now;
            }
        }

        // ******************************************************************
        [WebMethod]
        public static string RetriveWebControl(String id)
        {
            Page pg = new Page();
            pg.EnableEventValidation = false;
            HtmlForm htmlForm = new HtmlForm();
            pg.Controls.Add(htmlForm);

            string path = @"\UserControls\maquina.ascx";
            maquina control = (maquina) pg.LoadControl(path);
            control.NomeMaquina = "Maquina "+id;
            control.IdMaquina = int.Parse(id);
            control.setaDados();
            htmlForm.Controls.Add(control);
            StringWriter output = new StringWriter();
            HttpContext.Current.Server.Execute(pg, output, true);
            return output.ToString();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<MaquinaJSON> CarregaTiposMaquina(String ids)
        {
                GrobContext db = new GrobContext();

            String[] listIds = ids.Trim().Split(';');
            List<Maquina> listaComTodasAsMaquinas = new List<Maquina>();

            foreach (var stringId in listIds)
            {
                if (stringId != "")
                {
                    int id = Convert.ToInt32(stringId);
                    List<Maquina> listMaquinas = db.Maquinas.Where(m => m.IdTipoMaquina == id).ToList();
                    listaComTodasAsMaquinas.AddRange(listMaquinas);
                }
            }

            List<MaquinaJSON> maquinaJsons = new List<MaquinaJSON>();
            foreach(Maquina m in listaComTodasAsMaquinas)
            {
                MaquinaJSON maquinaJson = new MaquinaJSON(m.Id,m.Desc,m.Nome,m.IdTipoMaquina);
                maquinaJsons.Add(maquinaJson);
            }

            return maquinaJsons;
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<TipoMaquina> listTiposMaquinas = db.TipoMaquinas.OrderBy(tp => tp.Desc).ToList();

                tiposMaquina.DataSource = listTiposMaquinas;
                tiposMaquina.DataBind();
            }
         
        }
    
    }
}
