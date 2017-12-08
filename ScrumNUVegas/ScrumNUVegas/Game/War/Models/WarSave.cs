using GameHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumNUVegas.Game.War.Models
{
    public class WarSave
    {
        public List<WarPlayer> Players { get; set; }
        public Deck Deck { get; set; }
    }
}
