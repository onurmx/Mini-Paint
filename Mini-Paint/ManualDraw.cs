using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;

namespace Mini_Paint
{
    public class ManualDraw
    {
        public static int DrawingMode = 3; // 1- Rectangle(Enabled), 2- Ellipse(Enabled), 3- Disabled
        public static Point StartPoint = new Point();
        public static Rectangle Rectangle = new Rectangle();
        public static Ellipse Ellipse = new Ellipse();
    }
}
