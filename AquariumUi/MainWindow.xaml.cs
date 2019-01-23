using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AquariumUi
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataModel.Aquarium myAquarium = new DataModel.Aquarium(580, 350);
        Random RandPosition = new Random();
        int nbGoldFish = 0;
        int nbMoonFish = 0;
        int nbCatFish = 0;
        int randomX;
        int randomY;
        bool isPause = false;

        DispatcherTimer MonTimer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 0, 0, 20) // 20 ms.
        };
        public MainWindow()
        {
            InitializeComponent();

            MonTimer.Tick += delegate (object s, EventArgs args)
           {
               RefreshCanvas();
               DrawFishs();
                //InitDecorations();
            };
            MonTimer.Start();


        }
        public void InitDecorations()
        {
            ChoiceRandomPositions();
            myAquarium.AddAlgae(randomX, randomY);
            ChoiceRandomPositions();
            myAquarium.AddAlgae(randomX, randomY);

            foreach (DataModel.Decoration decoration in myAquarium.Decorations)
            {
                BitmapImage bi3 = new BitmapImage();
                Image myPicture = new Image();
                bi3.BeginInit();
                bi3.UriSource = new Uri(decoration.PicturePath, UriKind.Relative);
                bi3.EndInit();

                myPicture.Stretch = Stretch.Fill;
                myPicture.Source = bi3;

                Canvas.SetTop(myPicture, decoration.PositionY);
                Canvas.SetRight(myPicture, decoration.PositionX);
                aquariumCanvas.Children.Add(myPicture);
            }

        }
        private void EventClick(object sender, MouseEventArgs e)
        {
            Point initial;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                initial = Mouse.GetPosition(aquariumCanvas);
                // On ajoute le poisson le plus proche du clic dans la liste des poissons mangés, il sera supprimé après
                myAquarium.Fishs.Remove(FishSameClosest(initial));
                //myAquarium.FishsEating.Add(FishSameClosest(initial));
            }
        }

        private DataModel.Fish FishSameClosest(Point initial)
        {
            int smallerGap = 10000000;
            int gapBetweenSharkAndFish = 0;
            DataModel.Fish fishWithSmallGap = null;
            foreach (DataModel.Fish fish in myAquarium.Fishs)
            {
                if (!(fish is DataModel.Shark))
                {
                    continue;
                }
                gapBetweenSharkAndFish = (int)Distance(initial.X, initial.Y, fish.PositionX, fish.PositionY);
                if (gapBetweenSharkAndFish < smallerGap)
                {
                    smallerGap = gapBetweenSharkAndFish;
                    fishWithSmallGap = fish;
                }
            }
            return fishWithSmallGap;
        }
        private int CalculDelta(DataModel.Fish fish, int totalPositionThis)
        {
            int delta = 0;
            int total = fish.PositionX * fish.PositionX + fish.PositionY * fish.PositionY;

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

        public void KeyDownEventHandler(object sender, KeyEventArgs e)
        {
            ChoiceRandomPositions();
            if (e.Key == Key.R)
            {
                myAquarium.AddGoldFish(randomX, randomY);
            }
            if (e.Key == Key.L)
            {
                myAquarium.AddMoonFish(randomX, randomY);
            }
            if (e.Key == Key.C)
            {
                myAquarium.AddCatFish(randomX, randomY);
            }

        }
        private void DrawFishs()
        {
            nbGoldFish = 0;
            nbMoonFish = 0;
            nbCatFish = 0;

            foreach (DataModel.Fish fish in myAquarium.Fishs.ToList())
            {

                fish.Deplacement();
                int sizeHeight = 0;
                int sizeWidth = 0;
                // si le poisson est un oeuf alors on applique une taille plus petite (taille d'un oeuf)
                if (fish.IsAnEgg)
                {
                    sizeHeight = fish.SizeEgg;
                    sizeWidth = fish.SizeEgg;
                }
                else
                {
                    sizeHeight = fish.Size;
                    sizeWidth = fish.Size;
                }
                Image sprite = new Image
                {
                    Height = sizeHeight,
                    Width = sizeWidth
                };
                // Si un changement de direction est détecté lors du déplacement d'un poisson alors on retourne l'image du poisson
                if (fish.Inclinaison())
                {
                    sprite.RenderTransform = new ScaleTransform() { ScaleX = -1 };
                }

                if (fish is DataModel.GoldFish)
                {
                    nbGoldFish++;
                    if (fish.IsAnEgg)
                        sprite.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Resources\egg.png"));
                    else
                        sprite.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Resources\goldFish.png"));
                    //myEllipse.Fill = System.Windows.Media.Brushes.Red;
                }
                if (fish is DataModel.MoonFish)
                {
                    nbMoonFish++;
                    if (fish.IsAnEgg)
                        sprite.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Resources\egg.png"));
                    else
                        sprite.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Resources\moonFish.png"));
                    //myEllipse.Fill = System.Windows.Media.Brushes.Gray;
                }
                if (fish is DataModel.CatFish)
                {
                    nbCatFish++;
                    if (fish.IsAnEgg)
                        sprite.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Resources\egg.png"));
                    else
                        sprite.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Resources\catFish.png"));
                    //myEllipse.Fill = System.Windows.Media.Brushes.Beige;
                }
                if (fish is DataModel.Shark)
                {
                    sprite.Source = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Resources\shark.png"));
                    //myEllipse.Fill = System.Windows.Media.Brushes.Black;
                }

                Canvas.SetTop(sprite, fish.PositionY);
                Canvas.SetLeft(sprite, fish.PositionX);
                aquariumCanvas.Children.Add(sprite);
            }
            //foreach (DataModel.Fish fish in myAquarium.FishsEating)
            //{
            //    myAquarium.Fishs.Remove(fish);
            //}
            //myAquarium.FishsEating.Clear();

            labGoldFish.Content = "Poissons rouge : " + nbGoldFish;
            labMoonFish.Content = "Poissons lune : " + nbMoonFish;
            labCatFish.Content = "Poissons chat : " + nbCatFish;
        }
        private void RefreshCanvas()
        {
            aquariumCanvas.Children.Clear();
        }

        private void ClicButtonAddShark(object sender, RoutedEventArgs e)
        {
            ChoiceRandomPositions();
            myAquarium.AddShark(randomX, randomY);
        }

        private void ClicButtonExit(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void ClicButtonAddGoldFish(object sender, RoutedEventArgs e)
        {
            ChoiceRandomPositions();
            myAquarium.AddGoldFish(randomX, randomY);
        }

        private void ClicButtonAddMoonFish(object sender, RoutedEventArgs e)
        {
            ChoiceRandomPositions();
            myAquarium.AddMoonFish(randomX, randomY);
        }

        private void ClicButtonAddCatFish(object sender, RoutedEventArgs e)
        {
            ChoiceRandomPositions();
            myAquarium.AddCatFish(randomX, randomY);
        }
        private void ChoiceRandomPositions()
        {
            randomX = RandPosition.Next(myAquarium.Width); //Génère un entier compris entre 0 et la largeur de l'aquarium
            randomY = RandPosition.Next(myAquarium.Height); //Génère un entier compris entre 0 et la hauteur de l'aquarium
        }

        private void ButtonPauseClick(object sender, RoutedEventArgs e)
        {
            if (!isPause)
            {
                MonTimer.Stop();
                btnPause.Content = "Reprendre";
                isPause = true;
            }
            else
            {
                MonTimer.Start();
                btnPause.Content = "Pause";
                isPause = false;
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
        private void CanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            myAquarium.Height = (int)aquariumCanvas.ActualHeight;
            myAquarium.Width = (int)aquariumCanvas.ActualWidth;
        }
    }
}
