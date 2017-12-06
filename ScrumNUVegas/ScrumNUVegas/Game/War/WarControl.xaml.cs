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

namespace ScrumNUVegas.Game.War
{
    /// <summary>
    /// Interaction logic for WarControl.xaml
    /// </summary>
    public partial class WarControl : UserControl
    {

        private Player player1;
        private Player player2;
        private Deck deck;
        private Card card;
        public WarControl()
        {
            InitializeComponent();
        }

        public void featureSelection()
        {

            
        }
       
        public void deckManager()
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
        public void saveGame()
        {
            
        }
        public void loadGame()
        {

        }
        public void warBattle()
        {
            while(player1.Hand.Count != 0 && player2.Hand.Count !=0)
            {
                for(int x = 0; )
                if (player1.Hand[i].FaceValue)
            }
        }
    }
}
