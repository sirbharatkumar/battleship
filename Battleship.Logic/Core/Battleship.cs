using Battleship.Logic.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Logic
{
    public class Battleship
    {

        #region Private & Public Members
        // Instantiate random number generator.  
        private readonly Random _random = new Random();
        public int ShipNumber { get; set; }
        public Boolean IsSunk { get; set; }
        public List<Coordinate> Deployment { get; set; }
        #endregion

        #region Constructor
        public Battleship(int shipNumber = 1)
        {
            ShipNumber = shipNumber;
            IsSunk = false;
            Deployment = new List<Coordinate>();
        }
        #endregion

        #region Ship Deployment
        /// <summary>
        /// Deploy ship using the list of coordinates
        /// </summary>
        /// <param name="deployment"></param>
        public void SetupDeployment(List<Coordinate> deployment)
        {
            if (deployment?.Count > 0)
                Deployment = deployment;
        }

        /// <summary>
        /// Deploy ship randomly
        /// </summary>
        public void SetupRandomDeployment()
        {
            int randomCoordinate1 = RandomNumber(0, 10);
            int randomCoordinate2 = RandomNumber(0, 5);

            Orientation orientation = GetRandomOrientation();
            if (orientation == Orientation.VERTICAL)
            {
                for (int i = 0; i < 5; i++)
                {
                    Deployment.Add(new Coordinate(randomCoordinate1, i + randomCoordinate2));
                }
            }
            else if (orientation == Orientation.HORIZONTAL)
            {
                for (int i = 0; i < 5; i++)
                {
                    Deployment.Add(new Coordinate(i + randomCoordinate2, randomCoordinate1));
                }
            }

        }

        /// <summary>
        /// Deploy ship based on list of x,y coordinates
        /// </summary>
        /// <param name="deployment"></param>
        public void SetupDeployment(List<(int, int)> deployment)
        {
            if (deployment?.Count > 0)
            {
                foreach ((int x, int y) in deployment)
                {
                    Deployment.Add(new Coordinate(x, y));
                }
            }
        }
        #endregion

        /// <summary>
        /// Validates if the deployment is valid before even the game progress
        /// </summary>
        /// <returns></returns>
        public bool IsValidDeployment()
        {
            if (ShipNumber == 0) return false;

            if (Deployment.Count == 0) return false;

            int x = Deployment[0].X;
            int y = Deployment[0].Y;
            int minX = x;
            int maxX = x;
            int minY = y;
            int maxY = y;

            foreach (Coordinate location in Deployment)
            {
                if (minX > location.X) minX = location.X;
                if (maxX < location.X) maxX = location.X;
                if (minY > location.Y) minY = location.Y;
                if (maxY < location.Y) maxY = location.Y;
            }

            // Make sure the ships is 1-by-n sized, must be aligned either vertically or horizontally.
            if (maxX == minX && (maxY - minY + 1) == Deployment.Count) return true;
            if (maxY == minY && (maxX - minX + 1) == Deployment.Count) return true;

            return false;
        }

        /// <summary>
        /// Generates a random number within a range.      
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Get the random Orientation
        /// </summary>
        /// <returns></returns>
        public static Orientation GetRandomOrientation()
        {
            Array values = Enum.GetValues(typeof(Orientation));
            Random random = new Random();
            Orientation randomOrientation = (Orientation)values.GetValue(random.Next(values.Length));
            return randomOrientation;
        }
    }
}