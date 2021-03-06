using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.Tooltips
{

    public partial class TooltipPrimaryText : UserControl
    {
        public string PlaceHolder
        {
            set
            {
                TextHelp.Text = value;
            }

            get
            {
                return TextHelp.Text;
            }
        }

        public string Text
        {
            set
            {
                ToolTip = value;
            }

            get
            {
                return ToolTip as string;
            }
        }

        public TooltipPrimaryText()
        {
            InitializeComponent();



        }
    }
}
