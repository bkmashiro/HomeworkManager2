using System;
using System.Collections.Generic;
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

namespace HomeworkManager
{
    /// <summary>
    /// ringing.xaml 的交互逻辑
    /// </summary>
    public partial class ringing : Page
    {
        public ringing()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string[] subjects = {"语文","数学","英语","物理","化学","技术","政治","地理" };
            foreach (var subj in subjects)
            {
                string hint = subj + "课代表，你还没有确认今天的作业。";
                StackPanel stackPanel = new StackPanel();
                stackPanel.Tag = g;
                stackPanel.FlowDirection = FlowDirection.LeftToRight;
                Label label = new Label();
                label.Content = hint;
                label.FontSize = 15;
                stackPanel.Children.Add(label);
                Button button1 = new Button();
                button1.Content = "确认无作业或即将布置";
                button1.Tag = stackPanel;
                button1.Click += Button1_Click;
                stackPanel.Children.Add(button1);

                g.Children.Add(stackPanel);
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            (((StackPanel)(((Button)sender).Tag)).Tag as StackPanel).Children.Remove(((Button)sender).Tag as StackPanel);
            if (g.Children.Count==1)
            {
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
