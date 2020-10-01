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

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Colors.Gainsboro);
            BTNEdit.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = new SolidColorBrush(Colors.WhiteSmoke);
            BTNEdit.Visibility = Visibility.Collapsed;
        }


        private void OpenEdit(object sender, RoutedEventArgs e)
        {
            _Parent.Children.Add(new EditPlayer(DetailPlayer, Refreshlist));

        }

        private void CopyToken(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(TextToken.Text);
            MessageBox.Show("Token Copied !");

        }


        Action<object, RoutedEventArgs> Refreshlist;

    }
}
