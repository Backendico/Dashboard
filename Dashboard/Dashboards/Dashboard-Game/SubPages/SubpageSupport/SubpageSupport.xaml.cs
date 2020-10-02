using Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport.Element;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport
{
    /// <summary>
    /// Interaction logic for SubpageSupport.xaml
    /// </summary>
    public partial class SubpageSupport : UserControl
    {
        public SubpageSupport()
        {
            InitializeComponent();

            //init btnsener //send support
            BTNAddSupport.MouseDown += (o, s) =>
            {
                var Detailsupport = new BsonDocument
                {
                    { "Header" ,TextboxTitle.Text},
                    {"Priority",ComboBoxPriority.SelectedIndex },
                    {"Part",ComboBoxPart.SelectedIndex },
                };
                SDK.SDK_PageDashboards.DashboardGame.PageSupport.AddSupport(Detailsupport, result =>
                {
                    if (result)
                    {
                        DashboardGame.Notifaction("Support Add \n You will receive the answer soon", Notifaction.StatusMessage.Ok);
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild add", Notifaction.StatusMessage.Error);
                    }

                });

            };
            SDK.SDK_PageDashboards.DashboardGame.PageSupport.ReciveSupports(
                result =>
                {
                    foreach (var item in result[1].AsBsonArray)
                    {
                        PlaceContentSupport.Children.Add(new ModelSupport(item.AsBsonDocument, PageEachQuestion));
                    }
                },
                () =>
                {
                });


            Signal();

        }

        private void Close(object sender, EventArgs e)
        {
            DashboardGame.MainRoot.Children.Remove(this);
        }

        public async void Signal()
        {

        }
    }
}
