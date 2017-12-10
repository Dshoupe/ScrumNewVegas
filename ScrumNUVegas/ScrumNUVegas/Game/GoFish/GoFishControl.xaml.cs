using GameHub.Models;
using Microsoft.Win32;
using ScrumNUVegas.Game.GoFish.GoFishModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace ScrumNUVegas.Game.GoFish
{
    /// <summary>
    /// Interaction logic for GoFishControl.xaml
    /// </summary>
    public partial class GoFishControl : UserControl
    {
        Dealer dealer;
        int turnOrder = 0;
        int totalCardCount = 52;
        GoFishPlayer[] players;

        public GoFishControl()
        {
            InitializeComponent();

        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            SetUpPanel.Visibility = Visibility.Hidden;

            dealer = new Dealer();
            players = CreatePlayers(PlayersSlider.Value);
            int count = 0;
            foreach (GoFishPlayer p in players)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;

                Thickness margin = sp.Margin;
                margin.Top = 20;
                sp.Margin = margin;

                p.Hand = dealer.DealFiveCards();
                Label l = new Label();
                margin = l.Margin;
                margin.Top = 10;
                margin.Bottom = 10;
                l.Margin = margin;
                l.Width = 100;
                TextBox t = (TextBox)PlayerSelectionLabels.Children[count++];
                l.Background = new SolidColorBrush(Colors.DeepSkyBlue);
                l.Foreground = new SolidColorBrush(Colors.White);
                l.FontSize = 20;
                l.Content = t.Text;
                p.Name = l.Content.ToString();

                sp.Children.Add(l);
                
                for (int i = 0; i < p.Hand.Count(); i++)
                {
                    p.Hand[i].Flip();
                    //REALLY weird issue, had to access the images at runtime so for GoFish, the images will be held in the debug folder -- https://stackoverflow.com/questions/569561/dynamic-loading-of-images-in-wpf
                    Label label = new Label();
                    Image img = new Image();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.UriSource = p.Hand[i].CardUri;
                    bitmapImage.EndInit();
                    img.Source = bitmapImage;
                    label.Content = img;
                    margin = label.Margin;
                    margin.Right = 10;
                    margin.Left = 10;
                    label.Margin = margin;
                    label.Width = 50;
                    sp.Children.Add(label);
                }
                Label scoreCard = new Label();
                margin = scoreCard.Margin;
                margin.Top = 10;
                margin.Bottom = 10;
                scoreCard.Margin = margin;
                scoreCard.Width = 50;
                scoreCard.Content = $"Pairs: {p.Score}";
                scoreCard.Background = new SolidColorBrush(Colors.DeepSkyBlue);
                scoreCard.Foreground = new SolidColorBrush(Colors.White);
                sp.Children.Insert(0,scoreCard);

                PlayerField.Children.Add(sp);
                int currentTurn = turnOrder++ % (int)PlayersSlider.Value;
                checkForPairs(p, currentTurn);
            }
                ChangeActivePlayer();
        }

        private void goFish()
        {
            StackPanel sp = (StackPanel)PlayerField.Children[turnOrder % (int)PlayersSlider.Value];
            players[turnOrder % (int)PlayersSlider.Value].Hand.Add(dealer.dealOneCard());
            if (dealer.RemainingCards() == 0)
            {
                GoFishTester.Children.Clear();
            }
            Label label = new Label();
            Image img = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.UriSource = players[turnOrder % (int)PlayersSlider.Value].Hand.Last().CardUri;
            bitmapImage.EndInit();
            img.Source = bitmapImage;
            label.Content = img;
            Thickness margin = label.Margin;
            margin.Right = 10;
            margin.Left = 10;
            label.Margin = margin;
            label.Width = 50;
            sp.Children.Add(label);
            int playerOldScore = players[turnOrder % (int)PlayersSlider.Value].Score;
            checkForPairs(players[turnOrder % (int)PlayersSlider.Value], turnOrder % (int)PlayersSlider.Value);
            int playerNewScore = players[turnOrder % (int)PlayersSlider.Value].Score;
            if (playerOldScore < playerNewScore)
            {
                MessageBox.Show($"{players[turnOrder % (int)PlayersSlider.Value].Name} fished what they wanted. They get another turn!");
                
            }
            else
            {
                ChangeActivePlayer();
            }
        }

        private void ChangeActivePlayer()
        {
            int count = 2;
            if (turnOrder != 0)
            {
                StackPanel sp = (StackPanel)PlayerField.Children[turnOrder % (int)PlayersSlider.Value];
                foreach (Card c in players[turnOrder % (int)PlayersSlider.Value].Hand)
                {
                    c.Flip();
                    Label label = (Label)sp.Children[count++];
                    Image img = new Image();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.UriSource = c.CardUri;
                    bitmapImage.EndInit();
                    img.Source = bitmapImage;
                    label.Content = img;
                    Thickness margin = label.Margin;
                    margin.Right = 10;
                    margin.Left = 10;
                    label.Margin = margin;
                    label.Width = 50;
                }
                turnOrder++;
                sp = (StackPanel)PlayerField.Children[turnOrder % (int)PlayersSlider.Value];
                count = 2;
                foreach (Card c in players[turnOrder % (int)PlayersSlider.Value].Hand)
                {
                    c.Flip();
                    Label label = (Label)sp.Children[count++];
                    Image img = new Image();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.UriSource = c.CardUri;
                    bitmapImage.EndInit();
                    img.Source = bitmapImage;
                    label.Content = img;
                    Thickness margin = label.Margin;
                    margin.Right = 10;
                    margin.Left = 10;
                    label.Margin = margin;
                    label.Width = 50;
                }
            }
            else
            {
                StackPanel sp = (StackPanel)PlayerField.Children[turnOrder % (int)PlayersSlider.Value];
                foreach (Card c in players[turnOrder % (int)PlayersSlider.Value].Hand)
                {
                    c.Flip();
                    Label label = (Label)sp.Children[count++];
                    Image img = new Image();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.UriSource = c.CardUri;
                    bitmapImage.EndInit();
                    img.Source = bitmapImage;
                    label.Content = img;
                    Thickness margin = label.Margin;
                    margin.Right = 10;
                    margin.Left = 10;
                    label.Margin = margin;
                    label.Width = 50;
                }
            }
        }   

        private void checkForPairs(GoFishPlayer player, int playerNum)
        {
            bool hasPair = false;
            for (int i = 0; i < (player.Hand.Count - 1); i++)
            {
                for (int j = (i+1); j < player.Hand.Count && !hasPair; j++)
                {
                    if(player.Hand[i].FaceValue == player.Hand[j].FaceValue)
                    {
                        hasPair = true;
                        player.Score += 1;
                        MessageBox.Show($"{player.Name} played a pair of {player.Hand[i].FaceValue}'s. Their new score is {player.Score}");
                        player.Hand.RemoveAt(j);
                        StackPanel sp = (StackPanel)PlayerField.Children[playerNum];
                        sp.Children.RemoveAt(j + 2);
                        player.Hand.RemoveAt(i);
                        sp.Children.RemoveAt(i + 2);
                        totalCardCount -= 2;

                        sp.Children.RemoveAt(0);
                        Label scoreCard = new Label();
                        Thickness margin = scoreCard.Margin;
                        margin.Top = 10;
                        margin.Bottom = 10;
                        scoreCard.Margin = margin;
                        scoreCard.Width = 50;
                        scoreCard.Content = $"Pairs: {player.Score}";
                        scoreCard.Background = new SolidColorBrush(Colors.DeepSkyBlue);
                        scoreCard.Foreground = new SolidColorBrush(Colors.White);
                        sp.Children.Insert(0, scoreCard);
                        i -= 1;
                    }
                }
                hasPair = false;
            }
        }

        private GoFishPlayer[] CreatePlayers(double value)
        {
            GoFishPlayer[] players = new GoFishPlayer[(int)value];
            for (int i = 0; i < value; i++)
            {
                players[i] = new GoFishPlayer();
            }
            return players;
        }

        private void PlayersSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            PlayerSelectionLabels.Children.Clear();
            for (int i = 0; i < PlayersSlider.Value; i++)
            {
                TextBox t = new TextBox();
                t.Name = $"Player{i + 1}";
                t.Text = t.Name;
                t.Width = 50;
                Thickness margin = t.Margin;
                margin.Top = 10;
                t.Margin = margin;
                PlayerSelectionLabels.Children.Add(t);
            }
        }

        private void SaveGame()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".gofish";
            sfd.FileName = "GoFish.gofish";
            sfd.Filter = "GoFish Game Saves (*.gofish)|*.gofish";
            if (sfd.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(sfd.FileName, FileMode.OpenOrCreate))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, dealer);
                }
            }
        }

        private void LoadGameWindow()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".gofish";
            ofd.Filter = "GoFish Game Saves (*.gofish)|*.gofish";
            Dealer newGame = null;
            if (ofd.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(ofd.FileName, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    newGame = (Dealer)bf.Deserialize(stream);
                }
                LoadGame(newGame);
            }
        }

        private void LoadGame(Dealer newGame)
        {

        }

        private void GoFishTest_Click(object sender, RoutedEventArgs e)
        {
            goFish();
        }
    }
}
