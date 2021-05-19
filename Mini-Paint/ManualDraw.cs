using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mini_Paint
{
    public class ManualDraw
    {
        public int DrawingMode = 3; // 1- Rectangle(Enabled), 2- Ellipse(Enabled), 3- Disabled
        public Point StartPoint = new Point();
        public Rectangle Rectangle = new Rectangle();
        public Ellipse Ellipse = new Ellipse();

        public void MouseMove(Canvas canvas, MouseEventArgs e)
        {
            if (DrawingMode == 1)
            {
                if (e.GetPosition(canvas).X - StartPoint.X < 0)
                {
                    Canvas.SetLeft(Rectangle, e.GetPosition(canvas).X);
                    Rectangle.Width = -(e.GetPosition(canvas).X - StartPoint.X);
                }
                else
                {
                    Rectangle.Width = e.GetPosition(canvas).X - StartPoint.X;
                }
                if (e.GetPosition(canvas).Y - StartPoint.Y < 0)
                {
                    Canvas.SetTop(Rectangle, e.GetPosition(canvas).Y);
                    Rectangle.Height = -(e.GetPosition(canvas).Y - StartPoint.Y);
                }
                else
                {
                    Rectangle.Height = e.GetPosition(canvas).Y - StartPoint.Y;
                }
                Rectangle.RenderTransform = new RotateTransform(0, Rectangle.Width / 2, Rectangle.Height / 2);
            }
            if (DrawingMode == 2)
            {
                if (e.GetPosition(canvas).X - StartPoint.X < 0)
                {
                    Canvas.SetLeft(Ellipse, e.GetPosition(canvas).X);
                    Ellipse.Width = -(e.GetPosition(canvas).X - StartPoint.X);
                }
                else
                {
                    Ellipse.Width = e.GetPosition(canvas).X - StartPoint.X;
                }
                if (e.GetPosition(canvas).Y - StartPoint.Y < 0)
                {
                    Canvas.SetTop(Ellipse, e.GetPosition(canvas).Y);
                    Ellipse.Height = -(e.GetPosition(canvas).Y - StartPoint.Y);
                }
                else
                {
                    Ellipse.Height = e.GetPosition(canvas).Y - StartPoint.Y;
                }
                Ellipse.RenderTransform = new RotateTransform(0, Ellipse.Width / 2, Ellipse.Height / 2);
            }
        }
    }
}
