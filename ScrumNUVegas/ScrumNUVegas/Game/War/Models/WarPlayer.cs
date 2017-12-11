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
        public static int playerCount = 1;
        public WarPlayer(String name, bool isCPU)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                Name = "Player " + playerCount++;
            }
            else
            {
                Name = name;
            }
            IsCPU = isCPU;
            Hand = new List<Card>();
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
