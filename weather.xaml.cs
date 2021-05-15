using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// weather.xaml 的交互逻辑
    /// </summary>
    public partial class weather : Page
    {
		Panel panel1;
		Frame frame1;
		public weather(Panel panel,Frame frame)
        {
            InitializeComponent();
			panel1 = panel;
			frame1 = frame;
        }


		public class Alarm
		{
			public string alarm_type { get; set; }
			public string alarm_level { get; set; }
			public string alarm_content { get; set; }
		}

		public class Hours
		{
			public string hours { get; set; }
			public string wea { get; set; }
			public string wea_img { get; set; }
			public string tem { get; set; }
			public string win { get; set; }
			public string win_speed { get; set; }
		}

		public class Index
		{
			public string title { get; set; }
			public string level { get; set; }
			public string desc { get; set; }
		}

		public class Data
		{
			public string day { get; set; }
			public string date { get; set; }
			public string week { get; set; }
			public string wea { get; set; }
			public string wea_img { get; set; }
			public string wea_day { get; set; }
			public string wea_day_img { get; set; }
			public string wea_night { get; set; }
			public string wea_night_img { get; set; }
			public string tem { get; set; }
			public string tem1 { get; set; }
			public string tem2 { get; set; }
			public string humidity { get; set; }
			public string visibility { get; set; }
			public string pressure { get; set; }
			public string[] win { get; set; }
			public string win_speed { get; set; }
			public string win_meter { get; set; }
			public string sunrise { get; set; }
			public string sunset { get; set; }
			public string air { get; set; }
			public string air_level { get; set; }
			public string air_tips { get; set; }
			public Alarm alarm { get; set; }
			public List<Hours> hours { get; set; }
			public List<Index> index { get; set; }
		}

		public class Aqi
		{
			public string update_time { get; set; }
			public string cityid { get; set; }
			public string city { get; set; }
			public string cityEn { get; set; }
			public string country { get; set; }
			public string countryEn { get; set; }
			public string air { get; set; }
			public string air_level { get; set; }
			public string air_tips { get; set; }
			public string pm25 { get; set; }
			public string pm25_desc { get; set; }
			public string pm10 { get; set; }
			public string pm10_desc { get; set; }
			public string o3 { get; set; }
			public string o3_desc { get; set; }
			public string no2 { get; set; }
			public string no2_desc { get; set; }
			public string so2 { get; set; }
			public string so2_desc { get; set; }
			public string co { get; set; }
			public string co_desc { get; set; }
			public string kouzhao { get; set; }
			public string yundong { get; set; }
			public string waichu { get; set; }
			public string kaichuang { get; set; }
			public string jinghuaqi { get; set; }
		}

		public class RootObject
		{
			public string cityid { get; set; }
			public string city { get; set; }
			public string cityEn { get; set; }
			public string country { get; set; }
			public string countryEn { get; set; }
			public string update_time { get; set; }
			public List<Data> data { get; set; }
			public Aqi aqi { get; set; }
		}
		public static string Get(string url)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            try
            {
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
            return result;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

			Task task = new Task(new Action(() =>
			{
				string s = Get(@"https://v0.yiketianqi.com/api?version=v9&appid=34226732&appsecret=HlF5gzQ8");
				Console.WriteLine(s);
				RootObject rb = JsonConvert.DeserializeObject<RootObject>(s);
				main.Dispatcher.Invoke(new Action(() => {
					main.Text = $" 地区:{rb.city} 明日天气:{rb.data[1].wea} 明日温度：{rb.data[1].tem1}/{rb.data[1].tem2}\r 日出时间:{rb.data[1].sunrise}日落时间:{rb.data[1].sunset}\r" +
						$" 风向:{rb.data[1].win[1].ToString()+"/"+ rb.data[1].win[1].ToString()} 风速:{rb.data[1].win_speed}\r 湿度:{rb.data[1].humidity}能见度:{rb.data[1].visibility}\r 空气质量({rb.data[1].air_level}):{rb.data[1].air_tips}" +
						$"气压:{rb.data[1].pressure}\r 气象监测与预警:{((rb.data[1].alarm.alarm_content == "") ? "无警报" : $"预警类别{rb.data[1].alarm.alarm_type} 预警等级{rb.data[1].alarm.alarm_level} 预警信息{rb.data[1].alarm.alarm_content}")}";

				}));
			}));
			task.Start();
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
				panel1.Children.Remove(frame1);
            }
            catch (Exception ex)
            {
				MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
