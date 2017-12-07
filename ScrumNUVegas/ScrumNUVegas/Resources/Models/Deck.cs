using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameHub.Models
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
            CardColor cardColor = CardColor.Black;
            Uri resourceUri = null;
            for (int i = 0; i < 52; i++)
            {
                if (i <= 12)
                {
                    resourceUri = new Uri($"Resources/Images/CardImages/{i + 1}S.jpg", UriKind.Relative);
                }
                else if (i >= 13 && i <= 26)
                {
                    resourceUri = new Uri($"Resources/Images/CardImages/{(i % 13) + 1}C.jpg", UriKind.Relative);
                }
                else if (i >= 27 && i <= 39)
                {
                    resourceUri = new Uri($"Resources/Images/CardImages/{(i % 13) + 1}H.jpg", UriKind.Relative);
                }
                else
                {
                    resourceUri = new Uri($"Resources/Images/CardImages/{(i % 13) + 1}D.jpg", UriKind.Relative);
                }
                if (i >= 26)
                {
                    cardColor = CardColor.Red;
                }
                Cards.Add(new Card((Face)(i % 13), cardColor, (Suit)(i / 13), new BitmapImage(resourceUri)));
            }
        }

        public void Shuffle()
        {
            Random rand = new Random();
            for (int i = 0; i < Cards.Count; i++)
            {
                int randNum = rand.Next(0, Cards.Count() - 1);
                Card temp = Cards[randNum];
                Cards[randNum] = Cards[i];
                Cards[i] = temp;
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