﻿using GameHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ScrumNUVegas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
<<<<<<< HEAD

=======
            //SplashScreenThread();
>>>>>>> 2d92bf355f2af7879860c30f532979659a7831f1
            InitializeComponent();
            MainMenuLabel.Visibility = Visibility.Visible;
            //Deck deck = new Deck();
            //MessageBox.Show(deck.ToString());
        }

        public void SplashScreenThread()
        {
            SplashScreen splash = new SplashScreen("/Images/NUVegas.jpg");
            splash.Show(false);
            splash.Close(new TimeSpan(0, 0, 3));
        }

        private void BlackJackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoFishBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PokerBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Poker is currently in progress");
        }

        private void WarBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}