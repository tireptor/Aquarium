using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public abstract class Fish
    {
        private int positionX;
        private int positionY;
        private int speedX;
        private int speedY;
        private int randomX;
        private int randomY;
        private int randomNumberTurnOfDirectionX = 0;
        private int randomNumberTurnOfDirectionY = 0;
        private Aquarium myAquarium;

        Random RandDeplacement = new Random();

        public Fish(int positionX, int positionY, Aquarium myAquarium )
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.MyAquarium = myAquarium;
        }

        public int PositionX { get => positionX; protected set => positionX = value; }
        public int PositionY { get => positionY; protected set => positionY = value; }
        public int SpeedX { get => speedX; protected set => speedX = value; }
        public int SpeedY { get => speedY; protected set => speedY = value; }
        public Aquarium MyAquarium { get => myAquarium; protected set => myAquarium = value; }

        public void Deplacement()
        {
            int positionXTested;
            int positionYTested;
            int signe = 1;
            if (randomNumberTurnOfDirectionY == 0)
            {
                randomY = RandDeplacement.Next(2); //Génère un entier compris entre 0 et 1
                randomNumberTurnOfDirectionY = RandDeplacement.Next(1, 300);
            }
            if (randomNumberTurnOfDirectionX == 0)
            {
                randomX = RandDeplacement.Next(2); //Génère un entier compris entre 0 et 1
                randomNumberTurnOfDirectionX = RandDeplacement.Next(1,300);
            }
            // A chaque tour on décrémente le nombre de fois que le poisson doit prendre cette direction.
            randomNumberTurnOfDirectionX--;
            randomNumberTurnOfDirectionY--;


            if (randomX == 0)
            { signe = -1; }
            positionXTested = positionX + SpeedX * signe;

            signe =  1;

            if (randomY == 0)
            { signe = -1; }
            positionYTested = positionY + SpeedY * signe;

            if (positionXTested >= this.MyAquarium.Width)
            {
                positionXTested -= SpeedX;
                // Si on fonce dans un bord de l'aquarium on réinitialise la direction du poisson.
                randomNumberTurnOfDirectionX = 0;
            }
            if (positionYTested >= this.MyAquarium.Height)
            {
                positionYTested -= SpeedY;
                randomNumberTurnOfDirectionY = 0;
            }
            if (positionXTested <= 0)
            {
                positionXTested += SpeedX;
                randomNumberTurnOfDirectionX = 0;
            }
            if (positionYTested <= 0)
            {
                positionYTested += SpeedY;
                randomNumberTurnOfDirectionY = 0;
            }
            positionX = positionXTested;
            positionY = positionYTested;
        }
    }
}
