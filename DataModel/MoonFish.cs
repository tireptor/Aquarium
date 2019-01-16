using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public sealed class MoonFish : Fish
    {
        public MoonFish(int positionX, int positionY, Aquarium myAquarium) : base(positionX, positionY, myAquarium)
        {
            this.SpeedX = 2;
            this.SpeedY = 2;
        }
    }
}
