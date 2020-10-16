using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Log
{
    public interface IConsole
    {
        void WritelineToConsole(string text);

        ConsoleKeyInfo ReadKey();

        string ReadLine();

        void ClearConsole();

        void ExitConsole();
    }
}
