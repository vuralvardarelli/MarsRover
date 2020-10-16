using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Log
{
    public interface IConsole
    {
        /// <summary>
        /// For writing a text line to command prompt
        /// </summary>
        /// <param name="text"></param>
        void WritelineToConsole(string text);

        /// <summary>
        /// For reading a key stroke from command prompt
        /// </summary>
        /// <returns>returns key's info</returns>
        ConsoleKeyInfo ReadKey();

        /// <summary>
        /// For reading a string from command prompt
        /// </summary>
        /// <returns>returns string</returns>
        string ReadLine();

        /// <summary>
        /// For clearing command prompt
        /// </summary>
        void ClearConsole();

        /// <summary>
        /// For exitting from app 
        /// </summary>
        void ExitConsole();
    }
}
