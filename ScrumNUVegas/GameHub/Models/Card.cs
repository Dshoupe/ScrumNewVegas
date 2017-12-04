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

        public Card(Face faceValue, CardColor color, Suit suit, Brush brush)
        {
            FaceValue = faceValue;
            Color = color;
            Suit = suit;
            CardImage = brush;
        }

        public Card(Face faceValue, CardColor color, Suit suit)
        {
            FaceValue = faceValue;
            Color = color;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"{Color} - {Suit} - {FaceValue}";
        }
    }
}