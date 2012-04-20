using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity; 

namespace GrobDashboard.Domain
{
    public class InformacoesMaquina
    {
        [Key]
        public int IdMaquina { get; set; }
        public String Projeto { get; set; }
        public String NumGrob { get; set; }
        public String IdStamm { get; set; }
        public String Ordem { get; set; }
        public String NumSeq { get; set; }
        public String NumOperacao { get; set; }
        public String DataInicio { get; set; }
    }
}