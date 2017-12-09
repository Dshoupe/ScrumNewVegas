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
using GameHub.Models;
using ScrumNUVegas.Game.War.Models;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ScrumNUVegas.Game.War
{
    /// <summary>
    /// Interaction logic for WarControl.xaml
    /// </summary>
    public partial class WarControl : UserControl
    {
        private WarPlayer player1;
        private WarPlayer player2;
        private Deck deck;

        public WarControl()
        {
            InitializeComponent();
        }
       
        public void DeckManager()
        {
            for(int x = 0; x < deck.Cards.Count / 2; x++)
            {
                player1.Hand.Add(deck.Cards[x]);
            }
            for(int x = 26; x < deck.Cards.Count; x++)
            {
                player2.Hand.Add(deck.Cards[x]);
            }
        }

        public void SaveGame()
        {
            WarSave warSave = new WarSave() { Deck = deck, Players = new List<WarPlayer>() { player1, player2 } };
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".war";
            sfd.FileName = "War.war";
            sfd.Filter = "War Game Saves (*.war)|*.war";
            if (sfd.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(sfd.FileName, FileMode.OpenOrCreate))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, warSave);
                }
            }
        }

        public void LoadGameWindow()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".war";
            ofd.Filter = "War Game Saves (*.war)|*.war";
            WarSave newGame = null;
            if (ofd.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(ofd.FileName, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    newGame = (WarSave)bf.Deserialize(stream);
                }
                LoadGame(newGame);
            }
        }

        public void LoadGame(WarSave newGame)
        {
            player1 = newGame.Players[0];
            player2 = newGame.Players[1];
            deck = newGame.Deck;
        }

        public void WarBattle()
        {
            while(player1.Hand.Count != 0 && player2.Hand.Count !=0)
            {
                //for(int x = 0; )
                //if (player1.Hand[i].FaceValue)
            }
        }

        private void AICheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Player2Name.Visibility = Visibility.Hidden;
        }

        private void AICheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Player2Name.Visibility = Visibility.Visible;
        }

        private void StartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            GameModeSelection.Visibility = Visibility.Hidden;
            GameArea.Visibility = Visibility.Visible;
            player1 = new WarPlayer(Player1Name.Text, false, new List<Card>());
            if ((bool)AICheckBox.IsChecked)
            {
                player2 = new WarPlayer("Computer", true, new List<Card>());
            }
            else
            {
                player2 = new WarPlayer(Player2Name.Text, false, new List<Card>());
            }
            deck = new Deck();
            deck.Shuffle();
            DeckManager();
            Player1Details.Content = player1.ToString();
            Player2Details.Content = player2.ToString();
        }

        private void TurnBtn_Click(object sender, RoutedEventArgs e)
        {
            Uri resourceUri = new Uri($"{player1.Hand[0].CardImage}", UriKind.Relative);
            Player1CurrentCard.Source = new BitmapImage(resourceUri);
            resourceUri = new Uri($"{player2.Hand[0].CardImage}", UriKind.Relative);
            Player2CurrentCard.Source = new BitmapImage(resourceUri);
            player1.Hand.Add(player1.Hand.First());
            player1.Hand.Remove(player1.Hand.First());
            player2.Hand.Add(player2.Hand.First());
            player2.Hand.Remove(player2.Hand.First());
            Player1Details.Content = player1.ToString();
            Player2Details.Content = player2.ToString();
        }
    }
}