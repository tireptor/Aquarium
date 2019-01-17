using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Aquarium
    {
        private List<Fish> fishs = new List<Fish>(); // liste des poissons dans l'aquarium
        private List<Fish> fishsEating = new List<Fish>(); // liste des poissons qui viennent d'être mangés
        private List<Decoration> decorations = new List<Decoration>(); // création de la liste des décorations de l'aquarium
        private int width; 
        private int height;

        public Aquarium(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get => width; private set => width = value; }
        public int Height { get => height; private set => height = value; }

        public List<Fish> Fishs { get => fishs; private set => fishs = value; }
        public List<Decoration> Decorations { get => decorations; private set => decorations = value; }
        public List<Fish> FishsEating { get => fishsEating; set => fishsEating = value; }

        // Ajout des poissons
        public void AddGoldFish(int randomX, int randomY)
        {
            GoldFish f = new GoldFish(randomX, randomY, this);
            fishs.Add(f);
        }
        public void AddMoonFish(int randomX, int randomY)
        {
            MoonFish f = new MoonFish(randomX, randomY, this);
            fishs.Add(f);
        }
        public void AddCatFish(int randomX, int randomY)
        {
            CatFish f = new CatFish(randomX, randomY, this);
            fishs.Add(f);
        }

        // AJout du requin
        public void AddShark(int randomX, int randomY)
        {
            Shark s = new Shark(randomX, randomY, this);
            fishs.Add(s);
        }

        // Ajout des decorations
        public void AddAlgae(int randomX, int randomY)
        {
            Algae a = new Algae(randomX, randomY, this);
            decorations.Add(a);
        }
        public void AddShell(int randomX, int randomY)
        {
            Shell s = new Shell(randomX, randomY, this);
            decorations.Add(s);
        }
        public void AddWreck(int randomX, int randomY)
        {
            Wreck w = new Wreck(randomX, randomY, this);
            decorations.Add(w);
        }

    }
}
