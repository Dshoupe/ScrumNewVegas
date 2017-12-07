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
        public GoFishControl()
        {
            InitializeComponent();
        }

        private void MainMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            Player[] player = CreatePlayers(PlayersSlider.Value);

            //PlayerField.Children.Add();
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
