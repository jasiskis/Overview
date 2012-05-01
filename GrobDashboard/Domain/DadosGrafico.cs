using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrobDashboard.Domain
{
    public class DadosGrafico
    {
        public DadosGrafico(string key, TimeSpan? intervaloTempo)
        {
            Descricao = key;
            IntervaloTempo = intervaloTempo;
        }

        public String Descricao { get; set; }
        public TimeSpan? IntervaloTempo { get; set; }
    }
}