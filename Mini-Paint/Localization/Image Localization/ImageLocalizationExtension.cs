using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mini_Paint
{
    public class ImageLocalizationExtension : Binding
    {
        public ImageLocalizationExtension(string name) : base("[" + name + "]")
        {
            this.Mode = BindingMode.OneWay;
            this.Source = ImageTranslationSource.Instance;
            this.Converter = new BitmapToImageSourceConverter();
        }
    }
}
