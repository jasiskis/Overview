using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace GrobDashboard.Domain
{
    [Table("SPI_TB_MAQ_TIPO")]
    public class TipoMaquina
    {
            [Column("ID_TIPO_MAQ")]
            [Key]
            public int Id { get; set; }
            [Column("DSC_TIPO_MAQ")]
            public String Desc { get; set; }

        }
        
    }

