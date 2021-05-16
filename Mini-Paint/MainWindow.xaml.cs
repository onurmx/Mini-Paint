﻿using System;
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
            for (int i = 0; i < n; i++)
            {
                if (Random.Next(0, 2) < 1)
                {
                    int sizeX = Random.Next(100, 200);
                    int sizeY = Random.Next(100, 200);
                    DrawEllipse((double)Random.Next(sizeX, (int)MyCanvas.ActualWidth) - sizeX,
                                (double)Random.Next(sizeY, (int)MyCanvas.ActualHeight) - sizeY,
                                sizeX,
                                sizeY,
                                (byte)Random.Next(0, 255),
                                (byte)Random.Next(0, 255),
                                (byte)Random.Next(0, 255));
                }
                else
                {
                    int sizeX = Random.Next(100, 200);
                    int sizeY = Random.Next(100, 200);
                    DrawRectangle((double)Random.Next(sizeX, (int)MyCanvas.ActualWidth) - sizeX,
                                  (double)Random.Next(sizeY, (int)MyCanvas.ActualHeight) - sizeY,
                                  sizeX,
                                  sizeY,
                                  (byte)Random.Next(0, 255),
                                  (byte)Random.Next(0, 255),
                                  (byte)Random.Next(0, 255));
                }
            }
        }

        private Ellipse DrawEllipse(double left, double top, int width, int height, byte red, byte green, byte blue)
        {
            Ellipse ellipse = new Ellipse();
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = Color.FromArgb(255, red, green, blue);

            ellipse.Fill = solidColorBrush;
            ellipse.Width = width;
            ellipse.Height = height;
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
            solidColorBrush.Color = Color.FromArgb(255, red, green, blue);

            rectangle.Fill = solidColorBrush;
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
                return;
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
                return;
            }
            DeselectAllObjects();
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

            // Show save file dialog box
            Nullable<bool> result = saveFileDialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = saveFileDialog.FileName;
                //render ink to bitmap
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)MyCanvas.ActualWidth,
                                                                (int)MyCanvas.ActualHeight,
                                                                96d,
                                                                96d, PixelFormats.Default);
                rtb.Render(MyCanvas);

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(fs);
                }
            }
        }
    }
}
