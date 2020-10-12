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

namespace Dashboard.Dashboards.Dashboard_Game.PagePlayer.Elements
{
    public partial class ModelAbstractUser : UserControl
    {

        BsonDocument DetailPlayer;
        Grid _Parent;

        public ModelAbstractUser(BsonDocument DetailPlayer, Action<object, RoutedEventArgs> Refreshlist, Grid Parent)
        {
            InitializeComponent();
            TextToken.Text = DetailPlayer["Account"]["Token"].AsObjectId.ToString();
            TextLastLogin.Text = DetailPlayer["Account"]["LastLogin"].ToUniversalTime().ToString();
            TextCreated.Text = DetailPlayer["Account"]["Created"].ToUniversalTime().ToString();
            TextCountry.Text = DetailPlayer["Account"]["Country"].AsString;
            TextUsername.Text = DetailPlayer["Account"]["Username"].AsString;


            this.DetailPlayer = DetailPlayer;
            this.Refreshlist = Refreshlist;
            _Parent = Parent;
        }


        /// <summary>
        /// non refresh list
        /// </summary>
        /// <param name="DetailPlayer"></param>
        public ModelAbstractUser(BsonDocument DetailPlayer)
        {
            InitializeComponent();
            TextToken.Text = DetailPlayer["Account"]["Token"].AsObjectId.ToString();
            TextLastLogin.Text = DetailPlayer["Account"]["LastLogin"].ToUniversalTime().ToString();
            TextCreated.Text = DetailPlayer["Account"]["Created"].ToUniversalTime().ToString();
            TextCountry.Text = DetailPlayer["Account"]["Country"].AsString;
            TextUsername.Text = DetailPlayer["Account"]["Username"].AsString;

            this.DetailPlayer = DetailPlayer;
        }

    
     
        private void OpenEdit(object sender, MouseButtonEventArgs e)
        {
           DashboardGame.Dashboard.Root.Children.Add(new EditPlayer(DetailPlayer, Refreshlist));

        }

        private void CopyToken(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(TextToken.Text);
            DashboardGame.Notifaction("Token Copied !",Notifaction.StatusMessage.Ok);

        }


        Action<object, RoutedEventArgs> Refreshlist;

    }
}
