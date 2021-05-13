using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Effects;

namespace Mini_Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RandomShapeCreator(4);
        }

        private void RandomShapeCreator(int n)
        {
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                if (random.Next(0, 2) < 1)
                {
                    int sizeX = random.Next(100, 200);
                    int sizeY = random.Next(100, 200);
                    DrawEllipse((double)random.Next(sizeX, (int)MyCanvas.ActualWidth) - sizeX,
                                (double)random.Next(sizeY, (int)MyCanvas.ActualHeight) - sizeY,
                                sizeX,
                                sizeY,
                                (byte)random.Next(0, 255),
                                (byte)random.Next(0, 255),
                                (byte)random.Next(0, 255));
                }
                else
                {
                    int sizeX = random.Next(100, 200);
                    int sizeY = random.Next(100, 200);
                    DrawRectangle((double)random.Next(sizeX, (int)MyCanvas.ActualWidth) - sizeX,
                                  (double)random.Next(sizeY, (int)MyCanvas.ActualHeight) - sizeY,
                                  sizeX,
                                  sizeY,
                                  (byte)random.Next(0, 255),
                                  (byte)random.Next(0, 255),
                                  (byte)random.Next(0, 255));
                }
            }
        }

        private void DrawEllipse(double left, double top, int width, int height, byte red, byte green, byte blue)
        {
            Ellipse ellipse = new Ellipse();
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = Color.FromArgb(255, red, green, blue);

            ellipse.Fill = solidColorBrush;
            ellipse.Width = width;
            ellipse.Height = height;
            ellipse.Cursor = Cursors.Hand;
            ellipse.MouseRightButtonDown += ShapeGlow;

            MyCanvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, left);
            Canvas.SetTop(ellipse, top);
        }

        private Rectangle DrawRectangle(double left, double top, int width, int height, byte red, byte green, byte blue)
        {
            Rectangle rectangle = new Rectangle
            {
                Width = width,
                Height = height
            };
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = Color.FromArgb(255, red, green, blue);

            rectangle.Fill = solidColorBrush;
            rectangle.Cursor = Cursors.Hand;
            rectangle.MouseRightButtonDown += ShapeGlow;

            MyCanvas.Children.Add(rectangle);
            Canvas.SetLeft(rectangle, left);
            Canvas.SetTop(rectangle, top);

            return rectangle;
        }

        private void ShapeGlow(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse)
            {
                if (((Ellipse)sender).Effect == null)
                {
                    ((Ellipse)sender).Effect = new DropShadowEffect
                    {
                        Color = new Color { A = 255, R = 255, G = 255, B = 255 },
                        Direction = 270,
                        ShadowDepth = 0,
                        BlurRadius = 50
                    };
                    Canvas.SetZIndex((Ellipse)sender, 1);
                }
                else
                {
                    ((Ellipse)sender).Effect = null;
                    Canvas.SetZIndex((Ellipse)sender, 0);
                }
            }
            if (sender is Rectangle)
            {
                if (((Rectangle)sender).Effect == null)
                {
                    ((Rectangle)sender).Effect = new DropShadowEffect
                    {
                        Color = new Color { A = 255, R = 255, G = 255, B = 255 },
                        Direction = 270,
                        ShadowDepth = 0,
                        BlurRadius = 50
                    };
                    Canvas.SetZIndex((Rectangle)sender, 1);
                }
                else
                {
                    ((Rectangle)sender).Effect = null;
                    Canvas.SetZIndex((Rectangle)sender, 0);
                }
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            List<UIElement> DeleteList = new List<UIElement>();
            foreach (var o in MyCanvas.Children)
            {
                if (o is Ellipse)
                {
                    if (((Ellipse)o).Effect != null)
                    {
                        DeleteList.Add((Ellipse)o);
                    }
                }
                if (o is Rectangle)
                {
                    if (((Rectangle)o).Effect != null)
                    {
                        DeleteList.Add((Rectangle)o);
                    }
                }
            }
            foreach (var o in DeleteList)
            {
                if (o is Ellipse)
                {
                    MyCanvas.Children.Remove((Ellipse)o);
                }
                if (o is Rectangle)
                {
                    MyCanvas.Children.Remove((Rectangle)o);
                }
            }
        }

        private void RandomColorsButtonClick(object sender, RoutedEventArgs e)
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            Random random = new Random();
            foreach (var o in MyCanvas.Children)
            {
                if (o is Ellipse)
                {
                    if (((Ellipse)o).Effect != null)
                    {
                        var red = (byte)random.Next(0, 255);
                        var green = (byte)random.Next(0, 255);
                        var blue = (byte)random.Next(0, 255);
                        solidColorBrush.Color = Color.FromArgb(255, red, green, blue);
                        ((Ellipse)o).Fill = solidColorBrush;
                    }
                }
                if (o is Rectangle)
                {
                    if (((Rectangle)o).Effect != null)
                    {
                        var red = (byte)random.Next(0, 255);
                        var green = (byte)random.Next(0, 255);
                        var blue = (byte)random.Next(0, 255);
                        solidColorBrush.Color = Color.FromArgb(255, red, green, blue);
                        ((Rectangle)o).Fill = solidColorBrush;
                    }
                }
            }
        }

        private void ShapeButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender == RectangleButton)
            {

            }
            if (sender == EllipseButton)
            {

            }
        }

        private void MyCanvasLeftDown(object sender, MouseButtonEventArgs e)
        {
            ManualDraw.StartPoint = e.GetPosition(MyCanvas);
            ManualDraw.Rectangle = DrawRectangle(e.GetPosition(MyCanvas).X, e.GetPosition(MyCanvas).Y, 0, 0, 255, 255, 255);
        }

        private void MyCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (ManualDraw.Rectangle != new Rectangle())
            {
                if (e.GetPosition(MyCanvas).X - ManualDraw.StartPoint.X < 0)
                {
                    Canvas.SetLeft(ManualDraw.Rectangle, e.GetPosition(MyCanvas).X);
                    ManualDraw.Rectangle.Width = -(e.GetPosition(MyCanvas).X - ManualDraw.StartPoint.X);
                }
                else
                {
                    ManualDraw.Rectangle.Width = e.GetPosition(MyCanvas).X - ManualDraw.StartPoint.X;
                }
                if (e.GetPosition(MyCanvas).Y - ManualDraw.StartPoint.Y < 0)
                {
                    Canvas.SetTop(ManualDraw.Rectangle, e.GetPosition(MyCanvas).Y);
                    ManualDraw.Rectangle.Height = -(e.GetPosition(MyCanvas).Y - ManualDraw.StartPoint.Y);
                }
                else
                {
                    ManualDraw.Rectangle.Height = e.GetPosition(MyCanvas).Y - ManualDraw.StartPoint.Y;
                }
            }
        }

        private void MyCanvasLeftUp(object sender, MouseButtonEventArgs e)
        {
            ManualDraw.StartPoint = new Point();
            ManualDraw.Rectangle = new Rectangle();
        }
    }
}
