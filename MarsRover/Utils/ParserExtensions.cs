using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Utils
{
    public static class ParserExtensions
    {
        public static List<string> ParseLocation(this string location)
        {
            return location.Split(' ').ToList();
        }

        public static char[] ParseDirectives(this string directives)
        {
            return directives.ToCharArray();
        }
    }
}
