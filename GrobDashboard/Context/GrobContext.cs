using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GrobDashboard.Domain;

namespace GrobDashboard.Context
{
    public class GrobContext:DbContext
    {
        public DbSet<TipoMaquina> TipoMaquinas { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<InformacoesMaquina> InformacoesMaquinas { get; set; }
    }
}