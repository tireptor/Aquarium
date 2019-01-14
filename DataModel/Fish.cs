using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Fish
    {
        private int positionX;
        private int positionY;
        private int speedX;
        private int speedY;
        private Aquarium myAquarium;

        Random RandDeplacement = new Random();

        public Fish(int positionX, int positionY, Aquarium myAquarium )
        {
            PositionX = positionX;
            PositionY = positionY;
            MyAquarium = myAquarium;
        }

        public int PositionX { get => positionX; private set => positionX = value; }
        public int PositionY { get => positionY; private set => positionY = value; }
        public int SpeedX { get => speedX; private set => speedX = value; }
        public int SpeedY { get => speedY; private set => speedY = value; }
        public Aquarium MyAquarium { get => myAquarium; set => myAquarium = value; }

        protected void Deplacement(int width, int height)
        {
            int randomX = RandDeplacement.Next(1); //Génère un entier compris entre 0 et 1
            int randomY = RandDeplacement.Next(1); //Génère un entier compris entre 0 et 1

            int positionXTested;
            int positionYTested;

            if (randomX == 1)
            { positionXTested = positionX + 1; }
            else { positionXTested = positionX - 1; }

            if (randomY == 1)
            { positionYTested = positionY + 1; }
            else { positionYTested = positionY - 1; }

            if (positionXTested == width)
            {
                    positionXTested -= 1;
            }
            if (positionYTested == height)
            {
                positionYTested -= 1;
            }
            if (positionXTested == 0)
            {
                positionXTested += 1;
            }
            if (positionYTested == 0)
            {
                positionYTested += 1;
            }
            positionX = positionXTested;
            positionY = positionYTested;
        }
    }
}
