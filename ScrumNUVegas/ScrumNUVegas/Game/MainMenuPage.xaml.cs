using ScrumNUVegas.BlackJack;
using ScrumNUVegas.GoFish;
using ScrumNUVegas.Poker;
using ScrumNUVegas.War;
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

namespace ScrumNUVegas.Game
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
            
        }

        public void SplashScreenThread()
        {
            SplashScreen splash = new SplashScreen("/Images/NUVegas.jpg");
            splash.Show(false);
            splash.Close(new TimeSpan(0, 0, 3));
        }

        private void BlackJackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow navigation = new NavigationWindow();
            BlackJackPage page = new BlackJackPage();
            navigation.Navigate(page);
        }

        private void GoFishBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new GoFishPage();
        }

        private void PokerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new PokerPage();
        }

        private void WarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new WarPage();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MainMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Content = new MainMenuPage();
        }
    }
}
