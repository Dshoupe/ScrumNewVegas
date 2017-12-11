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
        private bool player1Win = false;
        private bool player2Win = false;
        private Deck deck;
        private List<Card> warBattleCards = new List<Card>();
        private Grid grid;
        public WarControl()
        {
            InitializeComponent();
            grid = GameArea;
            WarTheme.Source = new Uri("Music/warTheme.mp3", UriKind.Relative);
            this.Loaded += new RoutedEventHandler(WarControl_Loaded);


        }


        public void WarControl_Loaded(object sender, RoutedEventArgs e)
        {
            WarTheme.Play();           

        }

        public void checkWinner()
        {
            if(player1.Hand.Count <= 3 )
            {
                player1Win = true;
            }
            else if(player2.Hand.Count <= 3)
            {
                player2Win = true;
            }
          

        }
        public void DeckManager()
        {
            deck.Shuffle();
            for (int x = 0; x < deck.Cards.Count / 2; x++)
            {
                player1.Hand.Add(deck.Cards[x]);
            }
            for (int x = 26; x < deck.Cards.Count; x++)
            {
                player2.Hand.Add(deck.Cards[x]);
            }
        }

      

      
        public void WarBattle(int player1Card, int player2Card)
        {

            int i = player1Card;
            int k = player2Card;
            int count = 0;
            bool keepGoing = true;
            if (player1.Hand[i].FaceValue > player2.Hand[k].FaceValue)
            {
                player1.Hand.Add(player2.Hand[k]);
                player2.Hand.Remove(player2.Hand[k]);
               

            }
            else if (player2.Hand[k].FaceValue > player1.Hand[i].FaceValue)
            {
                player2.Hand.Add(player1.Hand[i]);
                player1.Hand.Remove(player1.Hand[i]);
              

            }
            else
            {
                int warCount = 1;
                MessageBox.Show("There is War Baby!!!!!");
                
                while (keepGoing)
                {
                    

                    i = player1.Hand.IndexOf(player1.Hand.First());
                    k = player2.Hand.IndexOf(player2.Hand.First());
                    if (player1.Hand[i].FaceValue > player2.Hand[k].FaceValue || player2.Hand.Count == 3)
                    {
                        keepGoing = false;
                        for (count = 0; count < warBattleCards.Count; count++)
                        {
                            player1.Hand.Add(warBattleCards[count]);
                        }
                        warBattleCards.Clear();
                    }
                    else if (player2.Hand[k].FaceValue > player1.Hand[i].FaceValue || player1.Hand.Count == 3)
                    {
                        keepGoing = false;
                        for (count = 0; count < warBattleCards.Count; count++)
                        {
                            player2.Hand.Add(warBattleCards[count]);
                        }
                        warBattleCards.Clear();
                    }
                    else
                    {
                        Player1WarArea.Visibility = Visibility.Visible;
                        Player2WarArea.Visibility = Visibility.Visible;

                        if (warCount >= 2)
                        {
                            MessageBox.Show("There is ANOTHER War Baby!!!!!");
                        }
                        for (count = 0; count < 3; count++)
                        {
                            warBattleCards.Add(player1.Hand[count]);
                            warBattleCards.Add(player2.Hand[count]);
                            player1.Hand.Remove(player1.Hand[count]);
                            player2.Hand.Remove(player2.Hand[count]);
                        }
                        warCount++;
                    }

                }


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
            for (int i = 0; i < player1.Hand.Count(); i++)
            {
                player1.Hand[i].LoadCard();
            }
            for (int i = 0; i < player2.Hand.Count(); i++)
            {
                player2.Hand[i].LoadCard();
            }
            deck = newGame.Deck;
            Player1Details.Content = player1.ToString();
            Player2Details.Content = player2.ToString();
            GameModeSelection.Visibility = Visibility.Hidden;
            GameArea.Visibility = Visibility.Visible;
            Uri resourceUri = new Uri($"{player1.Hand[0].CardImage}", UriKind.Relative);
            Player1CurrentCard.Source = new BitmapImage(resourceUri);
            resourceUri = new Uri($"{player2.Hand[0].CardImage}", UriKind.Relative);
            Player2CurrentCard.Source = new BitmapImage(resourceUri);

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
            player1 = new WarPlayer(Player1Name.Text, false);
            if ((bool)AICheckBox.IsChecked)
            {
                player2 = new WarPlayer("Computer", true);
            }
            else
            {
                player2 = new WarPlayer(Player2Name.Text, false);
            }
            deck = new Deck();
            deck.Shuffle();
            DeckManager();
            Player1Details.Content = player1.ToString();
            Player2Details.Content = player2.ToString();
        }

        public void resetGame()
        {
            GameArea.Visibility = Visibility.Hidden;
            player1 = null;
            player2 = null;
            player1Win = false;
            player2Win = false;
            warBattleCards.Clear();
            deck = new Deck();
            GameArea = grid;
            GameModeSelection.Visibility = Visibility.Visible;

        }
        private async void TurnBtn_Click(object sender, RoutedEventArgs e)
        {
            if (player1Win == true)
            {
                MessageBox.Show(player1.Name + " has won!!!!!!!!!!");
                resetGame();
            }
            else if (player2Win == true)
            {
                MessageBox.Show(player2.Name + " has won!!!!!!!!!!");
                resetGame();
            }
            else
            {
                Uri resourceUri = new Uri($"{player1.Hand[0].CardImage}", UriKind.Relative);
                Player1CurrentCard.Source = new BitmapImage(resourceUri);
                resourceUri = new Uri($"{player2.Hand[0].CardImage}", UriKind.Relative);
                Player2CurrentCard.Source = new BitmapImage(resourceUri);
                WarBattle(player1.Hand.IndexOf(player1.Hand.First()), player2.Hand.IndexOf(player2.Hand.First()));
                player1.Hand.Add(player1.Hand.First());
                player1.Hand.Remove(player1.Hand.First());
                player2.Hand.Add(player2.Hand.First());
                player2.Hand.Remove(player2.Hand.First());
                Player1Details.Content = player1.ToString();
                Player2Details.Content = player2.ToString();
                await Task.Delay(3000);
                Player1WarArea.Visibility = Visibility.Hidden;
                Player2WarArea.Visibility = Visibility.Hidden;
                checkWinner();
            }
        }

        private void warTheme_MediaOpened(object sender, RoutedEventArgs e)
        {
            MediaElement me = (MediaElement)sender;
            me.Play();
        }

        private void WarTheme_MediaEnded(object sender, RoutedEventArgs e)
        {

        }
    }
}
