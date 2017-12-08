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
using System.Windows.Resources;
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
            Deck deck = new Deck();
            Test1.Source = deck.Cards[0].CardImage;
            Save.Visibility = Visibility.Hidden;
            Load.Visibility = Visibility.Hidden;
        }

        public void ShowSplashScreen()
        {
            SplashScreen splash = new SplashScreen("/Images/NUVegas.jpg");
            splash.Show(false);
            splash.Close(new TimeSpan(0, 0, 3));
        }

        private void BlackJackBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            Uri resourceUri = new Uri("Resources/Images/CasinoTable.jpg", UriKind.Relative);
            Background.Source = new BitmapImage(resourceUri);
            GameArea.Children.Add(new BlackJackControl());
        }

        private void GoFishBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            Uri resourceUri = new Uri("Resources/Images/Go-Fish.jpg", UriKind.Relative);
            Background.Source = new BitmapImage(resourceUri);
            GameArea.Children.Add(new GoFishControl());
        }

        private void PokerBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            Uri resourceUri = new Uri("Resources/Images/Poker.jpg", UriKind.Relative);
            Background.Source = new BitmapImage(resourceUri);
            Background.Stretch = Stretch.Fill;
            Background.Width = 500;
            Background.Height = 300;
            GameArea.Children.Add(new PokerControl());
        }

        private void WarBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            Uri resourceUri = new Uri("Resources/Images/War.jpg", UriKind.Relative);
            Background.Source = new BitmapImage(resourceUri);
            GameArea.Children.Add(new WarControl());
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GameArea.Children.Clear();
            Uri resourceUri = new Uri("Resources/Images/NuVegas.jpg", UriKind.Relative);
            Background.Source = new BitmapImage(resourceUri);
            Background.Width = 600;
            Background.Height = 400;
            GameArea.Children.Add(mainMenu);
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Load_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}