using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios.Element;
using Dashboard.GlobalElement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageStudios
{

    public partial class SubPageStudios : UserControl
    {
        public SubPageStudios()
        {
            InitializeComponent();
            SDK.SDK_PageDashboards.DashboardGame.PageStudios.ReciveStudios(
                result =>
                {
                    foreach (var item in result)
                    {
                        PlaceContentStudios.Children.Add(new ModelStudio(item.Value.AsBsonDocument));
                    }
                },
                () =>
                {
                    DashboardGame.Notifaction("Faild Recive", Notifaction.StatusMessage.Error);
                }
                );

            MouseDown += (s, e) =>
            {

            };
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
            DashboardGame.Dashboard.Blure(false);
        }
    }
}
