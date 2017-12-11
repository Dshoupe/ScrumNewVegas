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
        int playerGuessValue = -1;
        bool gameOver = false;
        public GoFishControl()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you for choosing Go Fish! To play please click on your card you wish to fish for, then click on the player's name to fish for your selected card." +
                " Points will be awarded for each pair that you get. If you must go fish and fish a pair you will be given a second turn.");
            SetUpPanel.Visibility = Visibility.Hidden;

            dealer = new Dealer();
            players = CreatePlayers(PlayersSlider.Value);
            int count = 0;
            MessageBox.Show($"Please give the computer to the first player.");
            foreach (GoFishPlayer p in players)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;



                Thickness margin = sp.Margin;
                margin.Top = 20;
                sp.Margin = margin;

                Label scoreCard = new Label();
                margin = scoreCard.Margin;
                margin.Top = 10;
                margin.Bottom = 10;
                scoreCard.Margin = margin;
                scoreCard.Width = 50;
                scoreCard.Content = $"Pairs: {p.Score}";
                scoreCard.Background = new SolidColorBrush(Colors.DeepSkyBlue);
                scoreCard.Foreground = new SolidColorBrush(Colors.White);
                sp.Children.Add(scoreCard);


                p.Hand = dealer.DealFiveCards();
                Label l = new Label();
                margin = l.Margin;
                margin.Top = 10;
                margin.Bottom = 10;
                l.Margin = margin;
                l.Width = 100;
                TextBox t = (TextBox)PlayerSelectionLabels.Children[count];
                l.Background = new SolidColorBrush(Colors.DeepSkyBlue);
                l.Foreground = new SolidColorBrush(Colors.White);
                l.FontSize = 20;
                l.MouseLeftButtonDown += PlayerTarget_MouseLeftButtonDown;
                l.Content = t.Text;
                p.Name = l.Content.ToString();

                sp.Children.Add(l);

                for (int i = 0; i < p.Hand.Count(); i++)
                {
                    if (count != 0)
                    {
                        p.Hand[i].Flip();
                    }
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
                    label.MouseLeftButtonDown += PlayerGuess_MouseLeftButtonDown;
                    sp.Children.Add(label);
                }


                PlayerField.Children.Add(sp);
                checkForPairs(p, count++);
            }
        }

        private void PlayerGuess_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = (StackPanel)PlayerField.Children[turnOrder % (int)PlayersSlider.Value];
            for (int i = 2; i < sp.Children.Count; i++)
            {
                ((Label)sp.Children[i]).Background = null;
            }
            int index = sp.Children.IndexOf((Label)sender) - 2;
            if (index >= 0 && players[turnOrder % (int)PlayersSlider.Value].Hand.Count() > 0)
            {
                int temp = (int)players[turnOrder % (int)PlayersSlider.Value].Hand[index].FaceValue;
                if (temp >= 0 )
                {
                    playerGuessValue = temp;
                    ((Label)sender).Background = new SolidColorBrush(Colors.Red);
                }
            }
        }

        private void PlayerTarget_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string name = ((Label)sender).Content.ToString();
            if (playerGuessValue != -1 && name != players[turnOrder % (int)PlayersSlider.Value].Name)
            {
                Player p = players.Where(x => x.Name == name).First();
                bool goodGuess = false;
                MessageBox.Show($"Do you have any {(Face)playerGuessValue}'s");
                for (int i = 0; i < p.Hand.Count && !goodGuess; i++)
                {
                    if ((int)p.Hand[i].FaceValue == playerGuessValue)
                    {
                        players[turnOrder % (int)PlayersSlider.Value].Hand.Add(p.Hand[i]);
                        players[turnOrder % (int)PlayersSlider.Value].Hand.Last().Flip();
                        StackPanel sp = (StackPanel)PlayerField.Children[turnOrder % (int)PlayersSlider.Value];
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
                        label.MouseLeftButtonDown += PlayerGuess_MouseLeftButtonDown;
                        sp.Children.Add(label);
                        MessageBox.Show($"You Stole my {p.Hand[i].FaceValue}");
                        checkForPairs(players[turnOrder % (int)PlayersSlider.Value], turnOrder % (int)PlayersSlider.Value);
                        int playerIndex = 0;
                        for (int k = 0; k < players.Count(); k++)
                        {
                            if (players[k] == p)
                            {
                                playerIndex = k;
                            }
                        }
                        StackPanel secondPlayerSp = (StackPanel)PlayerField.Children[playerIndex];
                        secondPlayerSp.Children.RemoveAt(i + 2);
                        p.Hand.Remove(p.Hand[i]);

                        ChangeActivePlayer();
                        goodGuess = true;
                        playerGuessValue = -1;
                    }
                }
                if (!goodGuess)
                {
                    MessageBox.Show($"GO FISH!");
                    goFish();
                }
            }
        }

        private void goFish()
        {
            StackPanel sp = (StackPanel)PlayerField.Children[turnOrder % (int)PlayersSlider.Value];
            Card c = dealer.dealOneCard();
            if (c != null)
            {
                players[turnOrder % (int)PlayersSlider.Value].Hand.Add(c);
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
                label.MouseLeftButtonDown += PlayerGuess_MouseLeftButtonDown;
                sp.Children.Add(label);
                int previousScore = players[turnOrder % (int)PlayersSlider.Value].Score;
                checkForPairs(players[turnOrder % (int)PlayersSlider.Value], turnOrder % (int)PlayersSlider.Value);
                int newScore = players[turnOrder % (int)PlayersSlider.Value].Score;
                if (previousScore < newScore)
                {
                    MessageBox.Show($"{players[turnOrder % (int)PlayersSlider.Value].Name} fished what they wanted. They get another turn!");
                    playerGuessValue = -1;
                    for (int i = 2; i < sp.Children.Count; i++)
                    {
                        ((Label)sp.Children[i]).Background = null;
                    }
                    if (players[turnOrder % (int)PlayersSlider.Value].Hand.Count() == 0)
                    {
                        if (dealer.dealerDeck.Cards.Count > 0)
                        {
                            players[turnOrder % (int)PlayersSlider.Value].Hand = dealer.DealFiveCards();
                            reprintHand();
                        }
                        else
                        {
                            MessageBox.Show("There are not cards left in the deck to draw. Your turn has been skipped.");
                            ChangeActivePlayer();
                        }
                    }
                }
                else
                {
                    playerGuessValue = -1;
                    ChangeActivePlayer();
                }
            }
            else
            {
                ChangeActivePlayer();
            }
            
        }

        private void ChangeActivePlayer()
        {
            if (totalCardCount == 0)
            {
                List<GoFishPlayer> winningPlayers = new List<GoFishPlayer>();
                winningPlayers.Add(new GoFishPlayer());
                foreach (GoFishPlayer p in players)
                {
                    if (p.Score > winningPlayers[0].Score)
                    {
                        winningPlayers.Clear();
                        winningPlayers.Add(p);
                    }else if (p.Score == winningPlayers[0].Score)
                    {
                        winningPlayers.Add(p);
                    }
                }
                string names = "";
                foreach (GoFishPlayer winner in winningPlayers)
                {
                    names += $"{winner.Name} ";
                }
                MessageBox.Show($"{names} won the game with a score of {winningPlayers[0].Score}!! Please press Ctrl + B to return to the main menu");
                PlayerField.Children.Clear();
                gameOver = true;
            }
            if (!gameOver)
            {
                int count = 1;
                StackPanel sp = (StackPanel)PlayerField.Children[turnOrder % (int)PlayersSlider.Value];
                foreach (Card c in players[turnOrder % (int)PlayersSlider.Value].Hand)
                {
                    c.Flip();
                    Label label = (Label)sp.Children[++count];
                    label.Background = null;
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
                MessageBox.Show($"It is {players[turnOrder % (int)PlayersSlider.Value].Name}'s turn. Please pass the computer.");
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
                if (players[turnOrder % (int)PlayersSlider.Value].Hand.Count() == 0)
                {
                    if (dealer.dealerDeck.Cards.Count > 0)
                    {
                        players[turnOrder % (int)PlayersSlider.Value].Hand = dealer.DealFiveCards();
                        reprintHand();
                    }
                    else
                    {
                        MessageBox.Show("There are not cards left in the deck to draw. Your turn has been skipped.");
                        ChangeActivePlayer();
                    }
                }
            }
        }

        private void reprintHand()
        {
            StackPanel sp = (StackPanel)PlayerField.Children[turnOrder % (int)PlayersSlider.Value];
            Player p = players[turnOrder % (int)PlayersSlider.Value];
            for (int i = 0; i < p.Hand.Count(); i++)
            {
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

                Thickness margin = label.Margin;
                margin.Right = 10;
                margin.Left = 10;
                label.Margin = margin;
                label.Width = 50;
                label.MouseLeftButtonDown += PlayerGuess_MouseLeftButtonDown;
                sp.Children.Add(label);
            }
        }

        private void checkForPairs(GoFishPlayer player, int playerNum)
        {
            bool hasPair = false;
            for (int i = 0; i < (player.Hand.Count - 1); i++)
            {
                for (int j = (i + 1); j < player.Hand.Count && !hasPair; j++)
                {
                    if (player.Hand[i].FaceValue == player.Hand[j].FaceValue)
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

                        ((Label)sp.Children[0]).Content = $"Pairs: {player.Score}"; ;

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

        public void SaveGame()
        {
            GoFishSave save = new GoFishSave() { Dealer = dealer, Players = players.ToList(), TotalCardCount = totalCardCount, TurnOrder = turnOrder};
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".gofish";
            sfd.FileName = "GoFish.gofish";
            sfd.Filter = "GoFish Game Saves (*.gofish)|*.gofish";
            if (sfd.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(sfd.FileName, FileMode.OpenOrCreate))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, save);
                }
            }
        }

        public void LoadGameWindow()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".gofish";
            ofd.Filter = "GoFish Game Saves (*.gofish)|*.gofish";
            GoFishSave newGame = null;
            if (ofd.ShowDialog() == true)
            {
                using (FileStream stream = new FileStream(ofd.FileName, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    newGame = (GoFishSave)bf.Deserialize(stream);
                }
                LoadGame(newGame);
            }
        }

        private void LoadGame(GoFishSave newGame)
        {
            dealer = newGame.Dealer;
            turnOrder = newGame.TurnOrder;
            totalCardCount = newGame.TotalCardCount;
            players = new GoFishPlayer[newGame.Players.Count];
            PlayersSlider.Value = players.Count();
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = newGame.Players[i];
            }
            SetUpPanel.Visibility = Visibility.Hidden;
            int count = 0;
            PlayerField.Children.Clear();
            foreach (GoFishPlayer p in players)
            {
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;



                Thickness margin = sp.Margin;
                margin.Top = 20;
                sp.Margin = margin;

                Label scoreCard = new Label();
                margin = scoreCard.Margin;
                margin.Top = 10;
                margin.Bottom = 10;
                scoreCard.Margin = margin;
                scoreCard.Width = 50;
                scoreCard.Content = $"Pairs: {p.Score}";
                scoreCard.Background = new SolidColorBrush(Colors.DeepSkyBlue);
                scoreCard.Foreground = new SolidColorBrush(Colors.White);
                sp.Children.Add(scoreCard);


                p.Hand = dealer.DealFiveCards();
                Label l = new Label();
                margin = l.Margin;
                margin.Top = 10;
                margin.Bottom = 10;
                l.Margin = margin;
                l.Width = 100;
                l.Background = new SolidColorBrush(Colors.DeepSkyBlue);
                l.Foreground = new SolidColorBrush(Colors.White);
                l.FontSize = 20;
                l.MouseLeftButtonDown += PlayerTarget_MouseLeftButtonDown;
                l.Content = players[count].Name;
                p.Name = l.Content.ToString();

                sp.Children.Add(l);

                for (int i = 0; i < p.Hand.Count(); i++)
                {
                    if (count != turnOrder % players.Count())
                    {
                        p.Hand[i].Flip();
                    }
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
                    label.MouseLeftButtonDown += PlayerGuess_MouseLeftButtonDown;
                    sp.Children.Add(label);
                }


                PlayerField.Children.Add(sp);
                checkForPairs(p, count++);
            }
        }

    }
}
