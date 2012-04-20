using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrobDashboard.Context;
using GrobDashboard.Domain;
using NUnit.Framework;
namespace Test.Model
{
    
    [TestFixture]
    class MaquinaTest
    {
        private GrobContext db = new GrobContext();

        [Test]
        public void
            ListaTodasMaquinas()
        {
            List<Maquina> lista;
            lista = db.Maquinas.ToList();

            Assert.That(lista.Count, Is.GreaterThan(0)); 
        }
        [Test]
        public void
            listaTipoDeMaquina()
        {
            List<TipoMaquina> lista;
            lista = db.TipoMaquinas.ToList();
            
            Assert.That(lista.Count, Is.GreaterThan(0));
        }
        [Test]
        public void
            ListaMaquinasPorGrupo()
        {
            List<TipoMaquina> tipoMaquinas = db.TipoMaquinas.ToList();
            int id = tipoMaquinas[1].Id;
            List<Maquina> listMaquinas = db.Maquinas.Where(m => m.IdTipoMaquina == id).ToList();
            
            Assert.That(listMaquinas.Count, Is.GreaterThan(0));
        }

        [Test]
        public void
            PegaInformacoesMaquinaPorMaquina()
        {
            List<Maquina> lista;
            lista = db.Maquinas.ToList();

            InformacoesMaquina info = db.InformacoesMaquinas.Find(lista[0].Id);

           Assert.That(info, Is.Not.Null);
        }
    }
   
}
