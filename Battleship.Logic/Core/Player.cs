using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Logic
{
    public class Player
    {
        public string Name { get; set; }
        public BattleshipBoard PlayBoard { get; set; }
        public IReporter ReportTool { get; set; }

        /// <summary>
        /// Initialize the board
        /// </summary>
        public Player()
        {
            Name = "Flare Battleship";
            ReportTool = new Reporter();
            CreateBoard();
        }

        public void CreateBoard()
        {
            PlayBoard = new BattleshipBoard();
            PlayBoard.ReportTool = ReportTool;
        }

        /// <summary>
        /// Take attack using the Coordinates
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int TakeAnAttack(Coordinate position)
        {
            return PlayBoard.BoardCellAttacked(position);
        }

        /// <summary>
        /// Take attack using the x,y passed
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int TakeAnAttack(int x, int y)
        {
            var position = new Coordinate(x, y);
            return PlayBoard.BoardCellAttacked(position);
        }

        /// <summary>
        /// Report back if the player won
        /// </summary>
        /// <returns></returns>
        public bool IsWonGame()
        {
            return (PlayBoard.IsBoardReadyToPlay && PlayBoard.CheckAllShipsSunkOnPlayBoard());
        }

        /// <summary>
        /// Report the status of the board
        /// </summary>
        /// <param name="displayShipCoordinates"></param>
        public void ReportPlayBoardState(bool displayShipCoordinates = false)
        {
            if (PlayBoard.IsBoardReadyToPlay)
                PlayBoard.ReportBoardState(displayShipCoordinates);
            else
                ReportTool.WriteLine("Playboard is not ready yet.");
        }

    }
}
