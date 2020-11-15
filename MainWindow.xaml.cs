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

namespace ByteExtraction
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
			SizeChanged += (sender, e) =>
			{
				Title = RenderSize.ToString();
			};
#endif
		}

		private void OnInputTextBox1TextChanged(object sender, TextChangedEventArgs e)
		{
			var data = Encoding.UTF8.GetBytes((sender as TextBox).Text);
			string str = "";
			for(int i = 0; i < data.Length; i++)
			{
				if(i % 16 == 0)
				{
					str += (i != 0 ? "\n" : "") + i.ToString("X08") + "h\t";
				}
				str += data[i].ToString("X02") + " ";
			}
			tbOutput1.Text = str;
		}
		private void OnInputTextBox2TextChanged(object sender, TextChangedEventArgs e)
		{
			var data = (sender as TextBox).Text.Split(' ', '\t', '\n');
			var bytes = data.Select<string,byte>((s) =>
			{
				byte B;
				return byte.TryParse(s, System.Globalization.NumberStyles.HexNumber, null, out B) ? B : Byte.MinValue;
			}).Where((B) => {
				return B != 0;
			});
			var str = Encoding.UTF8.GetString(bytes.ToArray());
			tbOutput2.Text = str;
		}
	}
}
