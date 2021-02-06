using Dashboard.GlobalElement;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.DropDown
{
    /// <summary>
    /// Interaction logic for DropDownItem.xaml
    /// </summary>
    public partial class DropDownItem : UserControl
    {
        internal string Text
        {
            set
            {
                NameItem.Text = value;
            }
            get
            {
                return NameItem.Text;
            }
        }

        public DropDownItem()
        {
            InitializeComponent();
            MouseEnter += (s, e) =>
            {
                Background = (Brush)new BrushConverter().ConvertFromString("#d0e2ff");
            };

            MouseLeave += (s, e) =>
            {
                Background = new SolidColorBrush(Colors.Transparent);
            };

            MouseDown += (s, e) =>
            {
                GlobalEvents.FindParent<DropDownPrimary>(this).SelectItem(this);
            };
        }



    }
}
