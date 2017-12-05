using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHub.Models
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
            CardColor cardColor = CardColor.Black;
            for (int i = 0; i < 52; i++)
            {
                if (i >= 26)
                {
                    cardColor = CardColor.Red;
                }
                Cards.Add(new Card((Face)(i % 13), cardColor, (Suit)(i/13)));
            }
        }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            foreach (var card in Cards)
            {
                retVal.Append($"{card.ToString()} || ");
            }
            return retVal.ToString();
        }
    }
}