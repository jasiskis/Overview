using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WatiN.Core;

namespace Test.Integrated
{
    [TestFixture]
    class InteracaoTelaTest
    {
        private ApplicationRunner aplicacao;
        private InteractionManager tela;

        [SetUp]
        public void Start(){
            aplicacao = new ApplicationRunner();
            tela = new InteractionManager(aplicacao);
        }

  
        [Test]
        [STAThread]
        public void ClicaEmUmTipoDeMaquinaEApareceUmaMaquinaNoMenu()
        {
            tela.clicaNoPrimeiroTipoDeMaquina();
            tela.checaSeApareceuMaquinaNoMenu();

        }
        [Test]
        [STAThread]
        public void ClicaEArrastaMaquinaEChecaSeApareceuMaquina(){
            tela.clicaNoPrimeiroTipoDeMaquina();
            tela.checaSeApareceuMaquinaNoMenu();
            tela.arrastaMaquinaAteoMais();
            tela.checaSeAMaquinaFoiCriada();
        }
        
        [TearDown]
        public void finaliza()
        {
            aplicacao = null;
            tela = null;
        }


    }
}
