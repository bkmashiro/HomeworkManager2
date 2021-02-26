using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Ink;
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

        public HandWrite(WrapPanel wrap, Frame f)
        {
            InitializeComponent();
            wrapPanel = wrap;
            fr = f;
            drawingAttributes = new DrawingAttributes();

            //将 InkCanvas 的 DefaultDrawingAttributes 属性的值赋成创建的 DrawingAttributes 类的对象的引用
            //InkCanvas 通过 DefaultDrawingAttributes 属性来获取墨迹的各种设置，该属性的类型为 DrawingAttributes 型
            canv.DefaultDrawingAttributes = drawingAttributes;
            drawingAttributes.FitToCurve = true;

            drawingAttributes.IgnorePressure = false;
            Holder.Visibility = Visibility.Collapsed;
        }
        WrapPanel wrapPanel = null;
        Frame fr = null;
        DrawingAttributes drawingAttributes;


        private void canv_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void canv_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Pressed)
            //{
            //    // 返回指针相对于Canvas的位置
            //    Point point = e.GetPosition(canv);

            //    if (pointList.Count == 0)
            //    {
            //        // 加入起始点
            //        pointList.Add(new Point(this.startPoint.X, this.startPoint.Y));
            //    }
            //    else
            //    {
            //        // 加入移动过程中的point
            //        pointList.Add(point);
            //    }

            //    // 去重复点
            //    var disList = pointList.Distinct().ToList();
            //    var count = disList.Count(); // 总点数
            //    if (point != this.startPoint && this.startPoint != null)
            //    {
            //        var l = new Line();
            //        l.Stroke = Brushes.Black;

            //        l.StrokeThickness = 1;
            //        if (count < 2)
            //            return;
            //        l.X1 = disList[count - 2].X;  // count-2  保证 line的起始点为点集合中的倒数第二个点。
            //        l.Y1 = disList[count - 2].Y;
            //        // 终点X,Y 为当前point的X,Y
            //        l.X2 = point.X;
            //        l.Y2 = point.Y;
            //        canv.Children.Add(l);
            //    }
            //}

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void canv_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(this.Height);
            //Console.WriteLine(this.Width);


        }

        private void SendTips(string tip)
        {
            Snackbar.MessageQueue?.Clear();
            Snackbar.MessageQueue?.Enqueue($"{tip}", null, null, null, false, true, TimeSpan.FromSeconds(2));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SendTips("画笔");
            canv.EditingMode = InkCanvasEditingMode.Ink;
            //b1.IsEnabled = true;

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SendTips("橡皮（点擦除）");
            canv.EditingMode = InkCanvasEditingMode.EraseByPoint;


        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SendTips("橡皮（线擦除）");
            canv.EditingMode = InkCanvasEditingMode.EraseByStroke;


        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SendTips("选定");
            canv.EditingMode = InkCanvasEditingMode.Select;


        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            SendTips("锁定");
            canv.EditingMode = InkCanvasEditingMode.None;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            SendTips("关闭");
            wrapPanel.Children.Remove(fr);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            SendTips("颜色");

        }

        // Used for DialogHost.DialogClosingAttached
        private void Sample2_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
            => Debug.WriteLine($"SAMPLE 2: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult dr = colorDialog.ShowDialog();
            //如果选中颜色，单击“确定”按钮则改变文本框的文本颜色
            if (dr == DialogResult.OK)
            {
                PenColor= ToMediaColor(colorDialog.Color);
                pen_path.Fill = new SolidColorBrush(PenColor);
                drawingAttributes.Color = PenColor;
            }
        }
        System.Windows.Media.Color ToMediaColor(System.Drawing.Color drawingColor)
        {
            return System.Windows.Media.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
        }

        Color PenColor = Colors.Black;
        private void ColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            pen_path.Fill =new SolidColorBrush(e.NewValue);
        }

        private void Page_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Animation.OpacityAnimation(Holder, 0, 100, TimeSpan.FromSeconds(0.8), null);

        }

        private void Page_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Animation.OpacityAnimation(Holder, 100, 0, TimeSpan.FromSeconds(3.2), null);
        }
    }
}
