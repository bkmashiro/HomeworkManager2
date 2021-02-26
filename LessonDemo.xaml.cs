using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HomeworkManager
{
    /// <summary>
    /// LessonDemo.xaml 的交互逻辑
    /// </summary>
    public partial class LessonDemo : Page
    {
        public LessonDemo(Frame f, WrapPanel panel, pv.Task t)
        {
            InitializeComponent();
            frame = f;
            panel1 = panel;
            tt = t;
            lesson_name.Visibility = Visibility.Hidden;
            text.Visibility = Visibility.Hidden;
            Lesson_Clear.Visibility = Visibility.Hidden;
            Lesson_Settings.Visibility = Visibility.Hidden;
            slider.Visibility = Visibility.Hidden;

            time.Text = $"{(tt.EndTime.Year > 2050 ? "无限制时间" : $"{GetSection(tt.EndTime)} {tt.EndTime.ToShortTimeString()}")}    ";
           // time.Text = ( : Environment.NewLine + tt.EndTime.ToLongDateString() + tt.EndTime.ToLongTimeString());
            lesson_name.Text = "  " + t.name;
            text.AppendText(((tt.Appendix == string.Empty ? string.Empty : tt.Appendix) + Environment.NewLine + tt.Content).Trim());

        }
        pv.Task tt = new pv.Task();
        Frame frame = null;
        WrapPanel panel1 = null;
        private string GetSection(DateTime dt)
        {
            Console.WriteLine(dt.ToShortTimeString());
            switch (dt.ToShortTimeString())
            {
                case "19:20": return "晚一";
                case "20:20": return "晚二";
                case "21:20": return "晚三";
                case "23:59": return "今日";
                default: return string.Empty;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            panel1.Children.Remove(frame);
        }
        /// <summary>
        /// True显示按钮，False隐藏按钮.
        /// </summary>
        /// <param name="bo"></param>
        private void TogglleButtoms(bool bo)
        {
            if (!IsAnimationFinished && !BlockedAni)
            {
                //阻止并等待完成
                BlockedAni = true;
                return;
            }
            else if (!IsAnimationFinished)
            {
                return;
            }

            if (bo)
            {
                IsAnimationFinished = false;
                Animation.OpacityAnimation(Lesson_Clear, 100, 0, TimeSpan.FromSeconds(2.2), null);
                Animation.OpacityAnimation(Lesson_Settings, 100, 0, TimeSpan.FromSeconds(2.5), null);
                Animation.OpacityAnimation(slider, 100, 0, TimeSpan.FromSeconds(2), null);
                Animation.XPositionAnimation(time, 300, 0, TimeSpan.FromSeconds(4), Animationfinished);

            }
            else
            {

                IsAnimationFinished = false;
                Animation.OpacityAnimation(Lesson_Clear, 0, 100, TimeSpan.FromSeconds(3), null);
                Animation.OpacityAnimation(Lesson_Settings, 0, 100, TimeSpan.FromSeconds(3.3), Animationfinished);
                Animation.OpacityAnimation(slider, 0, 100, TimeSpan.FromSeconds(2.7), null);
                Animation.XPositionAnimation(time, 0, 300, TimeSpan.FromSeconds(0.5), null);

            }
            bo = !bo;
            BlockedAni = false;

        }
        bool BlockedAni = false;
        bool IsAnimationFinished = true;
        private void Animationfinished(object sender, EventArgs e)
        {
            IsAnimationFinished = true;
            if (BlockedAni)
            {
                TogglleButtoms(true);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Animation.OpacityAnimation(lesson_name, 0, 100, TimeSpan.FromSeconds(2), null);
            Animation.XPositionAnimation(lesson_name, 100, 0, TimeSpan.FromSeconds(2), ShowHomeworkInfo);
            UpdateHeight();

            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Interval = 5000;
            timer.Elapsed += new ElapsedEventHandler((_sender, eventArgs) =>
            {
                try
                {
                    if (ExecDateDiff(DateTime.Now, tt.EndTime).TotalMinutes < 5)
                    {
                        this.lesson_name.Dispatcher.Invoke(new Action(delegate
                        {
                            RotateTransform rtf = new RotateTransform();

                            lesson_name.RenderTransform = rtf;
                            DoubleAnimationUsingKeyFrames dbAscending = new DoubleAnimationUsingKeyFrames();
                            var keyFrames = dbAscending.KeyFrames;
                            #region KeyFrames
                            keyFrames.Add(new SplineDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
                            keyFrames.Add(new SplineDoubleKeyFrame(7, TimeSpan.FromSeconds(0.1)));
                            keyFrames.Add(new SplineDoubleKeyFrame(-7, TimeSpan.FromSeconds(0.2)));
                            keyFrames.Add(new SplineDoubleKeyFrame(5, TimeSpan.FromSeconds(0.25)));
                            keyFrames.Add(new SplineDoubleKeyFrame(-5, TimeSpan.FromSeconds(0.30)));
                            keyFrames.Add(new SplineDoubleKeyFrame(3, TimeSpan.FromSeconds(0.33)));
                            keyFrames.Add(new SplineDoubleKeyFrame(-3, TimeSpan.FromSeconds(0.36)));
                            keyFrames.Add(new SplineDoubleKeyFrame(2, TimeSpan.FromSeconds(0.39)));
                            keyFrames.Add(new SplineDoubleKeyFrame(-2, TimeSpan.FromSeconds(0.42)));
                            keyFrames.Add(new SplineDoubleKeyFrame(1, TimeSpan.FromSeconds(0.45)));
                            keyFrames.Add(new SplineDoubleKeyFrame(-1, TimeSpan.FromSeconds(0.48)));
                            keyFrames.Add(new SplineDoubleKeyFrame(0, TimeSpan.FromSeconds(0.5)));

                            keyFrames.Add(new SplineDoubleKeyFrame(0, TimeSpan.FromSeconds(3)));
                            #endregion
                            dbAscending.RepeatBehavior = RepeatBehavior.Forever;

                            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);


                            Task task = new Task(() =>
                            {
                                System.Threading.Thread.Sleep(1000 * 60 * 2);
                                this.lesson_name.Dispatcher.Invoke(new Action(delegate
                                {
                                    lesson_name.Text = "该任务将于30秒后删除";
                                }));
                                System.Threading.Thread.Sleep(1000 * 30);
                                this.panel1.Dispatcher.Invoke(new Action(delegate
                                {
                                    panel1.Children.Remove(frame);
                                }));

                            });
                            //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
                            task.Start();


                            //var alphaAnimation = new DoubleAnimationUsingKeyFrames();
                            //var keyFrames = alphaAnimation.KeyFrames;
                            //keyFrames.Add(new SplineDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
                            //keyFrames.Add(new SplineDoubleKeyFrame(1, TimeSpan.FromSeconds(4)));
                            //keyFrames.Add(new SplineDoubleKeyFrame(0, TimeSpan.FromSeconds(8)));
                            //alphaAnimation.RepeatBehavior = RepeatBehavior.Forever;
                            //stkpn.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            //stkpn.BeginAnimation(OpacityProperty, alphaAnimation);

                        }));
                        this.lesson_name.Dispatcher.Invoke(new Action(delegate
                        {
                            lesson_name.Text = lesson_name.Text + " 将于5分钟内过期";
                            lesson_name.Background = Brushes.AliceBlue;
                        }));
                        this.text.Dispatcher.Invoke(new Action(delegate
                        {
                            Random random = new Random();
                            if (random.Next(1, 100) < 3)
                            {
                                text.AppendText(Environment.NewLine + "触发彩蛋：小老弟怎么还没写完作业？中奖几率2%");
                            }
                        }));
                        timer.Stop();
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            });
            timer.Start();
        }

        private void ShowHomeworkInfo(object sender, EventArgs e)
        {
            Animation.OpacityAnimation(text, 0, 100, TimeSpan.FromSeconds(2), null);

            Animation.XPositionAnimation(text, 100, 0, TimeSpan.FromSeconds(2), null);
        }

        private void info_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateHeight();
        }

        public static TimeSpan ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan ts1 = new TimeSpan(dateBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dateEnd.Ticks);
            TimeSpan ts3 = ts2.Subtract(ts1);
            return ts3;
        }

        private void Page_MouseEnter(object sender, MouseEventArgs e)
        {


        }

        private void Page_MouseLeave(object sender, MouseEventArgs e)
        {


        }

        private void UpdateHeight()
        {

            double b = text.ExtentHeight + lesson_name.ActualHeight + 20;
            if (b > 30)
            {
                page.Height = b;
            }
            //int newlinecnt = info.li - 1;
            //stkpn.Height = skheight + 85 * newlinecnt;
            //this.Height = thisheight + 85 * newlinecnt;
            //this.tasks.Height = tasksheight + 85 * newlinecnt;
        }

        private void Lesson_Settings_Click(object sender, RoutedEventArgs e)
        {
            dialoguehost.IsOpen = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Timepicker.SelectedTime != null)
                {
                    tt.EndTime = (DateTime)Timepicker.SelectedTime;

                }
                dialoguehost.IsOpen = false;

                text.Document.Blocks.Clear();
                text.AppendText(((tt.EndTime.Year > 2050 ? "无限制时间" : Environment.NewLine + tt.EndTime.ToLongDateString() + tt.EndTime.ToLongTimeString()) + (Environment.NewLine + tt.Appendix == string.Empty ? string.Empty : tt.Appendix) + Environment.NewLine + tt.Content).Trim());

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Console.WriteLine(e.NewValue);
            try
            {
                if (this.IsLoaded)
                {
                    this.text.FontSize = 50 * (e.NewValue);
                    UpdateHeight();
                }
            }
            catch (Exception)
            {
            }
        }

        private void text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Page_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TogglleButtoms(false);
        }

        private void Page_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TogglleButtoms(true);
        }

        private void page_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
