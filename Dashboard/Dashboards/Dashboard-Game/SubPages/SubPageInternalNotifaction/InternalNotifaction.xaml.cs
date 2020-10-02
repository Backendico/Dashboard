using Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageInternalNotifaction.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageInternalNotifaction
{
    /// <summary>
    /// Interaction logic for InternalNotifaction.xaml
    /// </summary>
    public partial class InternalNotifaction : UserControl
    {

        ModelInternalNotifaction[] Notifactions;
        int CountNotifaction = 10;

        public InternalNotifaction()
        {
            InitializeComponent();

            InitNotifaction();

            //reload list
            BTNReload.MouseDown += (s, o) =>
            {
                InitNotifaction();
            };

            //remove all Notifaction in seces
            BTNDeleteAllView.MouseDown += (o, s) =>
            {
                foreach (var item in Notifactions)
                {

                    SDK.SDK_PageDashboards.DashboardGame.PageLog.DeleteLog(item.Detail, resultDelete =>
                    {
                        if (resultDelete)
                        {
                            PlaceContentNotifacction.Children.Remove(item);
                        }
                    });

                    InitNotifaction();
                };

            };

            //++ countnotifaction
            BTNReciveMore.MouseDown += (o, s) =>
            {
                CountNotifaction += 10;
                InitNotifaction();
            };


        }

        /// <summary>
        /// 1: recive logs 
        /// 2: instance Model notifaction
        /// 3 btn delete all init
        /// </summary>
        void InitNotifaction()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageLog.ReciveLog(CountNotifaction,
                result =>
                {
                    TextHistoryCount.Text = result[1].AsBsonArray.Count.ToString();
                    Notifactions = new ModelInternalNotifaction[result[1].AsBsonArray.Count];

                    PlaceContentNotifacction.Children.Clear();

                    var Count = 0;
                    foreach (var item in result[1].AsBsonArray)
                    {
                        Notifactions[Count] = new ModelInternalNotifaction(item.AsBsonDocument, InitNotifaction);
                        PlaceContentNotifacction.Children.Add(Notifactions[Count]);
                        Count++;
                    }

                },
                () =>
                {

                }
                );

        }

        private void Close(object sender, EventArgs e)
        {
            DashboardGame.MainRoot.Children.Remove(this);
        }

        private void _MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(Grid))
            {
                DoubleAnimation Opacity = new DoubleAnimation();
                Opacity.From = 1;
                Opacity.To = 0;
                Opacity.Duration = new Duration(TimeSpan.FromSeconds(0.3));
                Opacity.Completed += (o, s) =>
                {
                    Close(null, null);
                };

                Storyboard.SetTargetName(Opacity, Root.Name);
                Storyboard.SetTargetProperty(Opacity, new PropertyPath("Opacity"));

                Storyboard MyStory = new Storyboard();

                MyStory.Children.Add(Opacity);
                MyStory.Begin(this);

            }
        }
    }
}
