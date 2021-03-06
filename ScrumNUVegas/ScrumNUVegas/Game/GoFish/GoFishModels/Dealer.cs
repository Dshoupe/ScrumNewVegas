﻿using GameHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumNUVegas.Game.GoFish.GoFishModels
{
    [Serializable]
    public class Dealer
    {
        public Deck dealerDeck;
        public Dealer()
        {
            dealerDeck = new Deck();
            dealerDeck.Shuffle();
        }

        public List<Card> DealFiveCards()
        {
            List<Card> newHand = new List<Card>();
            for (int i = 0; i < 5 && dealerDeck.Cards.Count > 0; i++)
            {
                newHand.Add(dealerDeck.Cards.Last());
                dealerDeck.Cards.Remove(dealerDeck.Cards.Last());
            }
            return newHand;
        }

        public Card dealOneCard()
        {
            if (RemainingCards()>0)
            {
                Card temp = dealerDeck.Cards.Last();
                dealerDeck.Cards.Remove(dealerDeck.Cards.Last());
                return temp;
            }
            else
            {
                return null;
            }
        }

        public int RemainingCards()
        {
            return dealerDeck.Cards.Count();
        }
    }
}
