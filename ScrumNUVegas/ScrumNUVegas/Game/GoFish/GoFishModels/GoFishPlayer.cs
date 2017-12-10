using GameHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumNUVegas.Game.GoFish.GoFishModels
{
    [Serializable]
    public class GoFishPlayer : Player, INotifyPropertyChanged
    {
        public int Score { get; set; }
        
        public GoFishPlayer()
        {
        }

        public void FieldChanged(string field = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(field));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
