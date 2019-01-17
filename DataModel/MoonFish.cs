using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public sealed class MoonFish : Fish
    {// comment
        public MoonFish(int positionX, int positionY, Aquarium myAquarium, bool isAnEgg = false) : base(positionX, positionY, myAquarium, isAnEgg)
        {
            this.SpeedX = 2;
            this.SpeedY = 2;
            this.TimeBeforeHatching = 700;
        }
    }
}
