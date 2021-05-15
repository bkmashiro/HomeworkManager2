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

namespace HomeworkManager.addl
{
    /// <summary>
    /// customize.xaml 的交互逻辑
    /// </summary>
    public partial class customize : Page
    {
        public customize()
        {
            InitializeComponent();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ListBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {

        }

        private void ListBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {

        }

        private void ListBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {

        }

        private void ColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            Console.WriteLine(e.NewValue.A);
        }
    }
}
