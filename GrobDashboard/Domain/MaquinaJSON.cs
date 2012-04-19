using System;

namespace GrobDashboard.Domain
{
    public class MaquinaJSON
    {
        public MaquinaJSON(int id, string desc, string nome, int idTipoMaquina)
        {
            this.Id = id;
            this.Desc = desc;
            this.Nome = nome;
            this.IdTipoMaquina = IdTipoMaquina;
        }

        public int Id { get; set; }
            public String Nome { get; set; }
            public String Desc { get; set; }
            public int IdTipoMaquina { get; set; }
            
        } 
    }
