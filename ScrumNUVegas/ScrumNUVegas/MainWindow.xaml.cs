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
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int ticks = 0;
        private MenuItem saveItem = new MenuItem() { Header = "Save", Command = ApplicationCommands.Save, InputGestureText = "Ctrl+S" };
        private MenuItem openItem = new MenuItem() { Header = "Open", Command = ApplicationCommands.Close, InputGestureText = "Ctrl+O" };
        private KeyBinding saveBinding = new KeyBinding() { Command = ApplicationCommands.Save, Key = Key.S, Modifiers = ModifierKeys.Control };
        private KeyBinding openBinding = new KeyBinding() { Command = ApplicationCommands.Close, Key = Key.O, Modifiers = ModifierKeys.Control };
        private CommandBinding saveCommand = new CommandBinding() { Command = ApplicationCommands.Save };
        private CommandBinding openCommand = new CommandBinding() { Command = ApplicationCommands.Close };

        public MainWindow()
        {
            InitializeComponent();
            mainMenu = MainMenu;
            this.WindowState = WindowState.Maximized;
            this.ResizeMode = ResizeMode.NoResize;
            TimerSetup();
        }

        private void TimerSetup()
        {
            GameArea.Opacity = 0;
            timer.Tick += FadeIn;
            timer.Interval = 1;
            timer.Start();
        }

        private void FadeIn(object sender, EventArgs e)
        {
            ticks++;
            GameArea.Opacity += .008;
            if (GameArea.Opacity == 1)
            {
                timer.Stop();
            }
        }

        private void BlackJackBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            Uri resourceUri = new Uri("Resources/Images/CasinoTable.jpg", UriKind.Relative);
            Background.Source = new BitmapImage(resourceUri);
            Background.Stretch = Stretch.Fill;
            Background.Width = 1300;
            Background.Height = 700;
            MenuHeader.Items.Add(saveItem);
            MenuHeader.Items.Add(openItem);
            saveCommand.Executed += BlackJackSave_Executed;
            openCommand.Executed += BlackJackLoad_Executed;
            this.InputBindings.Add(saveBinding);
            this.InputBindings.Add(openBinding);
            this.CommandBindings.Add(saveCommand);
            this.CommandBindings.Add(openCommand);
            GameArea.Children.Add(new BlackJackControl());
        }

        private void GoFishBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            Uri resourceUri = new Uri("Resources/Images/Go-Fish.jpg", UriKind.Relative);
            Background.Source = new BitmapImage(resourceUri);
            Background.Stretch = Stretch.Fill;
            Background.Width = 1300;
            Background.Height = 700;
            MenuHeader.Items.Add(saveItem);
            MenuHeader.Items.Add(openItem);
            saveCommand.Executed += GoFishSave_Executed;
            openCommand.Executed += GoFishLoad_Executed;
            this.InputBindings.Add(saveBinding);
            this.InputBindings.Add(openBinding);
            this.CommandBindings.Add(saveCommand);
            this.CommandBindings.Add(openCommand);
            GameArea.Children.Add(new GoFishControl());
        }

        private void PokerBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            Uri resourceUri = new Uri("Resources/Images/Poker.jpg", UriKind.Relative);
            Background.Source = new BitmapImage(resourceUri);
            Background.Stretch = Stretch.Fill;
            Background.Width = 1300;
            Background.Height = 700;
            MenuHeader.Items.Add(saveItem);
            MenuHeader.Items.Add(openItem);
            this.InputBindings.Add(saveBinding);
            this.InputBindings.Add(openBinding);
            saveCommand.Executed += PokerSave_Executed;
            openCommand.Executed += PokerLoad_Executed;
            this.CommandBindings.Add(saveCommand);
            this.CommandBindings.Add(openCommand);
            GameArea.Children.Add(new PokerControl());
        }

        private void WarBtn_Click(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            Uri resourceUri = new Uri("Resources/Images/War.jpg", UriKind.Relative);
            Background.Source = new BitmapImage(resourceUri);
            Background.Stretch = Stretch.Fill;
            Background.Width = 1300;
            Background.Height = 700;
            MenuHeader.Items.Add(saveItem);
            MenuHeader.Items.Add(openItem);
            this.InputBindings.Add(saveBinding);
            this.InputBindings.Add(openBinding);
            saveCommand.Executed += WarSave_Executed;
            openCommand.Executed += WarLoad_Executed;
            this.CommandBindings.Add(saveCommand);
            this.CommandBindings.Add(openCommand);
            GameArea.Children.Add(new WarControl());
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainMenu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GameArea.Children.Clear();
            MenuHeader.Items.Remove(saveItem);
            MenuHeader.Items.Remove(openItem);
            this.InputBindings.Remove(saveBinding);
            this.InputBindings.Remove(openBinding);
            saveCommand.Executed -= WarSave_Executed;
            openCommand.Executed -= WarLoad_Executed;
            saveCommand.Executed -= PokerSave_Executed;
            openCommand.Executed -= PokerLoad_Executed;
            saveCommand.Executed -= BlackJackSave_Executed;
            openCommand.Executed -= BlackJackLoad_Executed;
            saveCommand.Executed -= GoFishSave_Executed;
            openCommand.Executed -= GoFishLoad_Executed;
            this.CommandBindings.Remove(saveCommand);
            this.CommandBindings.Remove(openCommand);
            Uri resourceUri = new Uri("Resources/Images/NuVegas.jpg", UriKind.Relative);
            Background.Source = new BitmapImage(resourceUri);
            GameArea.Children.Add(mainMenu);
        }

        private void WarSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("War Saved!");
        }

        private void WarLoad_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("War Opened!");
        }

        private void GoFishSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void GoFishLoad_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("GoFish Opened!");
        }

        private void PokerSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Poker Saved!");
        }

        private void PokerLoad_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Poker Opened!");
        }

        private void BlackJackSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("BlackJack Saved!");
        }

        private void BlackJackLoad_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("BlackJack Opened!");
        }
    }
}