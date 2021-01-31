using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.BTNs
{
    /// <summary>
    /// Interaction logic for DangerButtonIcon.xaml
    /// </summary>
    public partial class DangerButtonIcon : UserControl
    {
        public DangerButtonIcon()
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

        }
    }
}
