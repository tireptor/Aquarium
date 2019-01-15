﻿using System;
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
        int nbFish = 0;
        int randomX;
        int randomY;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer MonTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 20) // 10 ms.
            };
            MonTimer .Tick += delegate(object s, EventArgs args)
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
            myAquarium.AddAlgae(randomX,randomY);
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
            nbFish = 0;
            foreach (DataModel.Fish fish in myAquarium.Fishs)
            {
                nbFish++;
                fish.Deplacement();
                Ellipse myEllipse = new Ellipse
                {
                    Stroke = System.Windows.Media.Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20
                };
                if (fish is DataModel.GoldFish)
                {
                    myEllipse.Fill = System.Windows.Media.Brushes.Red;
                }
                if (fish is DataModel.MoonFish)
                {
                    myEllipse.Fill = System.Windows.Media.Brushes.Gray;
                }
                if (fish is DataModel.CatFish)
                {
                    myEllipse.Fill = System.Windows.Media.Brushes.Beige;
                }
                if (fish is DataModel.Shark)
                {
                    myEllipse.Fill = System.Windows.Media.Brushes.Black;
                }

                Canvas.SetTop(myEllipse, fish.PositionY);
                Canvas.SetRight(myEllipse, fish.PositionX);
                aquariumCanvas.Children.Add(myEllipse);
            }
            labNameFish.Content = "Nombre de poisson : " + nbFish;
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

    }
}
