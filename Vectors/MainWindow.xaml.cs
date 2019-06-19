using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Vectors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<LinePoint> PointList = new List<LinePoint>() { };
        int PointListCount;

        public MainWindow()
        {
            InitializeComponent();
            PointListCount = PointList.Count;
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            HorLine.Y1 = HorLine.Y2 = e.GetPosition(MainCanvas).Y;

            VerLine.X1 = VerLine.X2 = e.GetPosition(MainCanvas).X;
            HorLine.X1 = 0;
            HorLine.X2 = MainCanvas.ActualWidth;

            VerLine.Y1 = 0;
            VerLine.Y2 = MainCanvas.ActualHeight;

            PosTextLabelX.Content = "X :" + Math.Round(e.GetPosition(MainCanvas).X - HorMidLine.X1,2).ToString();
            PosTextLabelY.Content = "Y :" + Math.Round(VerMidLine.Y1 - e.GetPosition(MainCanvas).Y, 2).ToString();


        }

        private void MainCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateLines();
        }

        public void UpdateLines()
        {
            VerMidLine.Y1 = VerMidLine.Y2 = MainCanvas.ActualHeight * 0.5;
            VerMidLine.X1 = 0;
            VerMidLine.X2 = MainCanvas.ActualWidth;

            HorMidLine.X1 = HorMidLine.X2 = MainCanvas.ActualWidth * 0.5;
            HorMidLine.Y1 = 0;
            HorMidLine.Y2 = MainCanvas.ActualHeight;

            foreach (LinePoint point in PointList)
            {
                var var = MainCanvas.FindName(point.Name + "X");
                object LineX = MainCanvas.Children[(int)var];

                ((Line)LineX).Y2 = MainCanvas.ActualHeight;

                Line LineY = (Line)MainCanvas.Children[(int)MainCanvas.FindName(point.Name + "Y")];

                ((Line)LineY).X2 = MainCanvas.ActualWidth;
            }

            LineListsBox.Items.Clear();
            for (int i = 0; i < PointListCount; i++)
            {
                LineListsBox.Items.Add("Line " + i.ToString());
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateLines();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateLines();
            if (e.LeftButton == MouseButtonState.Pressed) {
                PointList.Add(new LinePoint(Mouse.GetPosition(MainCanvas),"Point_"+ PointListCount.ToString()));
                GeneratePointLines(PointList.Count-1);
                PointListCount++;
            }
        }

        public void GeneratePointLines(int PointListPos) {

            int lineXPos = MainCanvas.Children.Add(new Line()
            {
                Name = PointList[PointListPos].Name + "X",
                X1 = PointList[PointListPos].Pos.X,
                X2 = PointList[PointListPos].Pos.X,
                Y1 = 0,
                Y2 = MainCanvas.ActualHeight,
                Stroke = Brushes.Blue,
            });

            RegisterName(PointList[PointListPos].Name + "X", MainCanvas.Children[lineXPos]);


            int lineYPos = MainCanvas.Children.Add(new Line()
            {
                Name = PointList[PointListPos].Name + "Y",
                Y1 = PointList[PointListPos].Pos.Y,
                Y2 = PointList[PointListPos].Pos.Y,
                X1 = 0,
                X2 = MainCanvas.ActualWidth,
                Stroke = Brushes.Blue
            });
            RegisterName(PointList[PointListPos].Name + "Y", MainCanvas.Children[lineYPos]);
        }

        private void LineLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class LinePoint
    {
        public Point Pos { get; set; }
        public string Name { get; set; }
        public LinePoint(Point Pos, string Name)
        {
            this.Name = Name;
            this.Pos = Pos;
        }
    }
}
