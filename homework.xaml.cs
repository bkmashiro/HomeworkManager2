using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace HomeworkManager
{
    /// <summary>
    /// homework.xaml 的交互逻辑
    /// </summary>
    public partial class homework : Page
    {
        public homework()
        {
            InitializeComponent();
            plusbtnholder.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Frame frame = new Frame();
            //LessonDemo ls = new LessonDemo(frame, HomeWorkHolder);
            //ls.Margin = new Thickness(1, 1, 1, 1);
            //frame.Content = ls;
            //frame.Background = Brushes.Transparent;
            //frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            //Label l = new Label();
            //l.Content = "1232141";
            //HomeWorkHolder.Children.Add(frame);

            Animation.YPositionAnimation(addlesson, 0, 20, TimeSpan.FromMilliseconds(100), jump2);
            AddLesson ads = new AddLesson(GetPanel());
            ads.WindowState = WindowState.Normal;
            ads.Show();

        }

        private WrapPanel GetPanel()
        {
            double Len1 = 0;
            double Len2 = 0;
            foreach (Frame item in HomeWorkHolder1.Children)
            {
                Len1 += item.ActualHeight;
            }
            foreach (Frame item in HomeWorkHolder2.Children)
            {
                Len2 += item.ActualHeight;
            }
            if (Len1 > Len2)
            {
                return HomeWorkHolder2;
            }
            else
            {
                return HomeWorkHolder1;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            HomeWorkHolder1.Width = this.Width / 2;
            HomeWorkHolder2.Width = this.Width / 2;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Animation.ScaleAnimation(touchwrite, 1, 1.2, TimeSpan.FromMilliseconds(100), zoom2);
            WrapPanel wrap = GetPanel();
            Frame frame = new Frame();
            frame.Width = 960;
            HandWrite handWrite = new HandWrite(wrap, frame);
            frame.Content = handWrite;
            wrap.Children.Add(frame);
        }
        private void zoom2(object sender, EventArgs e)
        {
            Animation.ScaleAnimation(touchwrite, 1.2, 1, TimeSpan.FromMilliseconds(100), null);
        }
        private void jump2(object sender, EventArgs e)
        {
            Animation.YPositionAnimation(addlesson, 30, 0, TimeSpan.FromMilliseconds(100), null);
        }


        bool isFolding = true;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Flop();
            togglebtn.IsEnabled = false;

        }

        private void InvisibleFolding(object sender, EventArgs e)
        {
            plusbtnholder.Visibility = Visibility.Collapsed;
        }
        private void FoldingFinished(object sender, EventArgs e)
        {
            togglebtn.IsEnabled = true;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            WrapPanel wrap = GetPanel();
            Frame frame = new Frame();
            frame.Width = 960;
            Image image = new Image();
            string filepath = "";
            //string filename = "";
            OpenFileDialog openfilejpg = new OpenFileDialog();
            openfilejpg.Filter = "jpg图片(*.jpg)|*.jpg|gif图片(*.gif)|*.gif";
            openfilejpg.FilterIndex = 0;
            openfilejpg.RestoreDirectory = true;
            openfilejpg.Multiselect = false;
            if (openfilejpg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filepath = openfilejpg.FileName;
                    Image img = new Image();
                    BitmapImage bImg = new BitmapImage();
                    img.IsEnabled = true;
                    bImg.BeginInit();
                    bImg.UriSource = new Uri(filepath, UriKind.Relative);
                    bImg.EndInit();
                    image.Source = new BitmapImage(new Uri(filepath, UriKind.Absolute));
                    image.MouseDown += Image_MouseDown;
                    image.Tag = frame;
                    frame.Content = image;
                    frame.Tag = wrap;
                    wrap.Children.Add(frame);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    throw;
                }

            }
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (((Frame)((Image)sender).Tag).Tag as WrapPanel ).Children.Remove((Frame)((Image)sender).Tag);
        }


        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WrapPanel wrap = GetPanel();
            Frame frame = new Frame();
            frame.Width = 960;
            weather wea = new weather(wrap,frame);
            frame.Content = wea;
            wrap.Children.Add(frame);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            WrapPanel wrap = GetPanel();
            Frame frame = new Frame();
            frame.Width = 960;
            ringing ringing = new ringing();
            frame.Content = ringing;
            wrap.Children.Add(frame);
            
        }
        private void Flop()
        {
            Random random = new Random();

            if (isFolding)
            {
                plusbtnholder.Visibility = Visibility.Visible;
                Animation.YPositionAnimation(plusbtnholder, 0, -250, TimeSpan.FromMilliseconds(200), FoldingFinished);
                Animation.OpacityAnimation(plusbtnholder, 0, 1, TimeSpan.FromMilliseconds(200), null);
               // Animation.RotateAnimation(flopper, 0, -180, TimeSpan.FromMilliseconds(800), null);
                foreach (var item in plusbtnholder.Children)
                {
                    Animation.WRotateAnimation((UIElement)item, -random.Next(30, 50), 0, TimeSpan.FromMilliseconds(200), null);
                }
            }
            else
            {
                plusbtnholder.Visibility = Visibility.Visible;
                Animation.YPositionAnimation(plusbtnholder, -250, 0, TimeSpan.FromMilliseconds(200), FoldingFinished);
                Animation.OpacityAnimation(plusbtnholder, 1, 0, TimeSpan.FromMilliseconds(200), null);
                //Animation.RotateAnimation(flopper, 360, 0, TimeSpan.FromMilliseconds(800), null);

                foreach (var item in plusbtnholder.Children)
                {
                    Animation.WRotateAnimation((UIElement)item, 0, -random.Next(30, 50), TimeSpan.FromMilliseconds(200), InvisibleFolding);
                }
            }
            isFolding = !isFolding;
        }

        int WaitFlopTime = 400;
        private void Flop(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            togglebtn.IsChecked = !togglebtn.IsChecked;

            if (isFolding)
            {
                plusbtnholder.Visibility = Visibility.Visible;
                Animation.YPositionAnimation(plusbtnholder, 0, -250, TimeSpan.FromMilliseconds(WaitFlopTime), FoldingFinished);
                Animation.OpacityAnimation(plusbtnholder, 0, 1, TimeSpan.FromMilliseconds(WaitFlopTime), null);
                // Animation.RotateAnimation(flopper, 0, -180, TimeSpan.FromMilliseconds(800), null);
                foreach (var item in plusbtnholder.Children)
                {
                    Animation.WRotateAnimation((UIElement)item, -random.Next(30, 50), 0, TimeSpan.FromMilliseconds(WaitFlopTime), null);
                }
            }
            else
            {
                plusbtnholder.Visibility = Visibility.Visible;
                Animation.YPositionAnimation(plusbtnholder, -250, 0, TimeSpan.FromMilliseconds(WaitFlopTime), FoldingFinished);
                Animation.OpacityAnimation(plusbtnholder, 1, 0, TimeSpan.FromMilliseconds(WaitFlopTime), null);
                //Animation.RotateAnimation(flopper, 360, 0, TimeSpan.FromMilliseconds(800), null);

                foreach (var item in plusbtnholder.Children)
                {
                    Animation.WRotateAnimation((UIElement)item, 0, -random.Next(30, 50), TimeSpan.FromMilliseconds(WaitFlopTime), InvisibleFolding);
                }
            }
            isFolding = !isFolding;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in plusbtnholder.Children)
            {
                if (item.GetType()==typeof(System.Windows.Controls.Button))
                {
                    (item as System.Windows.Controls.Button).Click += Flop;

                }
            }
            //Flop();

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

        }

        private void addlesson_Copy_Click(object sender, RoutedEventArgs e)
        {
            AdvancedAddlesson ads = new AdvancedAddlesson(GetPanel());
            ads.WindowState = WindowState.Normal;
            ads.Show();
        }
    }
}
