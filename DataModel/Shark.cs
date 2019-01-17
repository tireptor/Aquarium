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
            MyAquarium.FishsEating.Add(FishPurchased);
        }
        private void InitSharkBehavior()
        {
            // Réinitialise le comportement du requin, reprise du mode balade et réinitialisation du poisson à poursuivre
            Ride();
            FishPurchased = null;
        }
        private void SearchCloser()
        {
            int smallerGap = 100;
            int gapBetweenSharkAndFish = 0;
            Fish fishWithSmallGap = FishPurchased;
            int total = this.PositionX + this.PositionY;
            foreach (Fish fish in MyAquarium.Fishs)
            {
                // On ne doit pas poursuivre les requins
                if (fish is Shark)
                continue;

                gapBetweenSharkAndFish = CalculDelta(fish,total);
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
            int signe = 1;

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

            if (randomX == 0)
            { signe = -1; }
            positionXTested = PositionX + SpeedX * signe;
            signe = 1;
            if (randomY == 0)
            { signe = -1; }
            positionYTested = PositionY + SpeedY * signe;

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
                    this.PositionX = this.PositionX + this.SpeedX;
                }
                if (this.PositionX > FishPurchased.PositionX)
                {
                    this.PositionX = this.PositionX - this.SpeedX;
                }
                if (this.PositionY < FishPurchased.PositionY)
                {
                    this.PositionY = this.PositionY + this.SpeedY;
                }
                if (this.PositionY > FishPurchased.PositionY)
                {
                    this.PositionY = this.PositionY - this.SpeedY;
                }//comment
            }

        }
    }
}
