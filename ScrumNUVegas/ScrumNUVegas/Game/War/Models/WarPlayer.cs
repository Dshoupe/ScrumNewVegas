using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameHub.Models;

namespace ScrumNUVegas.Game.War.Models
{
    [Serializable]
    public class WarPlayer : Player
    {
        bool IsCPU { get; set; }

        public WarPlayer(String name, bool isCPU, List<Card> hand)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                Name = "Player";
            }
            else
            {
                Name = name;
            }
            IsCPU = isCPU;
            Hand = hand;
        }

        public override string ToString()
        {
            if (IsCPU)
            {
                return $"{Name} - Card Count: {Hand.Count()} - AI";
            }
            else
            {
                return $"{Name} - Card Count: {Hand.Count()}";
            }
        }
    }
}
