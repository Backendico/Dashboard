using Dashboard.Dashboards.Dashboard_Game.PageKeyValue.Elements;
using Dashboard.GlobalElement;
using MongoDB.Bson;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Dashboard.Dashboards.Dashboard_Game.PageKeyValue
{
    /// <summary>
    /// Interaction logic for PageKeyValue.xaml
    /// </summary>
    public partial class PageKeyValue : UserControl
    {

        public PageKeyValue()
        {
            InitializeComponent();
            ReciveListAchievements();

            //Action show panel add Key
            BTNShowAddKey.MouseDown += (s, e) =>
            {
                ShowPanelAddChievements();
            };

            //acton Change Type
            ComboBoxType.SelectionChanged += (s, e) =>
            {
                TextBoxValue.IsEnabled = true;
                TextTotallValue.Text = "";

                switch (ComboBoxType.SelectedIndex)
                {

                    case 0: //string
                        {

                        }
                        break;
                    case 1: //int32
                        {

                        }
                        break;
                    case 2: //int64
                        {

                        }
                        break;
                    case 3: //boolean
                        {

                        }
                        break;
                    case 4: //Token
                        {
                            TextBoxValue.IsEnabled = false;
                            TextBoxValue.Text = ObjectId.GenerateNewId().ToString();

                        }
                        break;
                    default:
                        break;
                }

            };


            //action btn add
            BTNaddKeyValue.MouseDown += (s, e) =>
            {
                switch (ComboBoxType.SelectedIndex)
                {
                    case 0: //string
                        {
                            Inject(new BsonElement(TextBoxKey.Text, TextBoxValue.Text));
                        }
                        break;
                    case 1: //int32
                        {
                            try
                            {
                                Inject(new BsonElement(TextBoxKey.Text, Int32.Parse(TextBoxValue.Text)));
                            }
                            catch (Exception)
                            {
                                DashboardGame.Notifaction("The value is not a number", Notifaction.StatusMessage.Error);
                            }
                        }
                        break;
                    case 2: //int64
                        {
                            try
                            {
                                Inject(new BsonElement(TextBoxKey.Text, Int64.Parse(TextBoxValue.Text)));
                            }
                            catch (Exception)
                            {
                                DashboardGame.Notifaction("The value is not a number", Notifaction.StatusMessage.Error);
                            }
                        }
                        break;
                    case 3: //boolean
                        {

                            try
                            {
                                Inject(new BsonElement(TextBoxKey.Text, bool.Parse(TextBoxValue.Text)));
                            }
                            catch (Exception)
                            {
                                DashboardGame.Notifaction("The value is not a Boolean(true,false)", Notifaction.StatusMessage.Error);
                            }
                        }
                        break;
                    case 4: //Token
                        {
                            Inject(new BsonElement(TextBoxKey.Text, TextBoxValue.Text));

                        }
                        break;
                    default:
                        break;
                }

                void Inject(BsonElement Value)
                {

                    SDK.SDK_PageDashboards.DashboardGame.PageKeyValue.AddKey(Value, result =>
                    {
                        if (result)
                        {
                            ReciveListAchievements();
                            DashboardGame.Notifaction("Key Added", Notifaction.StatusMessage.Ok);

                        }
                        else
                        {
                            DashboardGame.Notifaction("Key Dublicated", Notifaction.StatusMessage.Warrning);
                        }
                        ShowOffPanelAchievements();
                    });
                }
            };

            //show off panel Add;
            PanelAddKey.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffPanelAchievements();
                }
            };
        }



        public void ReciveListAchievements()
        {
            SDK.SDK_PageDashboards.DashboardGame.PageKeyValue.ReciveKeys(result =>
            {
                PlaceContentValue.Children.Clear();

                if (result.ElementCount >= 1)
                {
                    if (result["KeyValue"].AsBsonDocument.ElementCount >= 1)
                    {

                        TextTotallValue.Text = result["KeyValue"].AsBsonDocument.ElementCount.ToString();

                        foreach (var item in result["KeyValue"].AsBsonDocument)
                        {
                            PlaceContentValue.Children.Add(new ModelKeyValue(item, ReciveListAchievements));
                        }
                    }
                    else
                    {
                        DashboardGame.Notifaction("No content", Notifaction.StatusMessage.Warrning);
                        ShowPanelAddChievements();
                    }
                }
                else
                {
                    DashboardGame.Notifaction("No content", Notifaction.StatusMessage.Warrning);
                    ShowPanelAddChievements();
                }
            });

        }

        void ShowPanelAddChievements()
        {
            PanelAddKey.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelAddKey.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void ShowOffPanelAchievements()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelAddKey.Visibility = Visibility.Collapsed;

                PanelAddKey.Visibility = Visibility.Collapsed;

                TextBoxKey.Text = "";
                TextBoxValue.Text = "";
                ComboBoxType.SelectedIndex = 0;
                TextBoxValue.IsEnabled = true;
            };

            Storyboard.SetTargetName(Anim, PanelAddKey.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

            TextBoxKey.BorderBrush = new SolidColorBrush(Colors.Gainsboro);
        }


    }



}
