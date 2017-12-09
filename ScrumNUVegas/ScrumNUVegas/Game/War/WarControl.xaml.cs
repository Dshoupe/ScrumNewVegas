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
        private bool player1Win;
        private bool player2Win;
        private Deck deck;
        private List<Card> warBattleCards = new List<Card>();

        public WarControl()
        {
            InitializeComponent();
            deck = new Deck();


            player1 = new WarPlayer("DYLAN", false);
            player2 = new WarPlayer("TRE", true);

            DeckManager();
            player1DeckCount.Content = player1.Hand.Count.ToString();
            player2DeckCount.Content = player2.Hand.Count.ToString();
        }


        public void FeatureSelection()
        {


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
                        if(warCount >= 2)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Card1.Source = player1.Hand.First().CardImage;
            Card2.Source = player2.Hand.First().CardImage;
            WarBattle(player1.Hand.IndexOf(player1.Hand.First()), player2.Hand.IndexOf(player2.Hand.First()));
            checkWinner();
            if(player1Win == true)
            {
                MessageBox.Show(player1.Name + " has Won the War!!!!");
            }
            else if(player2Win == true)
            {
                MessageBox.Show(player1.Name + " has Won the War!!!!");
            }
            else
            {
                player1.Hand.Add(player1.Hand.First());
                player1.Hand.Remove(player1.Hand.First());
                player2.Hand.Add(player2.Hand.First());
                player2.Hand.Remove(player2.Hand.First());
                player1DeckCount.Content = player1.Hand.Count.ToString();
                player2DeckCount.Content = player2.Hand.Count.ToString();
            }
          
            
        }
    }
}