using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity; 

namespace GrobDashboard.Domain
{
    [Table("SPI_VW_OPERACOES_EM_ABERTO_POR_MAQUINA")]
    public class InformacoesMaquina
    {
        [Column("ID_ORDEM_MAQ")]
        [Key]
        public Int64 IdOrdemMaq { get; set; }
        [Column("ID_MAQUINA")]
        public int IdMaquina { get; set; }
        [Column("DSC_PROJETO")]
        public String Projeto { get; set; }
        [Column("NUM_GROB")]
        public String NumGrob { get; set; }
        [Column("ID_STAMM")]
        public String IdStamm { get; set; }
        [Column("DSC_ORDEM")]
        public String Ordem { get; set; }
        [Column("NUM_SEQ")]
        public String NumSeq { get; set; }
        [Column("NUM_OPERACAO")]
        public String NumOperacao { get; set; }
        [Column("DT_INICIO")]
        public DateTime DataInicio { get; set; }
    }
}