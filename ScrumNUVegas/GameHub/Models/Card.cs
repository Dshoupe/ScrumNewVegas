using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameHub.Models
{
    public class Card
    {
        public Face FaceValue { get; set; }
        public CardColor Color { get; set; }
        public Suit Suit { get; set; }
        public Brush CardImage { get; set; }
        public bool IsFaceDown { get; set; }

        public Card(Face faceValue, CardColor color, Suit suit, Brush brush)
        {
            FaceValue = faceValue;
            Color = color;
            Suit = suit;
            CardImage = brush;
            IsFaceDown = false;
        }

        public Card(Face faceValue, CardColor color, Suit suit)
        {
            FaceValue = faceValue;
            Color = color;
            Suit = suit;
            IsFaceDown = false;
        }

        public override string ToString()
        {
            return $"{Color} - {Suit} - {FaceValue}";
        }
    }
}