﻿using Dashboard.Dashboards.Dashboard_Game.Add_ons.JsonEditor;
using Dashboard.Dashboards.Dashboard_Game.PageContent.Elements.ModelContent;
using Dashboard.GlobalElement;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard.Dashboards.Dashboard_Game.PageContent.Elements.EditContent
{

    public partial class EditContent : UserControl
    {
        Grid CurentPage;
        Button CurentTab;

        IEditContent Editor;

        public EditContent(IEditContent Editor)
        {
            InitializeComponent();
            CurentPage = ContentSetting;
            CurentTab = BTNSetting;
            this.Editor = Editor;

            InitEditor();

            //init edit content
            ContentElement.Children.Add(new JsonEditor(Editor.DetailContent["Content"].AsBsonDocument, () =>
            {
                Editor.Save();
                InitEditor();
            }));


            //Chaneg texname
            TextName.LostFocus += (s, e) =>
            {
                if (TextName.Text.Length >= 1)
                {
                    Editor.DetailContent["Settings"]["Name"] = TextName.Text;
                    Editor.Save();
                }
                else
                {
                    TextName.Text = Editor.DetailContent["Settings"]["Name"].ToString();
                }
            };

            TextAccess.LostFocus += (s, e) =>
            {
                if (GlobalEvents.ControllNumberFilde(TextAccess))
                {
                    Editor.DetailContent["Settings"]["Access"] = int.Parse(TextAccess.Text);
                    Editor.Save();
                }
                else
                {
                    TextAccess.Text = Editor.DetailContent["Settings"]["Access"].ToString();
                }
            };

            //Copy text
            TextToken.MouseDown += GlobalEvents.CopyText;
        }

        public void InitEditor()
        {
            TextContent.Text = Editor.DetailContent["Settings"]["Name"].ToString();
            TextName.Text = Editor.DetailContent["Settings"]["Name"].ToString();
            TextAccess.Text = Editor.DetailContent["Settings"]["Access"].ToString();
            TextToken.Text = Editor.DetailContent["Settings"]["Token"].ToString();
            TextIndex.Text = Editor.DetailContent["Settings"]["Index"].ToString();
            TextElement.Text = Editor.DetailContent["Content"].AsBsonDocument.ElementCount.ToString();
            TextCreated.Text = Editor.DetailContent["Settings"]["Created"].ToLocalTime().ToString();
        }


        void ChangePage(object sender, RoutedEventArgs e)
        {
            var ButtonTab = sender as Button;

            CurentPage.Visibility = Visibility.Collapsed;
            CurentTab.BorderBrush = new SolidColorBrush(Colors.Transparent);

            switch (ButtonTab.Content)
            {
                case "Setting":
                    {
                        CurentPage = ContentSetting;
                        CurentTab = BTNSetting;
                    }
                    break;
                case "Content":
                    {
                        CurentPage = ContentElement;
                        CurentTab = BTNContent;
                    }
                    break;
                default:
                    Debug.WriteLine("Not Set");
                    break;
            }

            CurentPage.Visibility = Visibility.Visible;
            CurentTab.BorderBrush = new SolidColorBrush(Colors.Orange);
        }

        public void Close(object E, RoutedEventArgs s)
        {
            DashboardGame.Dashboard.Root.Children.Remove(this);
        }



    }
}
