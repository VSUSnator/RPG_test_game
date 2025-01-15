using System;
using System.Collections.Generic;
using TextRPGGame.GamePlay;

namespace TextRPGGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.Start();
        }
    }
}