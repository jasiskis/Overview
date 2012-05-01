using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GrobDashboard.Context;

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
        [Column("ATIVO")]
        public Boolean Ativo { get; set; }
        [Column]
        [ForeignKey("IdTipoMaquina")]
        public virtual TipoMaquina Tipo { get; set; }


        public string Status()
        {
            const int ID_MOTIVO_MAQUINA_MANUTENCAO_PREV = 6;
            const int ID_MOTIVO_MAQUINA_MANUTENCAO_CORR = 18;

            GrobContext db = new GrobContext();

            List<ParadasMaquina> paradas = db.ParadasMaquinas.Where(p => p.IdMaquina == this.Id && p.DataFim == null).ToList();

            if(paradas.Count == 0)
            {
                return "Ativa";
            }else{
                List<ParadasMaquina> paradasMaquinas = db.ParadasMaquinas.Where(p => p.IdMaquina == this.Id && p.DataFim == null
                                                                     && (p.IdMotivo1 == ID_MOTIVO_MAQUINA_MANUTENCAO_CORR
                                                                         || p.IdMotivo1 == ID_MOTIVO_MAQUINA_MANUTENCAO_PREV)).ToList();

                if(paradasMaquinas.Count > 0)
                {
                    return "Manutenção";
                }else
                {
                    return "Parada";    
                }
            }

            return null;
        }

        public String CorDaMaquina()
        {
            string status = this.Status();

            if (status == "Ativa")
            {
                return "MaquinaVerde.png";
            }
            else if (status == "Parada")
            {
                return "MaquinaVermelha.png";
            }
            else if(status=="Manutenção")
            {
                return "MaquinaAmarela.png";
            }

            return null;
        }
    }
    
}