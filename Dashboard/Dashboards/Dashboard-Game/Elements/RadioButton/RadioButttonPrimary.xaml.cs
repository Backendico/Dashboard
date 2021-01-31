using System.Diagnostics;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.Elements.RadioButton
{
    /// <summary>
    /// Interaction logic for RadioButttonPrimary.xaml
    /// </summary>
    public partial class RadioButttonPrimary : UserControl
    {
        public bool IsSelect
        {
            set
            {
                if (value && Cheacker.IsEnabled)
                {
                    Cheack = value;
                    TextCheack.Text = "\xECCC";
                }
                else if (Cheacker.IsEnabled)
                {
                    Cheack = value;
                    TextCheack.Text = "";
                }
            }
            get { return Cheack; }
        }

        bool Cheack = false;

        public RadioButttonPrimary()
        {
            InitializeComponent();
            MouseDown += (s, e) =>
            {
                if (!Cheack)
                {
                    if (Parent.GetType() == typeof(Grid))
                    {
                        Debug.WriteLine("delete for grid");
                    }
                    else if (Parent.GetType() == typeof(StackPanel))
                    {

                        foreach (var item in (Parent as StackPanel).Children)
                        {
                            if (item.GetType() == typeof(RadioButttonPrimary))
                            {
                                (item as RadioButttonPrimary).IsSelect = false;
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Parent for radio btn not set ");
                    }

                    IsSelect = true;
                }
            };
        }
    }
}
