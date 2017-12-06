using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameHub.Models;

namespace ScrumNUVegas.Game.War.Models
{
    public class WarPlayer : Player
    {
        public WarPlayer(String name, bool isCpu, List<Card> hand)
        {
            name = this.Name;
            isCpu = this.isCpu;
            hand = this.Hand;
        }
        bool isCpu { get; set; }
    }
}
