using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    sealed public class GoldFish : Fish
    {
        public GoldFish(int positionX, int positionY, Aquarium myAquarium) : base(positionX, positionY, myAquarium)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.MyAquarium = myAquarium;
            this.SpeedX = 1;
            this.SpeedY = 1;
        }
    }
}
