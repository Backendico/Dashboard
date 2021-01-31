using Dashboard.Dashboards.Dashboard_Game.Elements.Icons;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.BTNs
{
    /// <summary>
    /// Interaction logic for GhostIcon.xaml
    /// </summary>
    public partial class GhostIcon : UserControl
    {
        public IconType Icon
        {
            set
            {
                TextIcon.Text = new Icons.Icons()[value];
                TextTooltip.Text = value.ToString();
            }
        }

        public GhostIcon()
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
