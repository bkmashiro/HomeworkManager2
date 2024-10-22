﻿using System;
using System.Windows;
using System.Windows.Input;

namespace HomeworkManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void NavBar_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        object cont_h = null;
        private void Homework_Click(object sender, RoutedEventArgs e)
        {
            //NavigationWindow window = new NavigationWindow();
            if (cont_h == null)
            {
                homework h = new homework();
                MainPage.Content = h;
                cont_h = h;
            }
            else
            {
                MainPage.Content = cont_h;
            }


        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        bool MaxOrMin = false;
        private void Button_Maxium(object sender, RoutedEventArgs e)
        {
            if (MaxOrMin)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
            MaxOrMin = !MaxOrMin;
        }

        private void Button_Minium(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void NavBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.bakamashiro.xyz");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("更多功能添加中！");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //MainPage.Content = new help();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("更多功能添加中！");

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            homework h = new homework();
            MainPage.Content = h;
            cont_h = h;
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            Animation.RotateAnimation(vb_flip, 0, -180, TimeSpan.FromMilliseconds(500), null);
        }
        bool flipped = false;

        private void flip_Click(object sender, RoutedEventArgs e)
        {
            DoFlip();
            IsDragMode = false;

        }

        private void DoFlip()
        {
            flip.IsEnabled = false;
            if (flipped)
            {
                Animation.YPositionAnimation(dk, 600, 0, TimeSpan.FromMilliseconds(500), null);
                //Animation.YPositionAnimation(MainPage, 600, 0, TimeSpan.FromMilliseconds(500), null);
                Animation.RotateAnimation(vb_flip, 0, -180, TimeSpan.FromMilliseconds(500), comp1);

            }
            else
            {
                Animation.YPositionAnimation(dk, 0, 600, TimeSpan.FromMilliseconds(500), null);
                //Animation.YPositionAnimation(MainPage, 0, 600, TimeSpan.FromMilliseconds(500), null);
                Animation.RotateAnimation(vb_flip, -180, 0, TimeSpan.FromMilliseconds(500), comp1);
            }
            flipped = !flipped;
        }

        private void DoFlip(bool b)
        {
            if (b)
            {
                if (!flipped)
                {
                    Animation.YPositionAnimation(dk, 600, 0, TimeSpan.FromMilliseconds(500), null);
                    //Animation.YPositionAnimation(MainPage, 600, 0, TimeSpan.FromMilliseconds(500), null);
                    Animation.RotateAnimation(vb_flip, 0, -180, TimeSpan.FromMilliseconds(500), comp1);
                    flipped = !flipped;
                }

            }
            else
            {
                if (flipped)
                {
                    Animation.YPositionAnimation(dk, 0, 600, TimeSpan.FromMilliseconds(500), null);
                    //Animation.YPositionAnimation(MainPage, 0, 600, TimeSpan.FromMilliseconds(500), null);
                    Animation.RotateAnimation(vb_flip, -180, 0, TimeSpan.FromMilliseconds(500), comp1);
                    flipped = !flipped;
                }
            }
        }

        private void comp1(object sender, EventArgs e)
        {
            flip.IsEnabled = true;
            IsLeftMouseDown = false;
        }


        private void MainPage_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void MainPage_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            IsLeftMouseDown = false;

        }
        bool IsLeftMouseDown = false;
        double MouseY = 0;
        bool IsDragMode = false;
        private void MainPage_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MainPage_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void MainPage_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            IsLeftMouseDown = true;
            IsDragMode = true;
            MouseY = Mouse.GetPosition(e.Source as FrameworkElement).Y;
        }

        private void MainPage_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragMode)
            {

                if (IsLeftMouseDown && (MouseY - Mouse.GetPosition(e.Source as FrameworkElement).Y) > 50)
                {
                    DoFlip(true);
                    IsLeftMouseDown = false;


                }
                else if (IsLeftMouseDown && (MouseY - Mouse.GetPosition(e.Source as FrameworkElement).Y) < -50)
                {
                    DoFlip(false);
                    IsLeftMouseDown = false;
                }
            }

        }

        private void MainPage_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            IsLeftMouseDown = false;

        }
    }
}
