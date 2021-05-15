using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
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
            if (img==string.Empty)
            {
                advancedAddlesson = new CustomizeLessonDemo((string)Subjects.SelectedItem, homeworklist1.Text,_endtime, WrapPanel, frame);
            }
            else
            {
                advancedAddlesson = new CustomizeLessonDemo((string)Subjects.SelectedItem, homeworklist1.Text, _endtime, WrapPanel, frame,img);
            }
            frame.Content = advancedAddlesson;
            frame.Background = Brushes.Transparent;


            WrapPanel.Children.Add(frame);



            SaveXML();
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
            this.Close();
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
                case -1:break;
                case 0:
                    
                    break;
                case 1: break;
                case 2: break;
                case 3: break;
                case 4: break;

                default:
                    break;
            }
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                img = openFileDialog.FileName;
            }  
        }
    }
}
