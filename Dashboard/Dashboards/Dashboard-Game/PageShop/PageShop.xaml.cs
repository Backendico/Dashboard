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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.Dashboards.Dashboard_Game.PageShop
{
    public partial class PageShop : UserControl
    {
        BsonDocument Detail = new BsonDocument
        {
            {"Name","" },
            {"Description", ""},
            {"MarketLink","" },
            {"AvatarLink","" },
            {"Tags",new BsonArray() },
            {"Products" ,new BsonArray()},
        };

        public PageShop()
        {
            InitializeComponent();


            //acaton add Store
            BTNaddStore.MouseDown += (s, e) =>
            {
                try
                {

                    if (TextboxName.Text.Length <= 5)
                        throw new Exception("Store name Short");

                    if (TextBoxMarketLink.Text.Length >= 1)
                    {
                        try
                        {
                            new Uri(TextBoxMarketLink.Text);

                        }
                        catch (Exception)
                        {
                            DashboardGame.Notifaction("The \" Market link \" is incorrect", Notifaction.StatusMessage.Error);
                        }

                    }


                    if (TextBoxAvatar.Text.Length >= 1)
                    {
                        try
                        {
                            new Uri(TextBoxAvatar.Text);

                        }
                        catch (Exception ex)
                        {
                            DashboardGame.Notifaction("The \" Avatar link \" is incorrect", Notifaction.StatusMessage.Error);
                        }

                    }


                }
                catch (Exception ex)
                {

                    DashboardGame.Notifaction(ex.Message, Notifaction.StatusMessage.Error);
                }

            };
        }
    }
}
