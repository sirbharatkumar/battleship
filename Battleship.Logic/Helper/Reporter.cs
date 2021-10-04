using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Logic
{
    public interface IReporter
    {
        void Write(string log);
        void WriteLine(string log);

    }
    public class Reporter : IReporter
    {
        /// <summary>
        /// This can be extended to log the status into a log file
        /// </summary>
        /// <param name="log"></param>
        public void Write(string log)
        {
            Console.Write(log);
        }

        public void WriteLine(string log)
        {
            Console.WriteLine(log);
        }
    }
}
