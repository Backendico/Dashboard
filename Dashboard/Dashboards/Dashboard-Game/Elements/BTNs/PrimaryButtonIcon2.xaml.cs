using Dashboard.Dashboards.Dashboard_Game.Elements.Icons;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.BTNs
{
    /// <summary>
    /// Interaction logic for PrimaryButtonIcon2.xaml
    /// </summary>
    public partial class PrimaryButtonIcon2 : UserControl
    {
        public IconType Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                TextIcon.Text = new Icons.Icons()[value];
                TextTooltip.Text = value.ToString();
                _icon = value;
            }
        }
        IconType _icon = IconType.Add;

        public PrimaryButtonIcon2()
        {
            InitializeComponent();

            Root.GotFocus += (s, e) =>
            {
                BorderFocuse.BorderThickness = new Thickness(2);
                BorderFocuse.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#0f62fe");
            };

            Root.LostFocus += (s, e) =>
            {
                BorderFocuse.BorderThickness = new Thickness(0);
                BorderFocuse.BorderBrush = new SolidColorBrush(Colors.Transparent);
            };
            MouseEnter += (s, e) =>
            {
                Tooltip.Visibility = Visibility.Visible;
            };
            MouseLeave += (s, e) => { Tooltip.Visibility = Visibility.Collapsed; };

            MouseDown += (s, e) =>
            {
                if (Worker != null)
                {
                    Worker();
                }
            };
        }


        internal event Action Worker;

    }
}
