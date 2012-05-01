using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity; 

namespace GrobDashboard.Domain
{
    [Table("SPI_TB_MAQUINA_PARADAS")]
    public class ParadasMaquina
    {
        [Column("ID_MAQUINA_PARADAS")]
        [Key]
        public Int64 Id { get; set; }
        [Column("DT_INICIO")]
        public DateTime DataInicio { get; set; }
        [Column("DT_FIM")]
        public DateTime? DataFim { get; set; }
        [Column("ID_MOTIVO1")]
        public int? IdMotivo1 { get; set; }
        [Column("ID_MOTIVO2")]
        public int? IdMotivo2 { get; set; }
        [Column("ID_MOTIVO3")]
        public int? IdMotivo3 { get; set; }
        [Column("ID_MAQUINA")]
        public int IdMaquina { get; set; }
        [Column]
        [ForeignKey("IdMaquina")]
        public virtual Maquina Maquina { get; set; }
        [Column]
        [ForeignKey("IdMotivo1")]
        public virtual Motivo1 Motivo1 { get; set; }
        
    }

    [Table("SPI_TB_MOTIVO1")]
    public class Motivo1
    {
        [Key]
        [Column("ID_MOTIVO1")]
        public int IdMotivo { get; set; }
        [Column("DSC_MOTIVO1")]
        public String Descricao { get; set; }
    }
}