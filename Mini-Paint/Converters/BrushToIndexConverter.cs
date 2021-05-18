using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Reflection;

namespace Mini_Paint
{
    public class BrushToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                List<ColorInfo> ColorInformations = new List<ColorInfo>();
                var properties = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
                ColorInformations = properties.Select(prop =>
                {
                    var color = (Color)prop.GetValue(null, null);
                    return new ColorInfo()
                    {
                        Name = prop.Name,
                        RGB = color
                    };
                }).ToList();

                int index;
                if ((index = ColorInformations.FindIndex(c => c.RGB == ((SolidColorBrush)value).Color)) != -1)
                {
                    return index;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<ColorInfo> ColorInformations = new List<ColorInfo>();
            var properties = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
            ColorInformations = properties.Select(prop =>
            {
                var color = (Color)prop.GetValue(null, null);
                return new ColorInfo()
                {
                    Name = prop.Name,
                    RGB = color
                };
            }).ToList();

            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = ColorInformations[(int)value].RGB;
            return solidColorBrush;
        }
    }
}
