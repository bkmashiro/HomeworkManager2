using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace HomeworkManager
{
    /// <summary>
    /// AddLesson.xaml 的交互逻辑
    /// </summary>
    public partial class AddLesson : Window
    {
        public AddLesson(WrapPanel wp)
        {
            InitializeComponent();
            WrapPanel = wp;
        }

        //Frame frame = null;
        WrapPanel WrapPanel = null;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            pv.ReadXML();
            string[] subjs = { "语文", "数学", "英语", "物理", "化学", "技术", "政治", "历史", "地理", "生物", "最高指示", "其他" };
            Subjects.ItemsSource = subjs;
            Subjects.SelectedIndex = 0;
            Subjects_Copy.ItemsSource = new string[] { "晚一", "晚二", "晚三", "今日", "长期", "选择时间（在下方选择）" };
            Subjects_Copy.SelectedIndex = 0;
            //Animation.OpacityAndPositionAnimation(hint, 100, 0, -30, 0, TimeSpan.FromSeconds(2), finished);

            //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
            //task.Start();
            homeworklist1.Focus();
            this.Left = 1920 / 2-400;
            this.Top = 300;
            main.Visibility = Visibility.Visible;
            //customize.Visibility = Visibility.Collapsed;
            others.Visibility = Visibility.Collapsed;

        }

        bool IsFinished = false;
        private void finished(object sender, EventArgs e)
        {
            if (!IsFinished)
            {
                homeworklist1.Text = string.Empty;
                //hint.Visibility = Visibility.Collapsed;
                IsFinished = true;
            }
        }

        private void finished2()
        {
            if (!IsFinished)
            {
                homeworklist1.Text = string.Empty;
                IsFinished = true;
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


        private void RegistNewHmwk()
        {
            if (homeworklist1.Text == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("不要输入空的作业！");
                return;
            }
            Animation.OpacityAnimation(gg, 1, 0, TimeSpan.FromMilliseconds(200), DimFinished);
            Frame frame = new Frame();
            pv.Task task = new pv.Task();
            task.name = Subjects.Text;

            task.Content = homeworklist1.Text.Trim();
            task.StartTime = DateTime.Now;
            try
            {
                task.EndTime = datepicker.SelectedDate.Value.Date.AddTicks(timepicker.SelectedTime.Value.TimeOfDay.Ticks);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Errors occurred at" + ex.Message + Environment.NewLine + "请输入正确的时间格式");
                task.EndTime = DateTime.Now.AddDays(1);
                return;
                throw;
            }
            if (!btg1)
            {
                task.EndTime = DateTime.Now.AddYears(50);
            }
            if (btg2)
            {
                task.Appendix = "页码:" + page1.Text + "~" + page2.Text;
            }
            else
            {
                task.Appendix = string.Empty;
            }
            task.Finished = false;
            LessonDemo ls = new LessonDemo(frame, WrapPanel, task);
            ls.Margin = new Thickness(1, 1, 1, 1);
            frame.Content = ls;
            frame.Background = Brushes.Transparent;


            WrapPanel.Children.Add(frame);



            SaveXML();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateAllCand();
            RegistNewHmwk();
           

        }

        private void DimFinished(object sender, EventArgs e)
        {
            gg.Children.Clear();
            //显示
            Frame frame = new Frame();
            frame.Width = 960;
            thank thank = new thank();
            frame.Content = thank;

            gg.Children.Add(frame);
            Animation.OpacityAnimation(gg, 0, 1, TimeSpan.FromMilliseconds(200), Finished2);
            //gg.Children.Add();

        }

        private void Finished2(object sender, EventArgs e)
        {


            Animation.OpacityAnimation(gg, 1, 0, TimeSpan.FromMilliseconds(2000), Finished3);
            //gg.Children.Add();
            //this.Close();

        }

        private void Finished3(object sender, EventArgs e)
        {
            this.Close();
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
        bool btg1 = false;

        private void tg1_Click(object sender, RoutedEventArgs e)
        {
            if (btg1)
            {
                Subjects_Copy.Visibility = Visibility.Collapsed;
                datepicker.Visibility = Visibility.Collapsed;
                timepicker.Visibility = Visibility.Collapsed;
            }
            else
            {
                Subjects_Copy.Visibility = Visibility.Visible;
                datepicker.Visibility = Visibility.Visible;
                timepicker.Visibility = Visibility.Visible;
            }
            btg1 = !btg1;
        }
        bool btg2 = false;
        private void tg2_Click(object sender, RoutedEventArgs e)
        {

            if (btg2)
            {
                page1.Visibility = Visibility.Collapsed;
                page2.Visibility = Visibility.Collapsed;
                bolang.Visibility = Visibility.Collapsed;
            }
            else
            {
                page1.Visibility = Visibility.Visible;
                page2.Visibility = Visibility.Visible;
                bolang.Visibility = Visibility.Visible;
            }
            btg2 = !btg2;
        }

        private void Subjects_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((string)Subjects_Copy.SelectedItem)
            {
                //{"晚一","晚二","晚三","今日","长期","选择时间（在下方选择）" };
                case "晚一": datepicker.SelectedDate = DateTime.Today; timepicker.SelectedTime = DateTime.Parse("19:20:00"); break;
                case "晚二": datepicker.SelectedDate = DateTime.Today; timepicker.SelectedTime = DateTime.Parse("20:20:00"); break;
                case "晚三": datepicker.SelectedDate = DateTime.Today; timepicker.SelectedTime = DateTime.Parse("21:20:00"); break;
                case "今日": datepicker.SelectedDate = DateTime.Today; timepicker.SelectedTime = DateTime.Parse("23:59:00"); break;
                case "选择时间（在下方选择）": datepicker.SelectedDate = DateTime.Today; break;
                default:
                    Console.WriteLine(timepicker.SelectedTime.Value.ToLongTimeString());
                    break;
            }
            Console.WriteLine(timepicker.SelectedTime.Value.ToLongTimeString());
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //if (addhomewrk.Text == "")
            //{
            //    System.Windows.MessageBox.Show("请勿添加空的作业！");
            //    return;
            //}
            //homeworklist.Items.Add(addhomewrk.Text);

            //bool found = false;
            //for (int i = 0; i < pv.candiate_lesson.Count; i++)
            //{
            //    if (pv.candiate_lesson[i] == (string)Subjects.SelectedItem)
            //    {
            //        if (!pv.candiate[i].Contains(addhomewrk.Text))
            //        {
            //            pv.candiate[i].Add(addhomewrk.Text);
            //            found = true;
            //        }
            //        break;
            //    }
            //}
            //if (!found && (string)Subjects.SelectedItem != "新增（在右侧选取，不保存）")
            //{
            //    if (!pv.candiate_lesson.Contains((string)Subjects.SelectedItem))
            //    {
            //        pv.candiate_lesson.Add((string)Subjects.SelectedItem);
            //        pv.candiate.Add(new List<string> { (string)homeworklist.Items[0] });
            //    }
            //}
            //addhomewrk.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //homeworklist.Items.Remove(homeworklist.SelectedItem);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            homeworklist1.Text = string.Empty;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void candi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (candi.SelectedItem != null)
            {
                homeworklist1.AppendText(Environment.NewLine + (string)candi.SelectedItem);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            PerformHelp("填写数字即可");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            PerformHelp("打开按钮就会显示时间，不显示时间就不需要打开。超时boom。");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            PerformHelp("直接输入 ");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            PerformHelp("mull");
        }

        private void PerformHelp(string help)
        {
            dialoghst.IsOpen = true;
            //helpinfo.Text = help;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            dialoghst.IsOpen = false;
        }


        private void addhomewrk_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    if (addhomewrk.Text == "")
            //    {
            //        System.Windows.MessageBox.Show("请勿添加空的作业！");
            //        return;
            //    }
            //    homeworklist.Items.Add(addhomewrk.Text);

            //    bool found = false;
            //    for (int i = 0; i < pv.candiate_lesson.Count; i++)
            //    {
            //        if (pv.candiate_lesson[i] == (string)Subjects.SelectedItem)
            //        {
            //            if (!pv.candiate[i].Contains(addhomewrk.Text))
            //            {
            //                pv.candiate[i].Add(addhomewrk.Text);
            //                found = true;
            //            }
            //            break;
            //        }
            //    }
            //    if (!found && (string)Subjects.SelectedItem != "新增（在右侧选取，不保存）")
            //    {
            //        if (!pv.candiate_lesson.Contains((string)Subjects.SelectedItem))
            //        {
            //            pv.candiate_lesson.Add((string)Subjects.SelectedItem);
            //            pv.candiate.Add(new List<string> { (string)homeworklist.Items[0] });
            //        }
            //    }
            //    addhomewrk.Text = "";

            //}
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
            if (!found && (string)Subjects.SelectedItem != "新增（在右侧选取，不保存）")
            {
                if (!pv.candiate_lesson.Contains((string)Subjects.SelectedItem))
                {
                    pv.candiate_lesson.Add((string)Subjects.SelectedItem);
                    pv.candiate.Add(new List<string> { s });
                }
            }
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


        private void homeworklist1_MouseEnter(object sender, MouseEventArgs e)
        {
            finished2();
        }

        private void candi_MouseEnter(object sender, MouseEventArgs e)
        {
            finished2();
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(11);

            //collection_Customize = nav.Content;
            
            main.Visibility = Visibility.Visible;
            //customize.Visibility = Visibility.Collapsed;
            others.Visibility = Visibility.Collapsed;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            //customize.Visibility = Visibility.Visible;
            main.Visibility = Visibility.Collapsed;
            others.Visibility = Visibility.Collapsed;
            //nav.Content = new addl.customize();
        }

        object collection_Customize = null;
        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            others.Visibility = Visibility.Visible;
            main.Visibility = Visibility.Collapsed;
            //customize.Visibility = Visibility.Collapsed;
            


        }
    }
}
