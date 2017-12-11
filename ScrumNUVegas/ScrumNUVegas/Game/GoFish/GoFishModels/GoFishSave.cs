using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumNUVegas.Game.GoFish.GoFishModels
{
    [Serializable]
    public class GoFishSave
    {
        public Dealer Dealer { get; set; }
        public int TurnOrder { get; set; }
        public int TotalCardCount { get; set; }
        public List<GoFishPlayer> Players { get; set; }
    }
}