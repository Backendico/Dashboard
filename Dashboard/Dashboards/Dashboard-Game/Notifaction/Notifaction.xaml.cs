using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Dashboard.Dashboards.Dashboard_Game.Notifaction
{

    public partial class Notifaction : UserControl
    {
        public Notifaction(string _Message, StatusMessage Status)
        {
            InitializeComponent();
            TextMessage.Text = _Message;

            switch (Status)
            {
                case StatusMessage.Error:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.Tomato);
                    break;
                case StatusMessage.Ok:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                    break;
                case StatusMessage.Warrning:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    break;
                default:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.Black);
                    break;
            }
            DashboardGame.Dashboard.Root.Children.Add(this);
        }



        public void Close(object sender, EventArgs e)
        {
            try
            {
                DashboardGame.Dashboard.Root.Children.Remove(this);
            }
            catch (Exception)
            {

                Debug.WriteLine("error notifactio");
            }
        }

    }

    public enum StatusMessage
    {

        Error, Ok, Warrning
    }
}
