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
            dealer = new Dealer();
            PlayerField.Children.Clear();
            Player[] players = CreatePlayers(PlayersSlider.Value);
            int count = 1;
            foreach (Player p in players)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;

                Thickness margin = sp.Margin;
                margin.Top = 20;
                sp.Margin = margin;

                p.Hand = dealer.DealFiveCards();
                TextBox l = new TextBox();
                margin = l.Margin;
                margin.Top = 10;
                margin.Bottom = 10;
                l.Margin = margin;
                l.Width = 100;
                l.Text = $"Player {count++}";

                sp.Children.Add(l);

                for (int i = 0; i < p.Hand.Count(); i++)
                {
                    Label label = new Label();
                    Image img = new Image();
                    img.Source = p.Hand[i].CardImage;
                    label.Background = new SolidColorBrush(Colors.Black);
                    label.Content = img;
                    margin = label.Margin;
                    margin.Right = 10;
                    margin.Left = 10;
                    label.Margin = margin;
                    label.Width = 30;
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
        
    }
}
