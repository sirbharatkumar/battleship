using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Logic
{
    /// <summary>
    /// Ship Orientation
    /// </summary>
    public enum Orientation
    {
        VERTICAL = 0,
        HORIZONTAL = 1
    }

    /// <summary>
    /// Ship State if its Initial, Occupied or while attacked it its Hit
    /// </summary>
    public enum CoordinateState
    {
        INITIAL,
        OCCUPIED,
        HIT,
    }
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return ($"Coordinate: X={X} Y={Y}");
        }
    }
}