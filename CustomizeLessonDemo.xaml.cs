﻿using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace HomeworkManager
{
    /// <summary>
    /// CustomizeLessonDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CustomizeLessonDemo : Page
    {
        public CustomizeLessonDemo(string _name, string content, DateTime endtime, WrapPanel wrapPanel, Frame frame)
        {
            InitializeComponent();
            edit.Visibility = Visibility.Collapsed;
            Hideit();
            parent = wrapPanel;
            frm = frame;
            homework_lesson.Text = _name;
            homework_content.Text = content;

            dt = endtime;
        }

        DateTime dt;
        public CustomizeLessonDemo(string _name, string content, DateTime endtime, WrapPanel wrapPanel, Frame frame, string image)
        {
            InitializeComponent();
            edit.Visibility = Visibility.Collapsed;
            Hideit();
            parent = wrapPanel;
            frm = frame;
            img.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
            homework_lesson.AppendText(_name);
            homework_content.AppendText(content);
            dt = endtime;
        }

        WrapPanel parent;
        Frame frm;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Hideit();
        }

        private void Hideit()
        {
            Animation.ScaleAnimation(settings_btns, 1, 0, TimeSpan.FromMilliseconds(300), null);
            Animation.ScaleAnimation(settings_holder, 1, 0, TimeSpan.FromMilliseconds(300), null);
            Animation.ScaleAnimation(settings_lab, 1, 0, TimeSpan.FromMilliseconds(300), null);
            Animation.OpacityAnimation(edit, 0, 1, TimeSpan.FromMilliseconds(300), null);
            settings_btns.Visibility = Visibility.Collapsed;
            settings_holder.Visibility = Visibility.Collapsed;
            settings_lab.Visibility = Visibility.Collapsed;
            edit.Visibility = Visibility.Visible;
            h1.Height = new GridLength(0, GridUnitType.Pixel);
            h2.Height = new GridLength(0, GridUnitType.Pixel);
            h3.Height = new GridLength(0, GridUnitType.Pixel);
            h4.Height = new GridLength(0, GridUnitType.Pixel);
            DoubleAnimation a = new DoubleAnimation();
            a.From = ca.ActualHeight;
            a.To = ca.ActualHeight - 100;
            a.Duration = TimeSpan.FromMilliseconds(400);
            a.FillBehavior = FillBehavior.Stop;
            ca.BeginAnimation(HeightProperty, a);

            ca.Height = double.NaN;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //homework_content.AppendText("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            Animation.ScaleAnimation(settings_btns, 0, 1, TimeSpan.FromMilliseconds(300), null);
            Animation.ScaleAnimation(settings_holder, 0, 1, TimeSpan.FromMilliseconds(300), null);
            Animation.ScaleAnimation(settings_lab, 0, 1, TimeSpan.FromMilliseconds(300), null);
            Animation.OpacityAnimation(edit, 1, 0, TimeSpan.FromMilliseconds(300), null);
            settings_btns.Visibility = Visibility.Visible;
            settings_holder.Visibility = Visibility.Visible;
            settings_lab.Visibility = Visibility.Visible;
            edit.Visibility = Visibility.Collapsed;
            h1.Height = GridLength.Auto;
            h2.Height = GridLength.Auto;
            h3.Height = GridLength.Auto;
            h4.Height = GridLength.Auto;
            DoubleAnimation a = new DoubleAnimation();
            a.From = ca.ActualHeight;
            a.To = ca.ActualHeight + 100;
            a.Duration = TimeSpan.FromMilliseconds(400);
            ca.BeginAnimation(HeightProperty, a);
            a.FillBehavior = FillBehavior.Stop;

            ca.Height = double.NaN;

        }

        private void homework_content_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            h1.Height = GridLength.Auto;
            h2.Height = GridLength.Auto;
            h3.Height = GridLength.Auto;
            h4.Height = GridLength.Auto;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            parent.Children.Remove(frm);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            infos.Text = $"科目{homework_lesson.Text}{Environment.NewLine}{homework_content.Text}{Environment.NewLine}即将结束";
            timer.Interval = 10000;
            timer.Elapsed += new ElapsedEventHandler((_sender, eventArgs) =>
            {

                if (ExecDateDiff(DateTime.Now, this.dt).Duration().TotalMinutes < 5)
                {
                    this.trans.Dispatcher.Invoke(new Action(delegate
                    {
                        trans.SelectedIndex = 1;
                        System.Timers.Timer timer2 = new System.Timers.Timer();
                        timer2.Interval = 1000;
                        TimeSpan timeSpan = DateTime.Now.Subtract(dt);


                        timer2.Elapsed += new ElapsedEventHandler((_sender2, eventArgs2) =>
                            {
                                if (timeSpan.TotalSeconds < 1)
                                {
                                    timer2.Stop();
                                    parent.Dispatcher.Invoke(new Action(delegate
                                    {
                                        parent.Children.Remove(frm);
                                    }));
                                }
                                timeSpan = timeSpan.Subtract(TimeSpan.FromSeconds(1));
                                this.cntdown.Dispatcher.Invoke(new Action(delegate
                                {
                                    cntdown.Text = timeSpan.Minutes.ToString().PadLeft(2,'0') + ":" + timeSpan.Seconds.ToString().PadLeft(2, '0');
                                }));
                                this.progress.Dispatcher.Invoke(new Action(delegate
                                {
                                    double s = timeSpan.TotalSeconds / 310;
                                    if (s>progress.Maximum)
                                    {
                                        s = progress.Maximum;
                                    }
                                    if (s < progress.Minimum    )
                                    {
                                        s = progress.Minimum;
                                    }
                                    progress.Value = s;
                                }));
                            });
                        timer2.Start();
                    }));
                    timer.Stop();
                }
            });
            timer.Start();
        }
        public static TimeSpan ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan ts1 = new TimeSpan(dateBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dateEnd.Ticks);
            TimeSpan ts3 = ts2.Subtract(ts1);
            return ts3;
        }
    }
}