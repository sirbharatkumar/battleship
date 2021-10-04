using Battleship.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Battleship.UnitTest
{
    [TestClass]
    public class BattleshipPlay
    {
        /// <summary>
        /// Test method to check if the deployed ship is valid or not
        /// Test will give false result as one coordinate is missing
        /// </summary>
        [TestMethod]
        public void BattleshipTest1()
        {
            Battleship.Logic.Battleship ship = new Battleship.Logic.Battleship(1);

            ship.SetupDeployment(new List<(int, int)> { (2, 3), (2, 5), (2, 6), (2, 7) });

            var result = (ship.ShipNumber == 1 && ship.IsValidDeployment());
            Assert.IsFalse(result, $"Battleship: ShipNumber={ship.ShipNumber} IsValidDeployment={result} ");
        }

        /// <summary>
        /// Test Method will give true as its a valid case
        /// </summary>
        [TestMethod]
        public void BattleshipTest2()
        {

            Battleship.Logic.Battleship ship = new Battleship.Logic.Battleship(2);
            ship.SetupDeployment(new List<(int, int)> { (3, 1), (3, 2), (3, 3), (3, 4) });

            var result = (ship.ShipNumber == 2 && ship.IsValidDeployment());
            Assert.IsTrue(result, $"Battleship: ShipNumber={ship.ShipNumber} IsValidDeployment={result} ");

        }

        /// <summary>
        /// Test Method will give false as its an overlap scenario
        /// </summary>
        [TestMethod]
        public void BattleshipTest3()
        {

            Battleship.Logic.Battleship ship = new Battleship.Logic.Battleship(3);
            ship.SetupDeployment(new List<(int, int)> { (3, 1), (3, 2), (4, 3), (3, 4) });

            var result = (ship.ShipNumber == 3 && ship.IsValidDeployment());
            Assert.IsFalse(result, $"Battleship: ShipNumber={ship.ShipNumber} IsValidDeployment={result} ");

        }

        /// <summary>
        /// Test method will give true as all the ships have been attacked
        /// </summary>
        [TestMethod]
        public void IsWonGameTest()
        {
            Player player = new Player();

            player.CreateBoard();
            Battleship.Logic.Battleship ship = new Battleship.Logic.Battleship(4);
            ship.SetupDeployment(new List<(int, int)> { (3, 1), (3, 2), (3, 3), (3, 4), (3, 5) });

            player.PlayBoard.AddAShipToBoard(ship);
            player.PlayBoard.IsBoardReadyToPlay = true;

            var result = player.PlayBoard.IsBoardReadyToPlay;

            if (result)
            {
                // [2-5,4]  should be shipNumber 4
                result = player.TakeAnAttack(3, 1) == 4
                         && player.TakeAnAttack(3, 2) == 4
                         && player.TakeAnAttack(3, 3) == 4
                         && player.TakeAnAttack(3, 4) == 4
                         && player.TakeAnAttack(3, 5) == 4;
            }

            if (result)
            {
                // Hit and lost
                result = player.IsWonGame();
            }

            Assert.IsTrue(result, "Won");
        }

        /// <summary>
        /// Test method will return false as all the ships were not destroyed.
        /// </summary>
        [TestMethod]
        public void IsLostGameTest()
        {
            Player player = new Player();

            player.CreateBoard();
            Battleship.Logic.Battleship ship = new Battleship.Logic.Battleship(4);
            ship.SetupDeployment(new List<(int, int)> { (3, 1), (3, 2), (3, 3), (3, 4), (3, 5) });

            player.PlayBoard.AddAShipToBoard(ship);
            player.PlayBoard.IsBoardReadyToPlay = true;

            var result = player.PlayBoard.IsBoardReadyToPlay;

            if (result)
            {
                // [2-5,4]  should be shipNumber 4
                result = player.TakeAnAttack(3, 1) == 4
                         && player.TakeAnAttack(3, 2) == 4
                         && player.TakeAnAttack(3, 3) == 4
                         && player.TakeAnAttack(3, 4) == 4;
            }

            if (result)
            {
                // Hit and lost
                result = player.IsWonGame();
            }

            Assert.IsFalse(result, "Lost");
        }
    }
}
