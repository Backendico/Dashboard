﻿using Dashboard.Dashboards.Pages.Aut;
using Dashboard.Dashboards.Pages.Main;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System.Windows.Controls;

namespace Dashboard.Dashboards.Pages.PageStudios.Moduls.Studios_GridView
{
    /// <summary>
    /// Interaction logic for StudioGridView.xaml
    /// </summary>
    public partial class StudioGridView : UserControl
    {
        public StudioGridView(BsonDocument Detail, IPageStudio PageStudio)
        {
            InitializeComponent();
            TextCreatedTime.Text = Detail["Created"].ToLocalTime().ToString();
            TextStudio.Text = Detail["Name"].ToString();
            TextType.Text = Detail["Type"].ToString();
            TextUniqeID.Text = Detail["Token"].ToString();
            TextTokenCreator.Text = Detail["Creator"].ToString();
            TextDatabase.Text = Detail["Database"].ToString();

            //open main page
            MouseDown += (s, e) =>
            {
                SettingUser.CurentDetailStudio = Detail;
                PageAUT.Placeholder.Children.Add(new MainPage());
                PageStudio.ChangeVisibility(false);

            };

        }
    }
}
