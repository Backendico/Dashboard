using Dashboard.Dashboards.Dashboard_Game.PageLogs.Elements;
using Dashboard.GlobalElement;
using System;
using System.Collections.Generic;
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

namespace Dashboard.Dashboards.Dashboard_Game.PageLogs
{
    /// <summary>
    /// Interaction logic for PageLogs.xaml
    /// </summary>
    public partial class PageLogs : UserControl
    {
        public PageLogs()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLog.ReciveLog(
                result =>
                {
                    foreach (var Category in result)
                    {
                        foreach (var Reports in Category.Value.AsBsonDocument)
                        {
                            foreach (var Values in Reports.Value.AsBsonDocument)
                            {
                                Values.Value.AsBsonDocument.Add("ID", Values.Name);
                                PlaceReportModel.Children.Add(new ModelLogDashboard(Values.Value.AsBsonDocument));
                            }
                        }
                        
                    }
                },
                () =>
                {
                });
        }

    }
}
