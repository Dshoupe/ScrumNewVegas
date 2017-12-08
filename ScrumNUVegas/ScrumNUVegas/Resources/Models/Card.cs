using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameHub.Models
{
    [Serializable]
    public class Card
    {
        public Face FaceValue { get; set; }
        public CardColor Color { get; set; }
        public Suit Suit { get; set; }
        public ImageSource CardImage { get; set; }
        public Uri CardUri { get; set; }
        public bool IsFaceDown { get; set; }

        public Card(Face faceValue, CardColor color, Suit suit, ImageSource brush, Uri uriResource)
        {
            FaceValue = faceValue;
            Color = color;
            Suit = suit;
            CardImage = brush;
            IsFaceDown = false;
            CardUri = uriResource;
        }

        public Card(Face faceValue, CardColor color, Suit suit)
        {
            FaceValue = faceValue;
            Color = color;
            Suit = suit;
            IsFaceDown = false;
        }

        public void Flip()
        {
            Uri resourceUri = null;
            if (!IsFaceDown)
            {
                resourceUri = new Uri($"Resources/Images/CardImages/CardBacks/Red_back.jpg", UriKind.Relative);
                IsFaceDown = true;
            }
            else
            {
                if (Suit == Suit.Spades)
                {
                    resourceUri = new Uri($"Resources/Images/CardImages/{(int)FaceValue + 1}S.jpg", UriKind.Relative);
                }
                else if (Suit == Suit.Clubs)
                {
                    resourceUri = new Uri($"Resources/Images/CardImages/{((int)FaceValue % 13) + 1}C.jpg", UriKind.Relative);
                }
                else if (Suit == Suit.Hearts)
                {
                    resourceUri = new Uri($"Resources/Images/CardImages/{((int)FaceValue % 13) + 1}H.jpg", UriKind.Relative);
                }
                else
                {
                    resourceUri = new Uri($"Resources/Images/CardImages/{((int)FaceValue % 13) + 1}D.jpg", UriKind.Relative);
                }
                IsFaceDown = false;
            }
            CardUri = resourceUri;
            CardImage = new BitmapImage(resourceUri);
        }

        public override string ToString()
        {
            return $"{Color} - {Suit} - {FaceValue}";
        }
    }
}