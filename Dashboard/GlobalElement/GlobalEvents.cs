using Dashboard.Dashboards.Dashboard_Game;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

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

        public static bool ControllNumberFilde(TextBox Sender)
        {
            if (Sender.Text.Length >= 1)
            {

                try
                {
                    int.Parse(Sender.Text);

                    return true;
                }
                catch (Exception)
                {
                    Sender.Text = "0";
                    DashboardGame.Notifaction("Field is Number", Dashboards.Dashboard_Game.Notifaction.StatusMessage.Error);
                    return false;
                }

            }
            else
            {
                return false;
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
            catch (Exception)
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
                if (Text.Text.Contains(".png") || Text.Text.Contains(".PNG") || Text.Text.Contains(".Gif") || Text.Text.Contains(".gif") || Text.Text.Contains("JPG") || Text.Text.Contains(".jpg"))
                {
                    return true;
                }
                else if (Text.Text.Length >= 1)
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
                if (Text.Text.Length >= 1)
                {
                    DashboardGame.Notifaction("Enter a direct link to Image", Dashboards.Dashboard_Game.Notifaction.StatusMessage.Warrning);
                }
                Text.Text = "";
                return false;
            }
        }

        public static T FindParent<T>(DependencyObject child)
              where T : DependencyObject
        {
            if (child == null) return null;

            T foundParent = null;
            var currentParent = VisualTreeHelper.GetParent(child);

            do
            {
                var frameworkElement = currentParent as FrameworkElement;
                if (frameworkElement is T)
                {
                    foundParent = (T)currentParent;
                    break;
                }

                currentParent = VisualTreeHelper.GetParent(currentParent);

            } while (currentParent != null);

            return foundParent;
        }

    }
}
