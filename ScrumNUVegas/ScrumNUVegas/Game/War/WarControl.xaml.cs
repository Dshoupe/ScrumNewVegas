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
        private Card card;

        public WarControl()
        {
            InitializeComponent();
            deck = new Deck();
        }

        public void FeatureSelection()
        {

            
        }
       
        public void DeckManager()
        {
            for(int x = 0; x < deck.Cards.Count / 2; x++)
            {
                player1.Hand.Add(deck.Cards[x]);
            }
            for(int x = 26; x < deck.Cards.Count; x++)
            {
                player1.Hand.Add(deck.Cards[x]);
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
    }
}