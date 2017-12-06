using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumNUVegas;
using ScrumNUVegas.Properties;
using GameHub.Models;

namespace CardGameSimulator.Blackjack
{
    class BlackjackPlayer : Player
    {

        public List<Card> playerHand { get; set; }
        public List<Card> PlayerHand = new List<Card>();

        public void AddCard(Card newCard)
        {
            PlayerHand.Add(newCard);
        }

        public Card PlayCard()
        {
            throw new NotImplementedException();
        }
    }
}
