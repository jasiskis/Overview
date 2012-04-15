using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LiteMedia.WatinExtension.WebTests;
using Test.TestAdd;
using WatiN.Core;
using NUnit.Framework;

namespace Test.Integrated
{

    public class InteractionManager
    {
        private Browser browser;

        public InteractionManager(ApplicationRunner aplicacao)
        {
            this.browser = aplicacao.browser;
        }

        public void clicaNoPrimeiroTipoDeMaquina()
        {
            var index = browser.Page<IndexView>();
            Li li = index.TipoMaquina.Items[0];
            browser.Eval("$('#selectable').data('selectable')._mouseStop(null)");
        }

        public void checaSeApareceuMaquinaNoMenu()
        {
            Div div = browser.Div("maquina1");
            String textoDoDiv = div.Text;

            Assert.That(textoDoDiv, Is.StringContaining("Máquina"));
        }

        public void arrastaMaquinaAteoMais()
        {
            throw new NotImplementedException();
        }

        public void checaSeAMaquinaFoiCriada()
        {
            throw new NotImplementedException();
        }

        public void selecionaItemNoCombo()
        {
            SelectList combo = browser.SelectList("combo123");
            combo.SelectByValue("Afonso");
        }

        public void clicanoBotao()
        {
            browser.Button("botao").Click();
        }

        public void checaTexto()
        {
            Div div = browser.Div("div");
            string text = div.Text;
            Assert.That(text, Is.EqualTo("Afonso"));
        }
    }
}
