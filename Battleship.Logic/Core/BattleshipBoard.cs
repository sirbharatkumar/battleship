using Battleship.Logic.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Logic
{
    public class BattleshipBoard
    {
        #region Private & Public Members
        public BoardCell[,] Board { get; set; }
        public Dictionary<int, Battleship> BattleShips { get; set; }
        public IReporter ReportTool { get; set; }
        public bool IsBoardReadyToPlay { get; set; }
        #endregion

        public BattleshipBoard()
        {
            CreateBattleshipBoard();
        }

        /// <summary>
        /// Create the battleship board with the given size of the ship
        /// </summary>
        public void CreateBattleshipBoard()
        {
            BattleShips = new Dictionary<int, Battleship>();
            Board = new BoardCell[ApplicationConstants.BattleshipBoardSize, ApplicationConstants.BattleshipBoardSize];
            IsBoardReadyToPlay = false;
            ReportTool = new Reporter();            // setup default export as console output
            for (var x = 0; x < ApplicationConstants.BattleshipBoardSize; x++)
            {
                for (var y = 0; y < ApplicationConstants.BattleshipBoardSize; y++)
                {
                    Board[x, y] = new BoardCell(0, CoordinateState.INITIAL);
                }
            }
        }

        //Check if the ship is Sunk or not
        public Boolean ToCheckShipIsSunk(Battleship ship)
        {
            if (!ship.IsSunk && ship.Deployment?.Count > 0)
            {
                foreach (Coordinate location in ship.Deployment)
                {
                    if (Board[location.X, location.Y].State != CoordinateState.HIT)
                        return false; // ship.IsSunk didn't change
                }
                ship.IsSunk = true; // A battleship is sunk if it has been hit on all the squares it occupies
            }
            return ship.IsSunk;     // A ghost ship occupies zero squares, it was sunk!
        }

        /// <summary>
        /// Report the status of the Cell
        /// </summary>
        /// <param name="type"></param>
        /// <param name="location"></param>
        public void ReportCellState(string type, Coordinate location)
        {
            BoardCell cell = Board[location.X, location.Y];
            if (cell.ShipNumber > 0)
                ReportTool.WriteLine($" Type: {type} {location} {cell}");
            else
                ReportTool.WriteLine($" Type: {type} {location} ");         // shipNumber <=0 should be treated as exception
        }

        /// <summary>
        /// Take attack and report back the status of the Ship
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public int BoardCellAttacked(Coordinate location)
        {
            if (location.X < 0 || location.X >= ApplicationConstants.BattleshipBoardSize || location.Y < 0 || location.Y >= ApplicationConstants.BattleshipBoardSize)
            {
                ReportTool.WriteLine($" Type: Wrong Position {location} ");
                return -1;
            }

            BoardCell cell = Board[location.X, location.Y];
            Battleship ship;

            switch (cell.State)
            {
                case CoordinateState.OCCUPIED:
                    cell.State = CoordinateState.HIT;
                    ship = BattleShips[cell.ShipNumber];
                    ToCheckShipIsSunk(ship);
                    ReportCellState("Hit", location);
                    break;
                case CoordinateState.HIT:
                    ship = BattleShips[cell.ShipNumber];
                    if (ship.IsSunk)
                        ReportCellState("Missed", location);  // Ship was sunk. 
                    else
                        ReportCellState("Hit", location);     // Hit the previous place, but ship isn't sunk. 
                    break;
                default:
                    ReportCellState("Missed", location);
                    break;
            }
            return cell.ShipNumber;
        }
        
        /// <summary>
        /// Validate and add the Ship to the board
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        public Boolean AddAShipToBoard(Battleship ship)
        {
            if (IsBoardReadyToPlay || ship.ShipNumber <= 0) return false;

            if (BattleShips.ContainsKey(ship.ShipNumber))
            {
                ReportTool.WriteLine("ShipNumber is already in board.");
                return false;
            }

            if (ship.Deployment.Count > ApplicationConstants.BattleshipBoardSize || ship.Deployment.Count <= 0)
            {
                ReportTool.WriteLine("Ship must fit entirely on the board");
                return false;
            }

            if (!ship.IsValidDeployment())
            {
                ReportTool.WriteLine("The ship should be 1-by-n sized");
                return false;
            }

            // check if ship position had been occupied by another ship
            foreach (Coordinate location in ship.Deployment)
                if (Board[location.X, location.Y].State == CoordinateState.OCCUPIED)
                {
                    ReportTool.WriteLine("Ship can't overlap another ship. {location}");
                    return false;
                }

            // put ship on the board
            foreach (Coordinate location in ship.Deployment)
            {
                BoardCell cell = Board[location.X, location.Y];
                cell.State = CoordinateState.OCCUPIED;
                cell.ShipNumber = ship.ShipNumber;
            }

            BattleShips.Add(ship.ShipNumber, ship);
            return true;
        }

        /// <summary>
        /// Check if the ship are Sunk or not
        /// </summary>
        /// <returns></returns>
        public bool CheckAllShipsSunkOnPlayBoard()
        {
            foreach (int shipNumber in BattleShips.Keys)
            {
                if (!BattleShips[shipNumber].IsSunk)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Report the status of the ships on the board
        /// </summary>
        /// <param name="displayShipCoordinates"></param>
        public void ReportBoardState(bool displayShipCoordinates = false)
        {
            for (var x = 0; x < ApplicationConstants.BattleshipBoardSize; x++)
            {
                for (var y = 0; y < ApplicationConstants.BattleshipBoardSize; y++)
                {
                    BoardCell cell = Board[x, y];
                    if (displayShipCoordinates)
                    {
                        ReportTool.Write($"{cell.State.ToString().Substring(0, 3)} ");
                    }
                    else
                    {
                        if (cell.State == CoordinateState.HIT)
                            ReportTool.Write($"||   ");
                        else
                            ReportTool.Write($"{x}{y}   ");
                    }
                }
                ReportTool.WriteLine("");
            }
        }

    }
}
