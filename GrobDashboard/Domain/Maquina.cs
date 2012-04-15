using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity; 

namespace GrobDashboard.Domain
{
    [Table("SPI_TB_MAQUINA")]
    public class Maquina
    {
        [Column("ID_MAQUINA")]
        [Key]
        public int Id { get; set; }
        [Column("NUM_MAQUINA")]
        public String Nome { get; set;}
        [Column("DSC_MAQUINA")]
        public String Desc { get; set;}

        [Column("ID_TIPO_MAQ")]
        public int IdTipoMaquina { get; set;}
        [ForeignKey("IdTipoMaquina")]
        public virtual TipoMaquina Tipo { get; set; }
    }
    
}