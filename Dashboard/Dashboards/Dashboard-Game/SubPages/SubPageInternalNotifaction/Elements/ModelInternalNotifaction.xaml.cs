using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages.SubPageInternalNotifaction.Elements
{
    public partial class ModelInternalNotifaction : UserControl
    {
        internal BsonDocument Detail;
        public ModelInternalNotifaction(BsonDocument Detail, Action RefreshList)
        {
            InitializeComponent();

            this.Detail = Detail;

            TextHeader.Text = Detail["Header"].ToString();
            TextDescription.Text = Detail["Description"].ToString();

            var Time = Detail["Time"].ToLocalTime();
            if (DateTime.Now.AddHours(-3) <= Detail["Time"].ToLocalTime())
            {
                TextTime.Text = Time.Hour + ":" + Time.ToLocalTime().Minute;

            }
            else
            {
                TextTime.Text = Time.Hour + ":" + Time.Minute + "  " + Time.Year + "/" + Time.Month + "/" + Time.Day;
            }


            //remove singel Notif
            BTNDelete.MouseDown += (s, e) =>
            {
                SDK.SDK_PageDashboards.DashboardGame.PageLog.DeleteLog(Detail, result =>
                {
                    (Parent as StackPanel).Children.Remove(this);

                    RefreshList();
                });
            };



            //collepts btn see detail Here
            if (Detail["Detail"].AsBsonDocument.ElementCount <= 0)
            {
                BTNDetail.Visibility = Visibility.Collapsed;
            }
            else
            {
                BTNDetail.MouseDown += (s, e) =>
                {
                    var serilsedetail = "";

                    foreach (var item in Detail["Detail"].AsBsonDocument)
                    {
                        serilsedetail += item.Name + " : " + item.Value + "\n";
                    }

                    DashboardGame.Dialog(serilsedetail, Detail["Header"].ToString());
                };
            };

        }
    }
}
