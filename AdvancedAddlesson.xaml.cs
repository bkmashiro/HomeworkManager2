using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace HomeworkManager
{
    /// <summary>
    /// AdvancedAddlesson.xaml 的交互逻辑
    /// </summary>
    public partial class AdvancedAddlesson : Window
    {
        public AdvancedAddlesson(WrapPanel wp)
        {
            InitializeComponent();
            WrapPanel = wp;
        }

        WrapPanel WrapPanel;

        private void candi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (candi.SelectedItem != null)
            {
                homeworklist1.AppendText(Environment.NewLine + (string)candi.SelectedItem);
            }
        }

        private void Subjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            candi.Items.Clear();
            List<string> ccc = new List<string>();
            for (int i = 0; i < pv.candiate_lesson.Count; i++)
            {
                if (pv.candiate_lesson[i] == (string)Subjects.SelectedItem)
                {
                    foreach (var item in pv.candiate[i])
                    {
                        if (item != string.Empty)
                        {
                            ccc.Add(item);
                        }
                    }
                    break;
                }
            }
            ccc.Reverse();
            foreach (var item in ccc)
            {
                candi.Items.Add(item);
            }
        }

        private void SaveXML()
        {
            XDocument xDoc = new XDocument();
            XDeclaration xDec = new XDeclaration("1.0", "utf-8", null);
            // 设置文档定义
            xDoc.Declaration = xDec;
            //2、创建根节点
            XElement rootElement = new XElement("Yuzhe");
            xDoc.Add(rootElement);
            //3、创建主要节点
            XElement ProgrammeSettings = new XElement("ProgrammeSettings");
            ProgrammeSettings.SetElementValue("Author", "YuzheShi and Wushishi @ bakamashiro.com");
            XElement UserData = getUserData();

            rootElement.Add(ProgrammeSettings);
            rootElement.Add(UserData);
            xDoc.Save(System.Windows.Forms.Application.StartupPath + "\\" + "Settings.xml");
            Console.WriteLine("ok");
        }

        private XElement getUserData()
        {
            XElement ele = new XElement("UsersData");
            XElement tmp = new XElement("Subjects");
            XElement tmp2 = new XElement("word");
            for (int i = 0; i < pv.candiate.Count; i++)
            {
                List<string> subj = pv.candiate[i];
                tmp = new XElement("Subjects");
                tmp.SetAttributeValue("lesson", pv.candiate_lesson[i]);
                foreach (var word in subj)
                {
                    tmp2.Value = (word);
                    tmp.Add(tmp2);
                    tmp2 = new XElement("word");
                }
                ele.Add(tmp);
            }
            return ele;
        }

        string img = "";
        private void RegistNewHmwk()
        {
            if (homeworklist1.Text == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("不要输入空的作业！");
                return;
            }
            //Animation.OpacityAnimation(gg, 1, 0, TimeSpan.FromMilliseconds(200), DimFinished);
            Frame frame = new Frame();
            CustomizeLessonDemo advancedAddlesson;
            if (img == string.Empty)
            {
                advancedAddlesson = new CustomizeLessonDemo((string)Subjects.SelectedItem, homeworklist1.Text, _endtime, WrapPanel, frame);
            }
            else
            {
                advancedAddlesson = new CustomizeLessonDemo((string)Subjects.SelectedItem, homeworklist1.Text, _endtime, WrapPanel, frame, img);
            }
            frame.Content = advancedAddlesson;
            frame.Background = Brushes.Transparent;


            WrapPanel.Children.Add(GetCustomizedHmwk());



            SaveXML();
            this.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pv.ReadXML();
            string[] subjs = { "语文", "数学", "英语", "物理", "化学", "技术", "政治", "历史", "地理", "生物", "最高指示", "其他" };
            Subjects.ItemsSource = subjs;
            Subjects.SelectedIndex = 0;

            //Animation.OpacityAndPositionAnimation(hint, 100, 0, -30, 0, TimeSpan.FromSeconds(2), finished);

            //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
            //task.Start();
            homeworklist1.Focus();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            UpdateAllCand();
            RegistNewHmwk();
        }
        private void UpdateAllCand()
        {
            string[] s = homeworklist1.Text.Split('\r');
            foreach (var item in s)
            {
                if (item != string.Empty)
                {
                    UpdateCand(item.Trim());
                }
            }
        }
        private void UpdateCand(string s)
        {
            bool found = false;
            for (int i = 0; i < pv.candiate_lesson.Count; i++)
            {
                if (pv.candiate_lesson[i] == (string)Subjects.SelectedItem)
                {
                    if (!pv.candiate[i].Contains(s))
                    {
                        pv.candiate[i].Add(s);
                        found = true;
                    }
                    break;
                }
            }

        }

        DateTime _endtime;
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (times.SelectedIndex)
            {
                case -1:
                    _endtime = DateTime.Parse("19:20:00");
                    break;
                case 0:
                    _endtime = DateTime.Parse("19:20:00");
                    break;
                case 1:
                    _endtime = DateTime.Parse("20:20:00");
                    break;
                case 2:
                    _endtime = DateTime.Parse("21:20:00");
                    break;
                case 3:
                    _endtime = DateTime.Parse("23:59:00");
                    break;
                case 4:
                    _endtime = DateTime.Now.AddYears(50);
                    break;
                default:
                    _endtime = DateTime.Parse("21:20:00");
                    break;
            }

        }

        private void RadioButtonGroupChoiceChipPrimaryOutline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (RadioButtonGroupChoiceChipPrimaryOutline.SelectedIndex)
            {
                case -1: break;
                case 0://经典
                    SettingsSelected = 1;
                    break;
                case 1:
                    //二次元
                    SettingsSelected = 2;

                    break;
                case 2:
                    //卡片
                    SettingsSelected = 3;

                    break;
                case 3:
                    //自定义
                    SettingsSelected = 4;

                    break;

                default:
                    break;
            }
            trans.SelectedIndex = 1;
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                img = openFileDialog.FileName;
                //media.Source = new Uri(img);
                MediaElement mediaElement = new MediaElement();
                mediaElement.Source = new Uri(img);
                mediaElement.MouseLeftButtonDown += MediaElement_MouseLeftButtonDown;
                mediaElement.MouseRightButtonDown += MediaElement_MouseRightButtonDown;
                mediaElement.HorizontalAlignment = HorizontalAlignment.Left;
                mediaElement.VerticalAlignment = VerticalAlignment.Top;
                mediaElement.MaxWidth = 1920 / 2;
                mediaElement.MinWidth = 100;
                AddToPreview(mediaElement);
            }
        }

        private void MediaElement_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as MediaElement).CanPause)
            {
                (sender as MediaElement).LoadedBehavior = MediaState.Manual;

                (sender as MediaElement).Play();
            }
        }

        private void MediaElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as MediaElement).CanPause)
            {
                (sender as MediaElement).LoadedBehavior = MediaState.Manual;

                (sender as MediaElement).Pause();
            }
        }

        UIElement currentElement = null;//当前选定的element


        private void EnableDrag(UIElement uiEle)
        {
            if (uiEle != null)
            {

                uiEle.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(Element_MouseLeftButtonDown), true);
                uiEle.AddHandler(MouseMoveEvent, new MouseEventHandler(Element_MouseMove), true);
                uiEle.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(Element_MouseLeftButtonUp), true);

                uiEle.MouseMove += new MouseEventHandler(Element_MouseMove);
                uiEle.MouseLeftButtonDown += new MouseButtonEventHandler(Element_MouseLeftButtonDown);
                uiEle.MouseLeftButtonUp += new MouseButtonEventHandler(Element_MouseLeftButtonUp);
            }
        }
        bool isDragDropInEffect = false;
        Point pos = new Point();

        void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement currEle = sender as FrameworkElement;
                double xPos = e.GetPosition(null).X - pos.X + currEle.Margin.Left;
                double yPos = e.GetPosition(null).Y - pos.Y + currEle.Margin.Top;
                currEle.Margin = new Thickness(xPos, yPos, 0, 0);
                pos = e.GetPosition(null);
            }
        }
        void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement fEle = sender as FrameworkElement;
            currentElement = fEle;
            elementInfo.Text = $"{fEle.GetType()}高{fEle.ActualHeight}宽{fEle.ActualWidth}位于Left:{fEle.Margin.Left}Top:{fEle.Margin.Top}";
            isDragDropInEffect = true;
            pos = e.GetPosition(null);
            fEle.CaptureMouse();
            fEle.Cursor = System.Windows.Input.Cursors.Hand;
        }
        void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement ele = sender as FrameworkElement;
                elementInfo.Text = $"{ele.GetType()}高{ele.ActualHeight}宽{ele.ActualWidth}位于Left:{ele.Margin.Left}Top:{ele.Margin.Top}";
                isDragDropInEffect = false;
                ele.ReleaseMouseCapture();
            }
        }

        private void AddToPreview(UIElement uIElement)
        {
            EnableDrag(uIElement);
            preview.Children.Add(uIElement);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var textBlock = new Emoji.Wpf.RichTextBox();
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
            textBlock.VerticalAlignment = VerticalAlignment.Top;
            textBlock.Width = double.NaN;
            textBlock.Height = double.NaN;
            textBlock.FontSize = 40;
            textBlock.MaxWidth = 1920 / 2;
            textBlock.MaxHeight = 1080;
            textBlock.Text = "点击来修改";
            textBlock.Background = Brushes.Transparent;
            textBlock.BorderBrush = Brushes.Transparent;



            AddToPreview(textBlock);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private Frame GetCustomizedHmwk()
        {
            Frame frame = new Frame();
            frame.Background = Brushes.Transparent;
            var tmp = this.prvh.Children[0];
            this.prvh.Children.Remove(this.prvh.Children[0]);
            if (tmp is Grid)
            {
                (tmp as Grid).Margin = new Thickness(0);

            }
            if (tmp is Page)
            {
                (tmp as Page).Margin = new Thickness(0);

            }
            frame.Content = tmp;
            return frame;
        }

        int SettingsSelected = 0;
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            trans.SelectedIndex = 0;
            //queren
            switch (SettingsSelected)
            {
                case 0:
                    break;
                case 1:
                    //classic

                    break;
                case 2:
                    //nijien
                    prvh.Children.Clear();
                    OnErciyuanTriggered();
                    break;
                case 3:
                    //card
                    break;
                case 4:
                    //customize
                    break;
                default:
                    break;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            trans.SelectedIndex = 0;
        }

        private void OnClassicTriggered()
        {

        }
        private void OnErciyuanTriggered()
        {
            Frame frame = new Frame();
            frame.Content = new cst.nijien();
            prvh.Children.Add(frame);
        }
        private void OnCardTriggered()
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (preview.Children.Contains(currentElement))
            {
                preview.Children.Remove(currentElement);
            }
        }
    }
}
