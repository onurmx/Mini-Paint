using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using System.Resources;


namespace Mini_Paint
{
    public class TextTranslationSource : INotifyPropertyChanged
    {
        private static readonly TextTranslationSource instance = new TextTranslationSource();
        private readonly ResourceManager resManager = Properties.Resources.ResourceManager;
        private CultureInfo currentCulture = null;
        public event PropertyChangedEventHandler PropertyChanged;

        public static TextTranslationSource Instance
        {
            get { return instance; }
        }

        public string this[string key]
        {
            get { return this.resManager.GetString(key, this.currentCulture); }
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
