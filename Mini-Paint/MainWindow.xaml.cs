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

        private void DrawRectangle(double left, double top, int width, int height, byte red, byte green, byte blue)
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
                }
                else
                {
                    ((Ellipse)sender).Effect = null;
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
                }
                else
                {
                    ((Rectangle)sender).Effect = null;
                }
            }
        }
    }
}
