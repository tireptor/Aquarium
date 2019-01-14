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
        DataModel.Aquarium myAquarium = new DataModel.Aquarium(580,350);
        Random RandPosition = new Random();

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer MonTimer = new DispatcherTimer();
            MonTimer .Interval = new TimeSpan(0, 0, 0, 0, 10); // 100 ms.
            MonTimer .Tick += delegate(object s, EventArgs args)
            {
                refreshCanvas();
                drawFishs();
            };
            MonTimer.Start();


        }
        private void drawFishs()
        {
            foreach (DataModel.Fish fish in myAquarium.Fishs)
            {
                fish.Deplacement();
                Ellipse myEllipse = new Ellipse();
                myEllipse.Stroke = System.Windows.Media.Brushes.Black;
                myEllipse.Fill = System.Windows.Media.Brushes.Red;
                myEllipse.HorizontalAlignment = HorizontalAlignment.Left;
                myEllipse.VerticalAlignment = VerticalAlignment.Center;
                myEllipse.Width = 20;
                myEllipse.Height = 20;
                Canvas.SetTop(myEllipse, fish.PositionY);
                Canvas.SetRight(myEllipse, fish.PositionX);
                aquariumCanvas.Children.Add(myEllipse);
            }
        }
        private void refreshCanvas()
        {
            aquariumCanvas.Children.Clear();
        }
        private void ClicButtonAddFish(object sender, RoutedEventArgs e)
        {
            int randomX = RandPosition.Next(myAquarium.Width); //Génère un entier compris entre 0 et la largeur de l'aquarium
            int randomY = RandPosition.Next(myAquarium.Height); //Génère un entier compris entre 0 et la hauteur de l'aquarium
            myAquarium.addFish(randomX,randomY);
        }

        private void ClicButtonAddShark(object sender, RoutedEventArgs e)
        {

        }

        private void ClicButtonExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            refreshCanvas();
        }
    }
}
