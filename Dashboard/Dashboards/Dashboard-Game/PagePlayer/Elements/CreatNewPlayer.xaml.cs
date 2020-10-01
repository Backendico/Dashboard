using Dashboard.Dashboards.Dashboard_Game.Notifaction;
using Dashboard.GlobalElement;
using RestSharp;
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

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements
{
    /// <summary>
    /// Interaction logic for CreatNewPlayer.xaml
    /// </summary>
    public partial class CreatNewPlayer : UserControl
    {

        public CreatNewPlayer(Action<object, RoutedEventArgs> Refreshlist)
        {
            InitializeComponent();

            this.Refreshlist = Refreshlist;
        }


        /// <summary>
        /// creat new user
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Add(object sender, MouseButtonEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.CreatPlayer(TextBoxUsername.Text, TextBoxPassword.Password,
             () =>
             {
                 Refreshlist(null, null);
                 DashboardGame.Notifaction("Player Added", StatusMessage.Ok);
             },
             () =>
             {

                 DashboardGame.Notifaction("Faild Add", StatusMessage.Error);

             });

        }

        private void Close(object sender, RoutedEventArgs e)
        {
            (Parent as Grid).Children.Remove(this);
        }



        private void CheackUsername(object sender, TextChangedEventArgs e)
        {

            var textbox = sender as TextBox;
            SDK.SDK_PageDashboards.DashboardGame.PagePlayers.SearchUsername(textbox.Text, Result =>
            {
                if (Result)
                {

                    textbox.BorderBrush = new SolidColorBrush(Colors.Tomato);
                    textbox.Text += new Random().Next();
                    DashboardGame.Notifaction("Username found ", StatusMessage.Error);
                }
                else
                {
                    textbox.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                }

            });

        }


        Action<object, RoutedEventArgs> Refreshlist;

    }
}
