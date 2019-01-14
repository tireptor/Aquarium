using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Aquarium
    {
        private List<Fish> fishs = new List<Fish>(); // création de la liste des poissons
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

        public Fish addFish (int randomX,int randomY)
        {
            Fish f = new Fish(randomX,randomY,this);
            fishs.Add(f);
            return f;
        }

    }
}
