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

namespace BinaryCalc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += (s, e) => { if (e.Key == Key.Escape) Clear(e.Key); };
            KeyDown += (s, e) => { if (e.Key == Key.Back) Clear(e.Key); };

            foreach (UIElement el in MainRoot.Children)
            {
                if(el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
        }
        bool needCLear = false;
        string num = "";
        string oprn = " ";
        int a = 0;
        int b = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)((Button)e.OriginalSource).Content;
            if (str == "1" || str == "0")
            {
                if (needCLear == true)
                {
                    textLabel.Text = "";
                    needCLear = false;
                }
                textLabel.Text += str;
                num = textLabel.Text;
            }
            if (str == "*" || str == "+")
            {
                a = Convert.ToInt32(num);
                oprn = str;
                textLabel.Text = "";
            }
            if (str == "=")
            {
                needCLear = true;
                b = Convert.ToInt32(num);
                if(oprn == "*")
                    textLabel.Text = Convert.ToString(a & b,2);
                else if(oprn == "+")
                    textLabel.Text = Convert.ToString(a | b, 2);
                else { }
            }
            if(str == "to10")
            {
                needCLear = true;
                textLabel.Text = textLabel.Text + to10(textLabel.Text);
            }
        }
        public static string to10(string a)
        {
            if (a.Contains('='))
            {
                return "";
            }
            else
            {
                a = a.Trim();
                double result = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] == '1')
                    {
                        result += Math.Pow(2, (a.Length - i - 1));
                    }
                }
                return " = " + Convert.ToString(result);
            }
        }
        public void Clear(Key a)
        {
            if(a == Key.Escape)
                textLabel.Text = "";
            if(a == Key.Back)
                if(textLabel.Text.Length !=0)
                textLabel.Text = textLabel.Text.Substring(0, textLabel.Text.Length - 1);
        }
    }
}
