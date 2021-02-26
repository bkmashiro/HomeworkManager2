using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HomeworkManager
{
    /// <summary>
    /// HandWrite.xaml 的交互逻辑
    /// </summary>
    public partial class HandWrite : Page
    {
        WrapPanel wrapPanel = null;
        Frame fr = null;
        public HandWrite(WrapPanel wrap, Frame f)
        {
            InitializeComponent();
            wrapPanel = wrap;
            fr = f;
        }

        Point startPoint;
        /// <summary>
        /// 点集合
        /// </summary>
        List<Point> pointList = new List<Point>();

        private void canv_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(canv);
        }

        private void canv_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // 返回指针相对于Canvas的位置
                Point point = e.GetPosition(canv);

                if (pointList.Count == 0)
                {
                    // 加入起始点
                    pointList.Add(new Point(this.startPoint.X, this.startPoint.Y));
                }
                else
                {
                    // 加入移动过程中的point
                    pointList.Add(point);
                }

                // 去重复点
                var disList = pointList.Distinct().ToList();
                var count = disList.Count(); // 总点数
                if (point != this.startPoint && this.startPoint != null)
                {
                    var l = new Line();
                    l.Stroke = Brushes.Black;

                    l.StrokeThickness = 1;
                    if (count < 2)
                        return;
                    l.X1 = disList[count - 2].X;  // count-2  保证 line的起始点为点集合中的倒数第二个点。
                    l.Y1 = disList[count - 2].Y;
                    // 终点X,Y 为当前point的X,Y
                    l.X2 = point.X;
                    l.Y2 = point.Y;
                    canv.Children.Add(l);
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            wrapPanel.Children.Remove(fr);
            Console.WriteLine();
        }


        private void canv_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pointList.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            canv.Children.Clear();
            pointList.Clear();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(this.Height);
            Console.WriteLine(this.Width);
        }
    }
}
