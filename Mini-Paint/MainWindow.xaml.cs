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
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace Mini_Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random Random = new Random();
        private ManualDraw ManualDraw = new ManualDraw();
        private int LanguageMode = 1; // 1-ENGLISH, 2-POLISH
        private List<ColorInfo> ColorInformations = new List<ColorInfo>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

            RandomShapeCreator(4);
        }

        private void RandomShapeCreator(int n)
        {
            ColorInfo RandomColorInfo;
            for (int i = 0; i < n; i++)
            {
                RandomColorInfo = ColorInformations[Random.Next(0, ColorInformations.Count - 1)];
                if (Random.Next(0, 2) < 1)
                {
                    int sizeX = Random.Next(100, 200);
                    int sizeY = Random.Next(100, 200);
                    DrawEllipse((double)Random.Next(sizeX, (int)MyCanvas.ActualWidth) - sizeX,
                                (double)Random.Next(sizeY, (int)MyCanvas.ActualHeight) - sizeY,
                                sizeX,
                                sizeY,
                                RandomColorInfo.RGB.R,
                                RandomColorInfo.RGB.G,
                                RandomColorInfo.RGB.B);
                }
                else
                {
                    int sizeX = Random.Next(100, 200);
                    int sizeY = Random.Next(100, 200);
                    DrawRectangle((double)Random.Next(sizeX, (int)MyCanvas.ActualWidth) - sizeX,
                                  (double)Random.Next(sizeY, (int)MyCanvas.ActualHeight) - sizeY,
                                  sizeX,
                                  sizeY,
                                  RandomColorInfo.RGB.R,
                                  RandomColorInfo.RGB.G,
                                  RandomColorInfo.RGB.B);
                }
            }
        }

        private Ellipse DrawEllipse(double left, double top, int width, int height, byte red, byte green, byte blue)
        {
            Ellipse ellipse = new Ellipse();
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            RotateTransform rotateTransform = new RotateTransform();
            solidColorBrush.Color = Color.FromArgb(255, red, green, blue);

            ellipse.Fill = solidColorBrush;
            ellipse.Width = width;
            ellipse.Height = height;
            ellipse.RenderTransform = rotateTransform;
            ellipse.Cursor = Cursors.Hand;
            ellipse.MouseLeftButtonDown += ObjectLeftDown;
            ellipse.MouseRightButtonDown += ObjectRightDown;

            MyCanvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, left);
            Canvas.SetTop(ellipse, top);

            return ellipse;
        }

        private Rectangle DrawRectangle(double left, double top, int width, int height, byte red, byte green, byte blue)
        {
            Rectangle rectangle = new Rectangle
            {
                Width = width,
                Height = height
            };
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            RotateTransform rotateTransform = new RotateTransform();
            solidColorBrush.Color = Color.FromArgb(255, red, green, blue);

            rectangle.Fill = solidColorBrush;
            rectangle.RenderTransform = rotateTransform;
            rectangle.Cursor = Cursors.Hand;
            rectangle.MouseLeftButtonDown += ObjectLeftDown;
            rectangle.MouseRightButtonDown += ObjectRightDown;

            MyCanvas.Children.Add(rectangle);
            Canvas.SetLeft(rectangle, left);
            Canvas.SetTop(rectangle, top);

            return rectangle;
        }

        private void ObjectLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (((UIElement)sender).Effect == null)
            {
                DeselectAllObjects();
                SelectObject(sender);
            }
        }

        private void ObjectRightDown(object sender, MouseButtonEventArgs e)
        {
            if (((UIElement)sender).Effect == null)
            {
                SelectObject(sender);
            }
            else
            {
                DeselectObject(sender);
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            List<UIElement> DeleteList = new List<UIElement>();
            foreach (var o in MyCanvas.Children)
            {
                if (((UIElement)o).Effect != null)
                {
                    DeleteList.Add((UIElement)o);
                }
            }
            foreach (var o in DeleteList)
            {
                MyCanvas.Children.Remove(o);
            }
        }

        private void RandomColorsButtonClick(object sender, RoutedEventArgs e)
        {
            SolidColorBrush solidColorBrush;
            foreach (var o in MyCanvas.Children)
            {
                if (((UIElement)o).Effect != null)
                {
                    solidColorBrush = new SolidColorBrush(Color.FromArgb(255,
                                                                             (byte)Random.Next(0, 255),
                                                                             (byte)Random.Next(0, 255),
                                                                             (byte)Random.Next(0, 255)));
                    ((Shape)o).Fill = solidColorBrush;
                }
            }
        }

        private void ShapeButtonClick(object sender, RoutedEventArgs e)
        {
            if (ManualDraw.DrawingMode == 3)
            {
                if (sender == RectangleButton)
                {
                    ManualDraw.DrawingMode = 1;
                    MyCanvas.Cursor = Cursors.Cross;
                }
                if (sender == EllipseButton)
                {
                    ManualDraw.DrawingMode = 2;
                    MyCanvas.Cursor = Cursors.Cross;
                }
            }
        }

        private void MyCanvasLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (ManualDraw.DrawingMode == 1)
            {
                ManualDraw.StartPoint = e.GetPosition(MyCanvas);
                ManualDraw.Rectangle = DrawRectangle(e.GetPosition(MyCanvas).X,
                                                     e.GetPosition(MyCanvas).Y,
                                                     0,
                                                     0,
                                                     (byte)Random.Next(0, 255),
                                                     (byte)Random.Next(0, 255),
                                                     (byte)Random.Next(0, 255));
                ManualDraw.Rectangle.Cursor = null;
                Mouse.Capture(MyCanvas);
            }
            if (ManualDraw.DrawingMode == 2)
            {
                ManualDraw.StartPoint = e.GetPosition(MyCanvas);
                ManualDraw.Ellipse = DrawEllipse(e.GetPosition(MyCanvas).X,
                                                 e.GetPosition(MyCanvas).Y,
                                                 0,
                                                 0,
                                                 (byte)Random.Next(0, 255),
                                                 (byte)Random.Next(0, 255),
                                                 (byte)Random.Next(0, 255));
                ManualDraw.Ellipse.Cursor = null;
                Mouse.Capture(MyCanvas);
            }
            if (ManualDraw.DrawingMode == 3)
            {
                foreach (UIElement o in MyCanvas.Children)
                {
                    if (o.IsMouseOver == true)
                    {
                        return;
                    }
                }
                DeselectAllObjects();
            }
        }

        private void MyCanvasMouseMove(object sender, MouseEventArgs e)
        {
            ManualDraw.MouseMove(MyCanvas, e);
        }

        private void MyCanvasLeftUp(object sender, MouseButtonEventArgs e)
        {
            if (ManualDraw.DrawingMode != 3)
            {
                ManualDraw.Rectangle.Cursor = Cursors.Hand;
                ManualDraw.Ellipse.Cursor = Cursors.Hand;
                ManualDraw = new ManualDraw();
                MyCanvas.Cursor = Cursors.Arrow;
                Mouse.Capture(null);
            }
        }

        private void SelectObject(object sender)
        {
            ((UIElement)sender).Effect = new DropShadowEffect
            {
                Color = new Color { A = 255, R = 255, G = 255, B = 255 },
                Direction = 270,
                ShadowDepth = 0,
                BlurRadius = 50
            };
            Canvas.SetZIndex((UIElement)sender, 1);
            this.DataContext = (UIElement)sender;
        }

        private void DeselectObject(object sender)
        {
            ((UIElement)sender).Effect = null;
            Canvas.SetZIndex((UIElement)sender, 0);
        }

        private void DeselectAllObjects()
        {
            foreach (var o in MyCanvas.Children)
            {
                DeselectObject(o);
            }
        }

        private void LanguageButtonClick(object sender, RoutedEventArgs e)
        {
            if (LanguageMode == 1)
            {
                TextTranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("pl");
                ImageTranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("pl");
                LanguageMode = 2;
                return;
            }
            if (LanguageMode == 2)
            {
                TextTranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en");
                ImageTranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en");
                LanguageMode = 1;
                return;
            }
        }

        private void ExportButtonClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "savedimage"; // Default file name
            saveFileDialog.Filter = "PNG File|*.png";
            Nullable<bool> result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                Rect bounds = VisualTreeHelper.GetDescendantBounds(MyCanvas);
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)bounds.Width,
                                                                               (int)bounds.Height,
                                                                               96D,
                                                                               96D,
                                                                               PixelFormats.Default);
                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    VisualBrush visualBrush = new VisualBrush(MyCanvas);
                    drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(), bounds.Size));
                }
                renderTargetBitmap.Render(drawingVisual);


                CroppedBitmap croppedBitmap = new CroppedBitmap(renderTargetBitmap,
                                                                new Int32Rect((int)bounds.X,
                                                                              (int)bounds.Y,
                                                                              (int)MyCanvas.ActualWidth,
                                                                              (int)MyCanvas.ActualHeight));


                PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
                pngBitmapEncoder.Frames.Add(BitmapFrame.Create(croppedBitmap));
                string filename = saveFileDialog.FileName;
                using (var file = File.OpenWrite(filename))
                {
                    pngBitmapEncoder.Save(file);
                }
            }
        }
    }
}