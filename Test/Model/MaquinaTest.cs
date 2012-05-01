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

            List<InformacoesMaquina> informacoesMaquinas = db.InformacoesMaquinas.Where(info => info.IdMaquina == 110).ToList();

            Assert.That(informacoesMaquinas, Is.Not.Null);
        }

        [Test] public void
               PegaStatusDaMaquina()
        {
            Maquina maquina = db.Maquinas.Find(11);
            Assert.That(maquina.Status(), Is.EqualTo("Ativa"));

            maquina = null;
            maquina = db.Maquinas.Find(23);
            Assert.That(maquina.Status(), Is.EqualTo("Parada"));

            maquina = null;
            maquina = db.Maquinas.Find(41);
            Assert.That(maquina.Status(), Is.EqualTo("Manutenção"));

            maquina = null;
            maquina = db.Maquinas.Find(100);
            Assert.That(maquina.Status(), Is.EqualTo("Manutenção"));


        }
        

        [Test]
        public void DeveListarTop5ParadasMaquina()
        {
            List<ParadasMaquina> paradasMaquinas = db.ParadasMaquinas.OrderByDescending(pd => pd.Id).Take(5).ToList();

            Assert.That(paradasMaquinas.Count, Is.EqualTo(5));
        }
        
        [Test]
        public void DeveListarTop5ParadasPorMaquinaEmPeriodo()
        {
            DateTime dateTime = DateTime.Now.AddDays(-50);
            List<IGrouping<int?, ParadasMaquina>> paradasMaquinas =
                db.ParadasMaquinas.Where(m => m.IdMaquina == 6 &&
                    m.DataInicio > dateTime).GroupBy(m => m.IdMotivo1).ToList();
            Assert.That(paradasMaquinas.Count(), Is.EqualTo(5));
        }
    }
   
}
