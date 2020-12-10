using Dashboard.Dashboards.Dashboard_Game;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Dashboard.GlobalElement
{
    abstract class GlobalEvents
    {
        public static void CopyText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBlock)
            {
                var Text = sender as TextBlock;
                Clipboard.SetText(Text.Text);

            }
            else if (sender is Run)
            {
                var Text = sender as Run;
                Clipboard.SetText(Text.Text);

            }
            else if (sender is TextBox)
            {
                var Text = sender as TextBox;
                Clipboard.SetText(Text.Text);

            }

            DashboardGame.Notifaction("Copyed !", Dashboards.Dashboard_Game.Notifaction.StatusMessage.Ok);
        }

        public static void ControllNumberFilde(TextBox Sender)
        {
            try
            {
                if (Sender.Text.Length >= 1)
                    int.Parse(Sender.Text);
            }
            catch (Exception)
            {
                Sender.Text = "";
                DashboardGame.Notifaction("Field is Number", Dashboards.Dashboard_Game.Notifaction.StatusMessage.Error);
            }

        }

        public static bool ControllLinks(object Sender)
        {
            var Text = Sender as TextBox;
            try
            {
                if (Text.Text.Length >= 1)
                    new Uri(Text.Text);
                return true;
            }
            catch (Exception )
            {
                Text.Text = "";
                DashboardGame.Notifaction("Field is Link", Dashboards.Dashboard_Game.Notifaction.StatusMessage.Error);
                return false;   
            }

        }

        public static bool ControllLinkImages(object Sender)
        {
            var Text = Sender as TextBox;

            try
            {
                if (Text.Text.Contains(".png")||Text.Text.Contains(".PNG"))
                {
                    return true;
                }
                else if(Text.Text.Length>=1)
                {
                    throw new Exception();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                if (Text.Text.Length>=1)
                {
                DashboardGame.Notifaction("Enter a direct link to Image", Dashboards.Dashboard_Game.Notifaction.StatusMessage.Warrning);
                }
                Text.Text = "";
                return false;   
            }
        }


    }
}
