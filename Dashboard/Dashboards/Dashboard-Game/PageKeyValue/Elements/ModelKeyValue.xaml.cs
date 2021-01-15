using Dashboard.GlobalElement;
using MongoDB.Bson;
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

namespace Dashboard.Dashboards.Dashboard_Game.PageKeyValue.Elements
{
    /// <summary>
    /// Interaction logic for ModelKeyValue.xaml
    /// </summary>
    public partial class ModelKeyValue : UserControl
    {
        public ModelKeyValue(BsonElement Detail, Action Refresh)
        {
            InitializeComponent();
            TextName.Text = Detail.Name;
            TextValue.Text = Detail.Value.ToString();
            TextBoxKey.Text = Detail.Name;
            TextBoxValue.Text = Detail.Value.ToString();


            //action delete key
            BTNDelete.MouseDown += (s, e) =>
            {
                SDK.SDK_PageDashboards.DashboardGame.PageKeyValue.RemoveKeys(Detail.Name, result =>
                {
                    if (result)
                    {
                        Refresh();
                    }
                    else
                    {
                        DashboardGame.Notifaction("Faild Delte", Notifaction.StatusMessage.Error);
                    }

                });
            };

            //action BTNEdit
            BTNEdit.MouseDown += (s, e) =>
            {
                Root.Visibility = Visibility.Collapsed;
                RootEdit.Visibility = Visibility.Visible;
            };


            //action btnBack
            BTNBack.MouseDown += (s, e) =>
            {
                Root.Visibility = Visibility.Visible;
                RootEdit.Visibility = Visibility.Collapsed;
            };


            //Action btn Save
            BTNSave.MouseDown += (s, e) =>
            {
                if (TextBoxValue.Text.Length >= 1 && TextBoxKey.Text.Length >= 1)
                {
                    SDK.SDK_PageDashboards.DashboardGame.PageKeyValue.UpdateKeys(TextBoxKey.Text, TextBoxValue.Text.ToString(), result =>
                    {
                        if (result)
                        {
                            DashboardGame.Notifaction("Key Updated", Notifaction.StatusMessage.Ok);
                            Refresh();
                        }
                        else
                        {
                            DashboardGame.Notifaction("Update Faild", Notifaction.StatusMessage.Error);
                        }

                    });

                }
                else
                {
                    DashboardGame.Notifaction("Key or Value Short", Notifaction.StatusMessage.Error);
                }
            };


        }
    }
}
