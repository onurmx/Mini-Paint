using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;
using System.ComponentModel;

namespace Mini_Paint
{
    public class ImageTranslationSource : INotifyPropertyChanged
    {
        private static readonly ImageTranslationSource instance = new ImageTranslationSource();
        private readonly ResourceManager resManager = Properties.Resources.ResourceManager;
        private CultureInfo currentCulture = null;
        public event PropertyChangedEventHandler PropertyChanged;

        public static ImageTranslationSource Instance
        {
            get { return instance; }
        }

        public object this[string key]
        {
            get { return this.resManager.GetObject(key, this.currentCulture); }
        }

        public CultureInfo CurrentCulture
        {
            get { return this.currentCulture; }
            set
            {
                if (this.currentCulture != value)
                {
                    this.currentCulture = value;
                    var @event = this.PropertyChanged;
                    if (@event != null)
                    {
                        @event.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                    }
                }
            }
        }
    }
}
