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
        bool IsCpu { get; set; }

        public WarPlayer(String name, bool isCpu)
        {
            Name = name;
            IsCpu = isCpu;
            Hand = new List<Card>();
        }
    }
}
