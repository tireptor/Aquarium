using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public sealed class CatFish : Fish
    {
        public CatFish(int positionX, int positionY, Aquarium myAquarium) : base(positionX, positionY, myAquarium)
        {
            this.SpeedX = 3;
            this.SpeedY = 3;
        }
    }
}
