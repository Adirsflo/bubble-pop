using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BubblePop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> _buttons = new List<Button>();
        private int remainingGreenBubbles = 8;

        private DispatcherTimer gameTimer = new DispatcherTimer();
        private DateTime startTime;

        public MainWindow()
        {
            InitializeComponent();


            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Button button = new Button();
                    // Konfigurera knappen
                    myGrid.Children.Add(button);
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                    _buttons.Add(button);

                    button.Style = (Style)FindResource("RoundButtonStyle");

                    GenerateRandomGreenButtons();

                }
            }
            foreach (Button _button in _buttons)
            {
                _button.Click += Button_Click;
            }

            StartGame();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (clickedButton.Background == Brushes.Green)
            {
                // Knappen är en grön bubbla
                clickedButton.Background = Brushes.White;
                // Uppdatera antalet kvarvarande gröna bubblor
                UpdateRemainingGreenBubbles();
            }
        }

        private void GenerateRandomGreenButtons()
        {
            List<int> randomIndices = new List<int>();
            Random rndm = new Random();

            while (randomIndices.Count < 8)
            {
                int index = rndm.Next(0, _buttons.Count);

                if (!randomIndices.Contains(index))
                {
                    randomIndices.Add(index);
                    _buttons[index].Background = Brushes.Green;
                }
            }
        }

        private void UpdateRemainingGreenBubbles()
        {
            remainingGreenBubbles--;

            if (remainingGreenBubbles == 0)
            {
                // Alla bubblor har poppats
                StopGameTimer();
                ShowWinMessage();
            }
        }

        private void StopGameTimer()
        {
            gameTimer.Stop();
        }

        private void ShowWinMessage()
        {
            MessageBox.Show("Du har poppat alla bubblor! Du vann!");
        }

        private void InitializeGameTimer()
        {
            gameTimer.Interval = TimeSpan.FromSeconds(1);
            gameTimer.Tick += GameTimer_Tick;
        }

        private void StartGameTimer()
        {
            startTime = DateTime.Now;
            gameTimer.Start();
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            // Visa tiden på spelgränssnittet
            timerLabel.Content = elapsedTime.ToString(@"mm\\:ss");
        }

        private void StartGame()
        {
            InitializeGameTimer();
            StartGameTimer();
        }
    }
}
