using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public sealed class Shark : Fish
    {
        private Fish fishPurchased;
        private int numberTurnToPurchasing=0;

        public Fish FishPurchased { get => fishPurchased; private set => fishPurchased = value; }
        public int NumberTurnToPurchasing { get => numberTurnToPurchasing; private set => numberTurnToPurchasing = value; }

        public Shark(int positionX, int positionY, Aquarium myAquarium) : base(positionX, positionY, myAquarium)
        {
            this.SpeedX = 2;
            this.SpeedY = 2;
            this.Size = 120;
        }
        private bool ImSamePositionWithFish()
        {
            // Si le requin est très proche du poisson qu'il poursuit alors la procédure renvoie vrai.
            if (((this.PositionX > FishPurchased.PositionX - 2)&&(this.PositionX < FishPurchased.PositionX + 2)) && (this.PositionY > FishPurchased.PositionY - 2)&& (this.PositionY < FishPurchased.PositionY + 2))
            {
                return true;
            }
            return false;
        }
        private void EatFish()
        {
            // On ajouter le poisson dans la liste des poissons mangés, tous les poissons de cette liste seront mangés
            //MyAquarium.FishsEating.Add(FishPurchased);
            MyAquarium.Fishs.Remove(FishPurchased);
        }
        private void InitSharkBehavior()
        {
            // Réinitialise le comportement du requin, reprise du mode balade et réinitialisation du poisson à poursuivre
            Ride();
            FishPurchased = null;
        }
        private void SearchCloser()
        {
            int smallerGap = 200;
            int gapBetweenSharkAndFish = 0;
            Fish fishWithSmallGap = FishPurchased;
            int total = this.PositionX + this.PositionY;
            foreach (Fish fish in MyAquarium.Fishs)
            {
                // On ne doit pas poursuivre les requins et les oeufs
                if ((fish is Shark) || (fish.IsAnEgg))
                continue;

                //gapBetweenSharkAndFish = CalculDelta(fish,total);
                gapBetweenSharkAndFish = (int)Distance(this.PositionX,this.PositionY,fish.PositionX,fish.PositionY);
                if (gapBetweenSharkAndFish < smallerGap)
                {
                    smallerGap = gapBetweenSharkAndFish;
                    FishPurchased = fish;
                }
            }
        }
        private int CalculDelta(Fish fish, int totalPositionThis)
        {
            int delta = 0;
            int total = fish.PositionX + fish.PositionY;

            if (total >= totalPositionThis)
            {
                delta = total - totalPositionThis;
            }
            if (total < totalPositionThis)
            {
                delta = totalPositionThis - total;
            }
            return delta;
        }

        private void Ride()
        {
            int positionXTested;
            int positionYTested;

            if (randomNumberTurnOfDirectionY == 0)
            {
                randomY = RandDeplacement.Next(3); //Génère un entier compris entre 0 et 2; 0 => sur place
                randomNumberTurnOfDirectionY = RandDeplacement.Next(1, 300);
            }
            if (randomNumberTurnOfDirectionX == 0)
            {
                randomX = RandDeplacement.Next(3); //Génère un entier compris entre 0 et 2
                randomNumberTurnOfDirectionX = RandDeplacement.Next(1, 300);
            }
            // A chaque tour on décrémente le nombre de fois que le poisson doit prendre cette direction.
            randomNumberTurnOfDirectionX--;
            randomNumberTurnOfDirectionY--;
            this.SigneX = 1;
            this.SigneY = 1;
            if (randomX == 0)
            { this.SigneX = -1; }
            positionXTested = PositionX + SpeedX * this.SigneX;

            if (randomY == 0)
            { this.SigneY = -1; }
            positionYTested = PositionY + SpeedY * this.SigneY;

            if (positionXTested >= this.MyAquarium.Width - this.Size)
            {
                positionXTested -= SpeedX;
                // Si on fonce dans un bord de l'aquarium on réinitialise la direction du poisson.
                randomNumberTurnOfDirectionX = 0;
            }
            if (positionYTested >= this.MyAquarium.Height - this.Size)
            {
                positionYTested -= SpeedY;
                randomNumberTurnOfDirectionY = 0;
            }
            if (positionXTested <= 0 + this.Size)
            {
                positionXTested += SpeedX;
                randomNumberTurnOfDirectionX = 0;
            }
            if (positionYTested <= 0 + this.Size)
            {
                positionYTested += SpeedY;
                randomNumberTurnOfDirectionY = 0;
            }
            if (randomX != 2)
            {
                PositionX = positionXTested;
            }
            if (randomY != 2)
            {
                PositionY = positionYTested;
            }
        }

        public override void Deplacement()
        {

            if (FishPurchased == null)
            {
                SearchCloser();
                // Si on a trouvé un poisson proche du requin, alors on définit un nombre de tour pendant lequel on va poursuivre le poisson
                if (FishPurchased != null)
                NumberTurnToPurchasing = RandDeplacement.Next(1, 300);
            }
            else
            {
                if (ImSamePositionWithFish())
                {
                    EatFish();
                    InitSharkBehavior();
                    return;
                }
            }
            if (NumberTurnToPurchasing == 0)
            {
                // si le requin ne poursuit aucun poisson alors il est en mode balade (déplacement conventionnel des poissons)
                InitSharkBehavior();
                return;
            }

            NumberTurnToPurchasing--;

            if (FishPurchased != null)
            {
                if (this.PositionX < FishPurchased.PositionX)
                {
                    this.SigneX = 1;
                    this.PositionX = this.PositionX + this.SpeedX;
                }
                if (this.PositionX > FishPurchased.PositionX)
                {
                    this.SigneX = -1;
                    this.PositionX = this.PositionX - this.SpeedX;
                }
                if (this.PositionY < FishPurchased.PositionY)
                {
                    this.PositionY = this.PositionY + this.SpeedY;
                }
                if (this.PositionY > FishPurchased.PositionY)
                {
                    this.PositionY = this.PositionY - this.SpeedY;
                }
            }

        }
        static public double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Sqr(y2 - y1) + Sqr(x2 - x1));
        }


        static public double Sqr(double a)
        {
            return a * a;
        }
    }
}
