using GameHub.Models;
using ScrumNUVegas.Game.GoFish.GoFishModels;
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

namespace ScrumNUVegas.Game.GoFish
{
    /// <summary>
    /// Interaction logic for GoFishControl.xaml
    /// </summary>
    public partial class GoFishControl : UserControl
    {
        Dealer dealer;
        public GoFishControl()
        {
            InitializeComponent();
        }

        private void MainMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            SetUpPanel.Visibility = Visibility.Hidden;

            dealer = new Dealer();
            PlayerField.Children.Clear();
            Player[] players = CreatePlayers(PlayersSlider.Value);
            int count = 0;
            foreach (Player p in players)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;

                Thickness margin = sp.Margin;
                margin.Top = 20;
                sp.Margin = margin;

                p.Hand = dealer.DealFiveCards();
                Label l = new Label();
                margin = l.Margin;
                margin.Top = 10;
                margin.Bottom = 10;
                l.Margin = margin;
                l.Width = 100;
                TextBox t = (TextBox)PlayerSelectionLabels.Children[count++];
                l.Content = t.Text;

                sp.Children.Add(l);
                
                for (int i = 0; i < p.Hand.Count(); i++)
                {
                    //REALLY weird issue, had to access the images at runtime so for GoFish, the images will be held in the debug folder -- https://stackoverflow.com/questions/569561/dynamic-loading-of-images-in-wpf
                    Label label = new Label();
                    Image img = new Image();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.UriSource = p.Hand[i].CardUri;
                    bitmapImage.EndInit();
                    img.Source = bitmapImage;
                    label.Content = img;
                    margin = label.Margin;
                    margin.Right = 10;
                    margin.Left = 10;
                    label.Margin = margin;
                    label.Width = 50;
                    sp.Children.Add(label);
                }
                PlayerField.Children.Add(sp);
            }
        }

        private Player[] CreatePlayers(double value)
        {
            Player[] players = new Player[(int)value];
            for (int i = 0; i < value; i++)
            {
                players[i] = new GoFishPlayer();
            }
            return players;
        }

        private void PlayersSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            PlayerSelectionLabels.Children.Clear();
            for (int i = 0; i < PlayersSlider.Value; i++)
            {
                TextBox t = new TextBox();
                t.Name = $"Player{i + 1}";
                t.Text = t.Name;
                t.Width = 50;
                Thickness margin = t.Margin;
                margin.Top = 10;
                t.Margin = margin;
                PlayerSelectionLabels.Children.Add(t);
            }
        }
    }
}
