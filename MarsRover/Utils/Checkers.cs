using MarsRover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover.Utils
{
    /// <summary>
    /// Checker functions for inputs
    /// </summary>
    public static class Checkers
    {
        //Global directions
        private static string[] _directions = new string[] { "E", "W", "N", "S" };

        //NASA directives
        private static char[] _moves = new char[] { 'L', 'M', 'R' };

        /// <summary>
        /// To check if input is valid for desired type
        /// </summary>
        /// <param name="input">input coming from user.</param>
        /// <param name="desiredInputType">which type of input do we want in input</param>
        /// <returns>returns boolean if input is valid and desired type</returns>
        public static bool CheckInput(string input, InputType desiredInputType)
        {
            try
            {

                if (desiredInputType == InputType.DIRECTIVE) //validates while system awaits for valid directives input
                {
                    List<char> directives = Parsers.ParseDirectivesForRover(input).Distinct().ToList();

                    if (directives.Count < 1)
                        return false;

                    foreach (var directive in directives)
                    {
                        bool isValidDirective = _moves.Any(d => d == directive);
                        if (!isValidDirective)
                            return false;
                    }


                }
                else if (desiredInputType == InputType.LOCATION) //validates while system awaits for valid location input
                {
                    List<string> values = Parsers.ParseLocationForRover(input);

                    int coordinateX = Convert.ToInt32(values[0]);
                    int coordinateY = Convert.ToInt32(values[1]);

                    if (coordinateX < 0) return false;
                    if (coordinateY < 0) return false;

                    bool isValidDirection = _directions.Any(d => d == values[2]);

                    if (!isValidDirection) return false;

                    return true;
                }
                else if (desiredInputType == InputType.MAX_DISTANCE) //validates while system awaits for valid max X distance for Plateau
                {
                    int maxDistance = Convert.ToInt32(input);

                    if (maxDistance < 0)
                        return false;

                    return true;
                }
            }
            catch //besides unvalid inputs, this catches the error if input location coordinates are not integer.
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// To check if user wants rovers to start moving
        /// </summary>
        /// <param name="input">string value</param>
        /// <returns>booelan</returns>
        public static bool CheckIfUserWantsToStart(string input)
        {
            if (input == "start")
                return true;
            return false;
        }

        /// <summary>
        /// To check if user wants to exit application
        /// </summary>
        /// <param name="input">string value</param>
        /// <returns>boolean</returns>
        public static bool CheckIfUserWantsToExit(string input)
        {
            if (input == "exit")
                return true;
            return false;
        }
    }
}
