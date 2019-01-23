using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public sealed class CatFish : Fish
    {
        public CatFish(int positionX, int positionY, Aquarium myAquarium, bool isAnEgg = false) : base(positionX, positionY, myAquarium, isAnEgg)
        {
            this.SpeedX = 3;
            this.SpeedY = 3;
            this.Size = 50;
            this.SizeEgg = 20;
            this.TimeBeforeHatching = 900;
        }
    }
}
