using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public sealed class Algae : Decoration
    {
        // public Algae(Aquarium myAquarium) : base(myAquarium)
        // {

        ////     this.MyAquarium = myAquarium;
        //     this.PicturePath = "";
        // }
        public Algae(int randomX, int randomY, Aquarium myAquarium) : base(randomX, randomY, myAquarium)
        {
            this.MyAquarium = myAquarium;
            this.PicturePath = @"C:\Users\Vincent\Documents\algues.png";
        }
    }
}