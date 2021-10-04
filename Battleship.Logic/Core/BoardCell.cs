using System;
using System.Collections.Generic;

namespace Battleship.Logic
{
    public class BoardCell
    {
        public int ShipNumber { get; set; }
        public CoordinateState State { get; set; }

        /// <summary>
        /// Set the State for the each cell for a give Ship number
        /// </summary>
        /// <param name="shipNumber"></param>
        /// <param name="state"></param>
        public BoardCell(int shipNumber, CoordinateState state)
        {
            ShipNumber = shipNumber;
            State = state;
        }
        public override string ToString()
        {
            return ($"BoardCell : {{ ShipNumber = {ShipNumber}, State = {State} }}");
        }
    }
}
