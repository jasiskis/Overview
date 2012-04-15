using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
            control.IdMauqina = int.Parse(id);
            control.setaDados();
            pg.Controls.Add(control);
            StringWriter output = new StringWriter();
            HttpContext.Current.Server.Execute(pg, output, true);
            return output.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<TipoMaquina> listTiposMaquinas = db.TipoMaquinas.OrderBy(tp => tp.Desc).ToList();

            tiposMaquina.DataSource = listTiposMaquinas;
            tiposMaquina.DataBind();

            
        }
    }
}