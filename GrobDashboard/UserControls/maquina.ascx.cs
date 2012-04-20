using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrobDashboard.Context;
using GrobDashboard.Domain;

namespace GrobDashboard.UserControls
{
    public partial class maquina : System.Web.UI.UserControl
    {
        public String NomeMaquina { get; set; }
        public int IdMaquina { get; set; }
        public Maquina OMaquina { get; set;}
        

        private GrobContext db = new GrobContext();
        
        public void setaDados()
        {
            lblNomeMaquina.Text = this.NomeMaquina;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OMaquina = db.Maquinas.Find(IdMaquina);
            NomeMaquina = OMaquina.Nome;

            lblNomeMaquina.Text = OMaquina.Nome;
        }

    }
}