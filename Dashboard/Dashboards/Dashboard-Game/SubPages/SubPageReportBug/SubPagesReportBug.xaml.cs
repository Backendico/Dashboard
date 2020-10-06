using Dashboard.GlobalElement;
using MongoDB.Bson;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.SubPages
{
    /// <summary>
    /// Interaction logic for SubPagesReportBug.xaml
    /// </summary>
    public partial class SubPagesReportBug : UserControl
    {
        public SubPagesReportBug()
        {
            InitializeComponent();
            DashboardName.Text = SettingUser.CurentDetailStudio["Name"].ToString();
            Database.Text = SettingUser.CurentDetailStudio["Database"].ToString();
            Token.Text = SettingUser.Token;

            BTNSend.MouseDown += (s, e) =>
            {
                if (Subject.Text.Length >= 1 && Description.Text.Length >= 1)
                {

                    var SerilseDetail = new BsonDocument
                    {
                        { "Topic",Topic.SelectedIndex},
                        {"Priority" ,Priority.SelectedIndex},
                        {"Subject",Subject.Text },
                        {"Description",Description.Text},
                        {"Follow",FllowBug.IsChecked.Value },
                        {"Dashboard",DashboardName.Text },
                        {"Database",Database.Text },
                        {"Token",ObjectId.Parse(Token.Text) },
                    };


                    SDK.SDK_PageDashboards.DashboardGame.PageSupport.AddReportBug(SerilseDetail, result =>
                    {
                        if (result)
                        {
                            DashboardGame.Notifaction("We thank you for sending reports", Notifaction.StatusMessage.Ok);
                        }
                        else
                        {
                            DashboardGame.Notifaction("Faild sending ", Notifaction.StatusMessage.Error);
                        }

                    });
                }
                else
                {
                    DashboardGame.Notifaction("Discription or Subject Short", Notifaction.StatusMessage.Warrning);
                }

            };
        }


        private void Close(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() == typeof(Grid))
            {
                DoubleAnimation anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
                anim.Completed += (s, ee) =>
                {
                    DashboardGame.Dashboard.Root.Children.Remove(this);
                };

                Storyboard.SetTargetName(anim, Root.Name);
                Storyboard.SetTargetProperty(anim, new PropertyPath("Opacity"));

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(anim);

                storyboard.Begin(this);


            }
        }
    }
}
