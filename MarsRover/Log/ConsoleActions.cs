using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Log
{
    public class ConsoleActions : IConsole
    {
        public void ClearConsole()
        {
            Console.Clear();
        }

        public void ExitConsole()
        {
            Environment.Exit(0);
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WritelineToConsole(string text)
        {
            Console.WriteLine(text);
        }
    }
}
