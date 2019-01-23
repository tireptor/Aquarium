using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    sealed public class GoldFish : Fish
    {
        public GoldFish(int positionX, int positionY, Aquarium myAquarium, bool isAnEgg = false) : base(positionX, positionY, myAquarium, isAnEgg)
        {
            this.SpeedX = 1;
            this.SpeedY = 1;
            this.Size = 40;
            this.SizeEgg = 10;
            this.TimeBeforeHatching = 600;
        }
    }
}
