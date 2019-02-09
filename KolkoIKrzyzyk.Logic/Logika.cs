using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolkoIKrzyzyk.Logic
{
    public class Logika
    {
        public Logika() { }
        private int amountOfMoves = 0;
        //liczenie ruchów
        public int AmountOfMoves
        {
            get { return amountOfMoves; }
            set { amountOfMoves = value; }
        }

        private int currentPlayer = 1;
        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }
        //bool wygrana albo nie
        private bool gameOver;
        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }
        public bool IsPlayerOneTurn
        {
            get { return AmountOfMoves % 2 == 0; }
        }
        private int[] pressedButtons = new int[25];
        public int[] PressedButtons
        {
            get { return pressedButtons; }
            set { pressedButtons = value; }
        }
        private bool WinningCombinationFound(int left1, int left2, int middle, int right2, int right1)
        {
            return (PressedButtons[left1] == CurrentPlayer &&
                PressedButtons[left2] == CurrentPlayer &&
                PressedButtons[middle] == CurrentPlayer &&
                PressedButtons[right2] == CurrentPlayer &&
                PressedButtons[right1] == CurrentPlayer);
        }
        private bool GameWinnerFound()
        {
            //poziomo
            if (WinningCombinationFound(0, 1, 2, 3, 4)) return true;
            if (WinningCombinationFound(5, 6, 7, 8, 9)) return true;
            if (WinningCombinationFound(10, 11, 12, 13, 14)) return true;
            if (WinningCombinationFound(15, 16, 17, 18, 19)) return true;
            if (WinningCombinationFound(20, 21, 22, 23, 24)) return true;
            //pionowo
            if (WinningCombinationFound(0, 5, 10, 15, 20)) return true;
            if (WinningCombinationFound(1, 6, 11, 16, 21)) return true;
            if (WinningCombinationFound(2, 7, 12, 17, 22)) return true;
            if (WinningCombinationFound(3, 8, 13, 18, 23)) return true;
            if (WinningCombinationFound(4, 9, 22, 23, 24)) return true;
            //skos
            if (WinningCombinationFound(0, 6, 12, 18, 24)) return true;
            if (WinningCombinationFound(4, 8, 12, 16, 20)) return true;
            return false;
        }
        public void CheckForGameWinner()
        {
            GameOver = GameWinnerFound();
        }
        //restart
        public void ResetGame()
        {
            AmountOfMoves = 0;
            GameOver = false;
            CurrentPlayer = 1;
            PressedButtons = new int[25];
        }

    }
}
