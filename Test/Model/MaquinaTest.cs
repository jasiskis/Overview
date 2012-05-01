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

        public MaquinaTest()
        {
        }

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
            List<IGrouping<string, ParadasMaquina>> paradasMaquinas =
                db.ParadasMaquinas.Where(m => m.IdMaquina == 6 &&
                    m.DataInicio > dateTime).GroupBy(m => m.Motivo1.Descricao).ToList();
            Assert.That(paradasMaquinas.Count(), Is.EqualTo(5));
        }

        [Test]
        public void DeveListarParadaEMostrarDescricaoDoMotivo()
        {
            List<ParadasMaquina> paradasMaquinas = db.ParadasMaquinas.Where(pd => pd.IdMotivo1 == 38).Take(5).ToList();

            Assert.That(paradasMaquinas[0].Motivo1.Descricao, Is.EqualTo("Eficiência"));
        }

        [Test]
        public void DeveListarTop5ParadasPorMaquinaEmPeriodoResultandoHorasParado()
        {
            DateTime dateTime = DateTime.Now.AddDays(-50);
            List<IGrouping<string, ParadasMaquina>> paradasMaquinas =
                db.ParadasMaquinas.Where(m => m.IdMaquina == 6 &&
                    m.DataInicio > dateTime).GroupBy(m => m.Motivo1.Descricao).ToList();


            List<DadosGrafico> dadosGraficos = new List<DadosGrafico>();

            foreach (var paradasMaquina in paradasMaquinas)
            {
                TimeSpan? intervaloTempo = TimeSpan.Zero;
                foreach (var maquina in paradasMaquina)
                {
                    intervaloTempo = intervaloTempo +(maquina.DataFim - maquina.DataInicio);
                }
                dadosGraficos.Add(new DadosGrafico(paradasMaquina.Key, intervaloTempo));
            }
            dadosGraficos = dadosGraficos.OrderByDescending(d => d.IntervaloTempo).ToList();

            Assert.That(dadosGraficos[0].Descricao, Is.EqualTo("Geral"));
        }

        [Test]
        public void DeveRetornarDisponibilidadeDaMaquinaEm1Dia()
        {
          DateTime dateTime = DateTime.Now.AddDays(-1);

            List<ParadasMaquina> paradasMaquinas =
                db.ParadasMaquinas.Where(m => m.IdMaquina == 6 &&
                    m.DataFim > dateTime || m.DataInicio > dateTime).ToList();

            TimeSpan? intervaloTempo = TimeSpan.Zero;
            foreach (var paradasMaquina in paradasMaquinas)
            {
                if(paradasMaquina.DataInicio < dateTime)
                {
                    paradasMaquina.DataInicio = dateTime;
                }
                intervaloTempo = intervaloTempo + (paradasMaquina.DataFim - paradasMaquina.DataInicio);
            }
            
            List<ParadasMaquina> maquinas = paradasMaquinas.Where(p => p.IdMotivo1 == 39).ToList();
            TimeSpan? intervaloTempoGeral = TimeSpan.Zero;
            foreach (var paradasMaquina in maquinas)
            {
                if (paradasMaquina.DataInicio < dateTime)
                {
                    paradasMaquina.DataInicio = dateTime;
                }
                intervaloTempoGeral = intervaloTempoGeral + (paradasMaquina.DataFim - paradasMaquina.DataInicio);
            }

            if (intervaloTempo == null)
            {
                intervaloTempo = TimeSpan.Zero;
            }
            if (intervaloTempoGeral == null)
            {
                intervaloTempoGeral = TimeSpan.Zero;
            }

            double totalHoras = intervaloTempo.Value.TotalHours;
            double totalHorasGerais = intervaloTempoGeral.Value.TotalHours;

            int disponibilidade = (int) (((24 - totalHoras/24 - totalHorasGerais)*100)/24);
            
            Assert.That(disponibilidade, Is.EqualTo(95));
        }
        
    }
   
}
