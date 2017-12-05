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

namespace GoFish
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GoFishWindow : Window
    {
        public GoFishWindow()
        {
            SplashScreenThread();
            InitializeComponent();
            //Deck deck = new Deck();
            //MessageBox.Show(deck.ToString());
        }

        public void SplashScreenThread()
        {
            SplashScreen splash = new SplashScreen("/Images/GoFish.jpg");
            splash.Show(false);
            splash.Close(new TimeSpan(0, 0, 5));
        }
    }
}
