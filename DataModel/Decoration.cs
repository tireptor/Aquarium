using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public abstract class Decoration
    {
        private int positionX;
        private int positionY;
        private string picturePath;
        private Aquarium myAquarium;

        public Decoration(int randomX, int randomY, Aquarium myAquarium)
        {
            this.MyAquarium = myAquarium;
            this.positionX = randomX;
            this.positionY = randomY;
        }

        public string PicturePath { get => picturePath; protected set => picturePath = value; }
        public int PositionX { get => positionX; protected set => positionX = value; }
        public int PositionY { get => positionY; protected set => positionY = value; }
        public Aquarium MyAquarium { get => myAquarium; set => myAquarium = value; }


    }
}
