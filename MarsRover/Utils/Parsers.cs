using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Utils
{
    /// <summary>
    /// String parser functions
    /// </summary>
    public static class Parsers
    {
        /// <summary>
        /// To parse location values for rover from user input
        /// </summary>
        /// <param name="location">string location from users input</param>
        /// <returns>returns x,y coordinates and direction as strings</returns>
        public static List<string> ParseLocationForRover(string location)
        {
            return location.Split(' ').ToList();
        }

        /// <summary>
        /// To parse NASA directives from user input
        /// </summary>
        /// <param name="directives">string directives from users input</param>
        /// <returns>returns directives as char array</returns>
        public static char[] ParseDirectivesForRover(string directives)
        {
            return directives.ToCharArray();
        }
    }
}
