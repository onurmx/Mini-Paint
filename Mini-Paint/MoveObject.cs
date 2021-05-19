using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mini_Paint
{
    public class MoveObject
    {
        public bool isEnabled = false;
        public Point StartPoint = new Point();
        public List<UIElement> SelectedObjects = new List<UIElement>();
        public List<Point> InitialPositions = new List<Point>();

        public void AssignInitialPosition()
        {
            foreach (UIElement o in SelectedObjects)
            {
                InitialPositions.Add(new Point(Canvas.GetLeft(o), Canvas.GetTop(o)));
            }
        }

        public void MouseMove(Canvas canvas, MouseEventArgs e)
        {
            if (isEnabled == true)
            {
                for (int i = 0; i < SelectedObjects.Count; i++)
                {
                    Canvas.SetLeft(SelectedObjects[i], InitialPositions[i].X + (e.GetPosition(canvas).X - StartPoint.X));
                    Canvas.SetTop(SelectedObjects[i], InitialPositions[i].Y + (e.GetPosition(canvas).Y - StartPoint.Y));
                }
            }
        }
    }
}
