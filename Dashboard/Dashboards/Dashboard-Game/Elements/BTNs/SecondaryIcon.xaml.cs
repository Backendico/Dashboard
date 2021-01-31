using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.BTNs
{
    /// <summary>
    /// Interaction logic for SecondaryIcon.xaml
    /// </summary>
    public partial class SecondaryIcon : UserControl
    {
        public SecondaryIcon()
        {
            InitializeComponent();

            GotFocus += (s, e) =>
            {
                BorderFocuse.BorderThickness = new Thickness(2);
            };

            LostFocus += (s, e) =>
            {
                BorderFocuse.BorderThickness = new Thickness(0);
            };
            MouseEnter += (s, e) =>
            {
                Tooltip.Visibility = Visibility.Visible;
            };
            MouseLeave += (s, e) => { Tooltip.Visibility = Visibility.Collapsed; };


        }
    }
}
