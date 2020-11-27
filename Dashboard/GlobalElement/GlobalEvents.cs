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

        public static void ControllNumberFilde(object Sender, TextChangedEventArgs Event)
        {
            var Textbox = Sender as TextBox;

            try
            {
                int.Parse(Textbox.Text);
            }
            catch (Exception ex)
            {
                Textbox.Text = "";
            }

        }

    }
}
