using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public sealed class Shell : Decoration
    {
        public Shell(int randomX, int randomY, Aquarium myAquarium) : base(randomX, randomY, myAquarium)
        {
            this.PicturePath = "";
        }
    }
}
