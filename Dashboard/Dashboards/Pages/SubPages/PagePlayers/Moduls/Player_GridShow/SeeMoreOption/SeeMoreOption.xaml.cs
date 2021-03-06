using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Pages.SubPages.PagePlayers.Moduls.Player_GridShow.SeeMoreOption
{

    public partial class SeeMoreOption : UserControl
    {

        public string Text
        {
            get
            {
                return TextOption.Text;
            }
            set
            {
                TextOption.Text = value;
            }
        }
        public SeeMoreOption()
        {
            InitializeComponent();

            MouseEnter += (s, e) =>
            {

                Background = (Brush)new BrushConverter().ConvertFromString("#f4f4f4");
            };

            MouseLeave += (s, e) =>
            {

                Background = new SolidColorBrush(Colors.Transparent);
            };
        }


    }
}
