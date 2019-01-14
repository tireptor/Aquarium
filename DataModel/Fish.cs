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
        private int randomX;
        private int randomY;
        private int randomNumberTurnOfDirection = 0;
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

        public void Deplacement()
        {
            if (randomNumberTurnOfDirection == 0)
            {
                randomX = RandDeplacement.Next(2); //Génère un entier compris entre 0 et 1
                randomY = RandDeplacement.Next(2); //Génère un entier compris entre 0 et 1
                randomNumberTurnOfDirection = RandDeplacement.Next(1,200);
            }
            // A chaque tour on décrémente le nombre de fois que le poisson doit prendre cette direction.
            randomNumberTurnOfDirection--;
            int positionXTested;
            int positionYTested;

            if (randomX == 1)
            { positionXTested = positionX + 1; }
            else { positionXTested = positionX - 1; }

            if (randomY == 1)
            { positionYTested = positionY + 1; }
            else { positionYTested = positionY - 1; }

            if (positionXTested == MyAquarium.Width)
            {
                positionXTested -= 1;
                // Si on fonce dans un bord de l'aquarium on réinitialise la direction du poisson.
                randomNumberTurnOfDirection = 0;
            }
            if (positionYTested == MyAquarium.Height)
            {
                positionYTested -= 1;
                randomNumberTurnOfDirection = 0;
            }
            if (positionXTested == 0)
            {
                positionXTested += 1;
                randomNumberTurnOfDirection = 0;
            }
            if (positionYTested == 0)
            {
                positionYTested += 1;
                randomNumberTurnOfDirection = 0;
            }
            positionX = positionXTested;
            positionY = positionYTested;
        }
    }
}
