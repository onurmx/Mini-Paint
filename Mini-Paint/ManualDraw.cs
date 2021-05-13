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
        public int DrawingMode = 3; // 1- Rectangle(Enabled), 2- Ellipse(Enabled), 3- Disabled
        public Point StartPoint = new Point();
        public Rectangle Rectangle = new Rectangle();
        public Ellipse Ellipse = new Ellipse();
    }
}
