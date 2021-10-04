using Battleship.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace SinglePlayerBattleship
{
    class Program
    {
        static void Main(string[] args)
        {
            //Call the Battleship game, create the board and deploy a ship at random location
            using (BattleshipGame battleshipGame = new BattleshipGame())
            {
                battleshipGame.Play();
            }
            Console.ReadLine();
        }
    }
}
