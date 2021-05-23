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

namespace HomeworkManager.cst
{
    /// <summary>
    /// nijien.xaml 的交互逻辑
    /// </summary>
    public partial class nijien : Page
    {
        public nijien()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
            media.Play();
            media.Width = double.NaN;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            media.Stop();
            
            media.Width = 193;

        }

        private void media_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string img = "";
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                img = openFileDialog.FileName;
                //media.Source = new Uri(img);
                MediaElement mediaElement = new MediaElement();
                mediaElement.Source = new Uri(img);
                mediaElement.HorizontalAlignment = HorizontalAlignment.Left;
                mediaElement.VerticalAlignment = VerticalAlignment.Top;
               
            }
        }
    }
}
