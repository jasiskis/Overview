using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using GrobDashboard.Context;
using GrobDashboard.Domain;
using GrobDashboard.UserControls;

namespace GrobDashboard
{
    public partial class Overview : System.Web.UI.Page
    {

        private GrobContext db = new GrobContext();

        [WebMethod]
        public static string RetriveWebControl(String id)
        {
            Page pg = new Page();
            string path = @"\UserControls\maquina.ascx";
            maquina control = (maquina) pg.LoadControl(path);
            control.NomeMaquina = "Maquina "+id;
            control.IdMaquina = int.Parse(id);
            control.setaDados();
            pg.Controls.Add(control);
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
