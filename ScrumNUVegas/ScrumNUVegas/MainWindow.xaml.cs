using GameHub.Models;
using ScrumNUVegas.Game.BlackJack;
using ScrumNUVegas.Game.GoFish;
using ScrumNUVegas.Game.Poker;
using ScrumNUVegas.Game.War;
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
        private StackPanel mainMenu;
        public MainWindow()
        {
            InitializeComponent();
            mainMenu = MainMenu;
        }

        public void SplashScreenThread()
        {
            SplashScreen splash = new SplashScreen("/Images/NUVegas.jpg");
            splash.Show(false);
            splash.Close(new TimeSpan(0, 0, 3));
        }

        private void BlackJackBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            GameArea.Children.Add(new BlackJackControl());
        }

        private void GoFishBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            GameArea.Children.Add(new GoFishControl());
        }

        private void PokerBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            GameArea.Children.Add(new PokerControl());
        }

        private void WarBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            GameArea.Children.Add(new WarControl());
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MainMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GameArea.Children.Clear();
            GameArea.Children.Add(mainMenu);
        }
    }
}