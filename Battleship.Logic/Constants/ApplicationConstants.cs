using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Logic.Constants
{
    /// <summary>
    /// Application constants are defined at one place for reference
    /// </summary>
    public static class ApplicationConstants
    {
        public static int ErrorValue => -999;
        public static int BattleshipBoardSize => 10;
        public static int NumberOfAttemptsAllowed => 10;
        public static int ShipSize => 5;
    }
}
