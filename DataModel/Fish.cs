using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public abstract class Fish : INotifyPropertyChanged
    {
        private int positionX;
        private int positionY;
        private int speedX;
        private int speedY;
        private bool isAnEgg;
        private int timeBeforeHatching;
        private int signeX = 1;
        private int signeY = 1;
        protected int randomX;
        protected int randomY;
        protected int randomNumberTurnOfDirectionX = 0;
        protected int randomNumberTurnOfDirectionY = 0;
        private Aquarium myAquarium;

        protected Random RandDeplacement = new Random();

        public event PropertyChangedEventHandler PropertyChanged;

        public Fish(int positionX, int positionY, Aquarium myAquarium, bool isAnEgg = false)
        {
            this.PositionX  = positionX;
            this.PositionY  = positionY;
            this.IsAnEgg    = isAnEgg;
            this.MyAquarium = myAquarium;
        }

        public int PositionX { get => positionX; protected set => positionX = value; }
        public int PositionY { get => positionY; protected set => positionY = value; }
        public int SpeedX { get => speedX; protected set => speedX = value; }
        public int SpeedY { get => speedY; protected set => speedY = value; }
        public Aquarium MyAquarium { get => myAquarium; protected set => myAquarium = value; }
        public bool IsAnEgg { get => isAnEgg; private set => isAnEgg = value; }
        public int TimeBeforeHatching { get => timeBeforeHatching; protected set => timeBeforeHatching = value; }

        public void Lay()
        {
            if (this is GoldFish)
            {
                GoldFish f = new GoldFish(this.PositionX, this.PositionY, MyAquarium, true);
                MyAquarium.Fishs.Add(f);
            }
            if (this is MoonFish)
            {
                MoonFish f = new MoonFish(this.PositionX, this.PositionY, MyAquarium, true);
                MyAquarium.Fishs.Add(f);
            }
            if (this is CatFish)
            {
                CatFish f = new CatFish(this.PositionX, this.PositionY, MyAquarium, true);
                MyAquarium.Fishs.Add(f);
            }

        }

        private void ActionEgg()
        {
            if (TimeBeforeHatching <= 0)
            {
                this.IsAnEgg = false;
                return;
            }
            TimeBeforeHatching--;
            if(this.PositionY < MyAquarium.Height)
            {
                this.PositionY++;
            }
        }

        public virtual void Deplacement()
        {
            int positionXTested;
            int positionYTested;
            int randomLay;
            

            if (this.IsAnEgg)
            {
                ActionEgg();
                return;
            }

            // Un poisson a une chance sur 1000 de pondre un oeuf à chaque déplacement, un requin ne pond pas.
            randomLay = RandDeplacement.Next(500);
            if ((randomLay == 10) && !(this is Shark))
            {
                Lay();
            }
            if (randomNumberTurnOfDirectionY == 0)
            {
                randomY = RandDeplacement.Next(3); //Génère un entier compris entre 0 et 2; 0 => sur place
                randomNumberTurnOfDirectionY = RandDeplacement.Next(1, 300);
            }
            if (randomNumberTurnOfDirectionX == 0)
            {
                randomX = RandDeplacement.Next(3); //Génère un entier compris entre 0 et 2
                randomNumberTurnOfDirectionX = RandDeplacement.Next(1,300);
            }
            // A chaque tour on décrémente le nombre de fois que le poisson doit prendre cette direction.
            randomNumberTurnOfDirectionX--;
            randomNumberTurnOfDirectionY--;

            signeX = 1;
            if (randomX == 0)
            { signeX = -1; }
            positionXTested = positionX + SpeedX * signeX;


            signeY = 1;
            if (randomY == 0)
            { signeY = -1; }
            positionYTested = positionY + SpeedY * signeY;

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
            if (randomX != 2)
            {
                positionX = positionXTested;
            }
            if (randomY != 2)
            {
                positionY = positionYTested;
            }

        }

        public bool Inclinaison()
        {
            if (signeX == -1)
            {
                return true;
            }
            return false;
        }
    }
}
