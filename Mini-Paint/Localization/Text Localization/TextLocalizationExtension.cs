using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mini_Paint
{
    public class TextLocalizationExtension : Binding
    {
        public TextLocalizationExtension(string name) : base("[" + name + "]")
        {
            this.Mode = BindingMode.OneWay;
            this.Source = TextTranslationSource.Instance;
        }
    }
}
