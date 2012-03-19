using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrobDashboard.UserControls
{
    public partial class maquina : System.Web.UI.UserControl
    {
        private String _nomeMaquina;
        private int _idMauqina;

        public void setaDados()
        {
            lblNomeMaquina.Text = this._nomeMaquina;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblNomeMaquina.Text = this._nomeMaquina;
        }
        

        public string NomeMaquina
        {
            get { return _nomeMaquina; }
            set { _nomeMaquina = value; }
        }

        public int IdMauqina
        {
            get { return _idMauqina; }
            set { _idMauqina = value; }
        }
    }
}