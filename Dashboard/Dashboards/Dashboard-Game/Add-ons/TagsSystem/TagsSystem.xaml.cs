using Dashboard.Dashboards.Dashboard_Game.Add_ons.TagsSystem.Elements;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using RestSharp;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.Add_ons.TagsSystem
{
    /// <summary>
    /// Interaction logic for TagsSystem.xaml
    /// </summary>
    public partial class TagsSystem : UserControl, IControlTag
    {
        BsonArray List;

        public TagsSystem(BsonArray ListTag)
        {
            List = ListTag;
            DashboardGame.Dashboard.Root.Children.Add(this);

            InitializeComponent();

            foreach (var item in ListTag)
            {
                PlaceTags.Children.Add(new ModelTag(item.AsBsonDocument, this));
            }


            Root.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    Close();
                }
            };

            //action add tag to array
            BTN_AddTag.MouseDown += (s, e) =>
            {
                var NewTag = new BsonDocument
                {
                    {"Name",TextNewTag.Text },
                    {"Color",SelectedColor.Background.ToString() }
                };

                if (!ListTag.Contains(NewTag))
                {
                    PlaceTags.Children.Add(new ModelTag(NewTag, this));
                    ListTag.Add(NewTag);

                    TextNewTag.Text = "";
                }
                else
                {
                    DashboardGame.Notifaction("Tag Duplicated", Notifaction.StatusMessage.Warrning);
                }
            };

        }

        public void Delete(BsonValue value ,UserControl Element)
        {
            List.Remove(value);
            PlaceTags.Children.Remove(Element);

        }

        void Close()
        {
            try
            {
                DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
                Anim.Completed += (s, e) =>
                {
                    DashboardGame.Dashboard.Root.Children.Remove(this);
                };

                Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
                Storyboard.SetTargetName(Anim, Content.Name);
                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(Anim);
                storyboard.Begin(this);

            }
            catch (Exception)
            {
                DashboardGame.Dashboard.Root.Children.Remove(this);
            }
        }
    }

    public interface IControlTag
    {
        void Delete(BsonValue value, UserControl Element);

    }
}
