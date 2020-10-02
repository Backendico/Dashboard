using Dashboard.Dashboards.Dashboard_Game.SubPages.SubpageSupport.Element;
using Dashboard.GlobalElement;
using Microsoft.AspNetCore.SignalR.Client;
using MongoDB.Bson;
using System;
using System.Diagnostics;
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
        }

        private void Close(object sender, EventArgs e)
        {
            DashboardGame.MainRoot.Children.Remove(this);
        }

        ////here
        //public async void Signal()
        //{
        //    var connection = new HubConnectionBuilder().WithUrl("https://localhost:44377/chathub").Build();
            
        //    //connection.On<string, string>("ReceiveMessage", (s, ss) =>
        //    //    {
        //    //        Debug.WriteLine(s, ss);

        //    //    });

        //    await connection.StartAsync();
        //    await connection.InvokeAsync("SendMessage", "Koala", "slm");
        //    Debug.WriteLine(connection.ConnectionId);

        //}
    }
}
