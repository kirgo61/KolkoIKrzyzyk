using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KolkoIKrzyzyk.Logic;

namespace KolkoIKrzyzyk.Test
{
    [TestClass]
    public class Testy
    {
        [TestMethod]
        public void CheckForGameWinner_NoWinner_ReturnsFalse()
        {
            // Arrange
            Logika gameLogic = new Logika();
            gameLogic.CurrentPlayer = 1;
            gameLogic.PressedButtons = new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 2, 1, 1, 1, 2, 2, 1, 2, 2, 1, 1, 1, 1, 2, 1, 2, 2 };
            bool gameWon = false;

            // Act
            gameLogic.CheckForGameWinner();

            // Assert
            Assert.AreEqual(gameWon, gameLogic.GameOver);
        }

        [TestMethod]
        public void CheckForGameWinner_ValidWinner_ReturnsTrue()
        {
            // Arrange
            Logika gameLogic = new Logika();
            gameLogic.CurrentPlayer = 1;
            gameLogic.PressedButtons = new int[] { 1, 1, 2, 0, 0, 0, 1, 2, 1, 0, 2, 1, 2, 2, 1, 2, 1, 2, 1, 2, 2, 1, 1, 2, 1 };
            bool gameWon = true;

            // Act
            gameLogic.CheckForGameWinner();

            // Assert
            Assert.AreEqual(gameWon, gameLogic.GameOver);
        }
    }
}
