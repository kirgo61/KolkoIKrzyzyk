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
using KolkoIKrzyzyk.Logic;

namespace KolkoIKrzyzyk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string player1Alert = "Tura Gracza 1";
        private const string player2Alert = "Tura Gracza 2";
        private const string draw = "REMIS";
        private const string markX = "X";
        private const string markO = "O";


        private Logika kolkoIKrzyzykLogika = null;
        private Logika KolkoIKrzyzykLogika
        {
            get
            {
                if (kolkoIKrzyzykLogika == null) { kolkoIKrzyzykLogika = new Logika(); }
                return kolkoIKrzyzykLogika;
            }
        }


        private void ButtonClick(object sender, MouseButtonEventArgs e)
        {
            if (KolkoIKrzyzykLogika.GameOver)
            {
                return;
            }
            Rectangle click = e.Source as Rectangle;
            int buttonUid = Convert.ToInt32(click.Uid);

            if (IsValidMove(buttonUid))
            {
                UpdateGameUi(click);
                UpdateGameLogicMap(buttonUid);
                KolkoIKrzyzykLogika.AmountOfMoves++;
                if (KolkoIKrzyzykLogika.AmountOfMoves >= 9)
                {
                    CheckForGameWinner();
                    CheckForGameTie();
                }
                KolkoIKrzyzykLogika.CurrentPlayer = KolkoIKrzyzykLogika.IsPlayerOneTurn ? 1 : 2;
            }
        }


        private void UpdateGameLabelForNextPlayer(string labelMessage, Brush color)
        {
            Info.Content = labelMessage;
            Info.Foreground = color;
        }


        public MainWindow()
        {
            InitializeComponent();
            UpdateGameLabelForNextPlayer(player1Alert, Brushes.Red);
        }

        /// <param name="sender"></param>
		/// <param name="e"></param>
        private void Btn_Reset_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            foreach (Rectangle gamePieces in Container.Children.OfType<Rectangle>())
            {
                gamePieces.Fill = new SolidColorBrush(Colors.Wheat);
            }
            KolkoIKrzyzykLogika.ResetGame();
            UpdateGameLabelForNextPlayer(player1Alert, Brushes.Red);
        }


        private void CheckForGameWinner()
        {
            KolkoIKrzyzykLogika.CheckForGameWinner();

            if (KolkoIKrzyzykLogika.GameOver)
            {
                UpdateGameLabelForNextPlayer(string.Format("Gracz {0} wygrał!", KolkoIKrzyzykLogika.CurrentPlayer), Brushes.DarkGoldenrod);
            }
        }


        private void CheckForGameTie()
        {
            if (KolkoIKrzyzykLogika.AmountOfMoves >= 25 && !KolkoIKrzyzykLogika.GameOver)
            {
                UpdateGameLabelForNextPlayer(draw, Brushes.Black);
                KolkoIKrzyzykLogika.GameOver = true;
            }
        }


        private bool IsValidMove(int clickedRectangleIndex)
        {
            if (KolkoIKrzyzykLogika.PressedButtons[clickedRectangleIndex] == 0)
            {
                return true;
            }
            return false;
        }
        private void UpdateGameUi(Rectangle clickedButton)
        {
            if (KolkoIKrzyzykLogika.IsPlayerOneTurn)
            {
                UpdateButtonFill(clickedButton, markX, new SolidColorBrush(Colors.Red));
                UpdateGameLabelForNextPlayer(player2Alert, Brushes.Blue);
            }
            else
            {
                UpdateButtonFill(clickedButton, markO, new SolidColorBrush(Colors.Blue));
                UpdateGameLabelForNextPlayer(player1Alert, Brushes.Red);
            }
        }
        private void UpdateButtonFill(Rectangle clickedButton, string playerSymbol, Brush color)
        {
            if (clickedButton != null)
            {
                TextBlock tb = new TextBlock();
                tb.FontSize = 72;
                tb.Background = color;
                tb.Text = playerSymbol;
                BitmapCacheBrush bcb = new BitmapCacheBrush(tb);
                clickedButton.Fill = bcb;
            }
        }
        private void UpdateGameLogicMap(int arrayIndex)
        {
            int mapValue = KolkoIKrzyzykLogika.CurrentPlayer;
            KolkoIKrzyzykLogika.PressedButtons[arrayIndex] = mapValue;
        }
    }
}
