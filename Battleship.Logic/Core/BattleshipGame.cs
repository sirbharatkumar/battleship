using Battleship.Logic.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Logic
{
    public class BattleshipGame : IDisposable
    {
        // To detect redundant calls
        private bool _disposed = false;

        ~BattleshipGame() => Dispose(false);
        public void Play()
        {

            try
            {
                //Initialize the Player
                Player player = new Player();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("#######  Battleship Game: You Vs Computer #######");
                sb.AppendLine("Game will generate random coordinate and place the ship which will be size of 1X5");
                sb.AppendLine("You have to guess the coordinate and type it when prompted");
                sb.AppendLine("Type the coordinates as {x}{y}. Coordiate example: for X = 3 and Y = 5, type 35. ");
                sb.AppendLine($"Only have {ApplicationConstants.NumberOfAttemptsAllowed} attempts and you will see the countdown after each attempt.");
                sb.AppendLine("If all the coordinates are hit within allowed attempts you WIN or you Lose.");
                sb.AppendLine("Incase incorrect or invalid coordinates are typed, game will be over immediately");
                sb.AppendLine("Hope you enjoy this interactive Battleship game !!");
                player.ReportTool.WriteLine(sb.ToString());

                var shipNumber = 1;
                Battleship ship = new Battleship(shipNumber);

                ship.SetupRandomDeployment(); //Setup the Random ship on the board
                player.PlayBoard.AddAShipToBoard(ship);//Add the random ship to the board
                player.PlayBoard.IsBoardReadyToPlay = true;
                //player.ReportPlayBoardState(true);//This will show the deployed coordinates before playing the game

                int numberOfAttempts = 0, inputCorrdinates = 0;
                string playerInput;
                do
                {
                    numberOfAttempts++;
                    if (numberOfAttempts <= ApplicationConstants.NumberOfAttemptsAllowed)
                    {
                        Console.Write("Enter Coordinates: ");//Enter the coordinates as {x}{y}
                        playerInput = Console.ReadLine();

                        if (int.TryParse(playerInput, out inputCorrdinates))
                        {
                            int x = 0, y = 0;
                            if (!string.IsNullOrEmpty(playerInput))
                            {
                                if (playerInput.Length == 2 || playerInput.Length == 3)
                                {
                                    x = inputCorrdinates / ApplicationConstants.BattleshipBoardSize;
                                    y = inputCorrdinates % ApplicationConstants.BattleshipBoardSize;
                                }
                                else
                                {
                                    player.ReportTool.WriteLine("Valid Coordinates were not supplied.");
                                    break;
                                }
                            }

                            player.TakeAnAttack(x, y);//Take the attack with the input coordinates
                            player.ReportPlayBoardState();//Report the status of the Ship if its Hit or Missed
                            if (player.IsWonGame())//If all the Ships are down in allowed attempts you won
                            {
                                player.ReportTool.WriteLine("You Won!!!");
                                break;
                            }
                        }
                        else
                        {
                            player.ReportTool.WriteLine("Valid Coordinates were not supplied.");
                            break;
                        }
                    }
                    player.ReportTool.WriteLine($" No of attempts left: {ApplicationConstants.NumberOfAttemptsAllowed - numberOfAttempts}");
                }
                while (numberOfAttempts < ApplicationConstants.NumberOfAttemptsAllowed);//Loop untill the number of attempts is less than the allowed

                if (!player.IsWonGame())
                {
                    player.ReportTool.WriteLine("No more moves left. You Lost. Game over!!!");
                    player.ReportTool.WriteLine("Display the ship coordinates !!!");
                    player.ReportPlayBoardState(true);
                }
            }
            catch (Exception ex)
            {
                //Log the error for diagnostics
                Console.WriteLine("Sometime went wrong. Game over!!!");
                
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed objects that implement IDisposable.
                // Assign null to managed objects that consume large amounts of memory or consume scarce resources.
            }

            // Free unmanaged resources (unmanaged objects).

            _disposed = true;
        }
    }
}
