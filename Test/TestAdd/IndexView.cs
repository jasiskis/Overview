using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteMedia.WatinExtension.WebTests;
using WatiN.Core;

namespace Test.TestAdd
{
    public class IndexView : Page
    {
        private const string TipoMaquinaList = "selectable";

        public Ul TipoMaquina
        {
            get { return Document.ElementOfType<Ul>(TipoMaquinaList); }
        }
    }

}
