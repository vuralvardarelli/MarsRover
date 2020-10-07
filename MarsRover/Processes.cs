using MarsRover.Models;
using MarsRover.Utils;
using System;
using System.Collections.Generic;

namespace MarsRover
{
    /// <summary>
    /// Main processes to run.
    /// </summary>
    public class Processes
    {
        List<Rover> _rovers;

        //if user wants to start moving rovers
        private bool _startReceived = false;

        //if user wants to exit application
        private bool _exitReceived = false;

        public Processes()
        {
            _rovers = new List<Rover>();
        }

        public void Init()
        {

            AddRovers();

            MoveRovers();

            GetLastLocations();

        }

        /// <summary>
        /// First, tries until if user input is valid for current location of rover.
        /// Then, tries until if user input is valid for NASA directives.
        /// When both are valid , adds rover to list.
        /// Until user types 'start' or 'exit' , repeats the process and can add countless rovers if it is desired.
        /// </summary>
        private void AddRovers()
        {
            while (!_startReceived)
            {
                string location = "";

                //try until get first input is valid and location for the rover
                while (!Checkers.CheckInput(location, InputType.LOCATION))
                {
                    location = Console.ReadLine();

                    _startReceived = Checkers.CheckIfUserWantsToStart(location);
                    _exitReceived = Checkers.CheckIfUserWantsToExit(location);

                    if (_startReceived || _exitReceived)
                        break;
                }

                if (_startReceived)
                    continue;

                if (_exitReceived)
                    ExitApplication();

                //first input values to add rover's properties
                List<string> locationValues = Parsers.ParseLocationForRover(location);

                string directives = "";

                //try until get second input is valid and directives for the rover
                while (!Checkers.CheckInput(directives, InputType.DIRECTIVE))
                {
                    //second input value to add rover's properties
                    directives = Console.ReadLine();

                    _startReceived = Checkers.CheckIfUserWantsToStart(directives);
                    _exitReceived = Checkers.CheckIfUserWantsToExit(directives);

                    if (_startReceived || _exitReceived)
                        break;
                }

                if (_startReceived)
                    continue;

                if (_exitReceived)
                    ExitApplication();

                _rovers.Add(new Rover()
                {
                    CoordinateX = Convert.ToInt32(locationValues[0]),
                    CoordinateY = Convert.ToInt32(locationValues[1]),
                    Direction = locationValues[2],
                    Directives = directives
                });
            }
        }

        /// <summary>
        /// If list has rovers inside , starts to move them 1 by 1,
        /// else clears console and lets user start again.
        /// </summary>
        private void MoveRovers()
        {
            if (_rovers.Count > 0) //if list has valid rover/rovers
            {
                //move rovers sequentially
                foreach (var rover in _rovers)
                {
                    char[] directives = Parsers.ParseDirectivesForRover(rover.Directives);

                    foreach (var directive in directives)
                    {
                        ChangeRoverValues(rover, directive);
                    }

                }
            }
            else //if list has no valid rover.
            {
                Console.WriteLine("You have no rovers with valid values. Please hit 'Enter' to start again.");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                { }
                Console.Clear();
                _startReceived = false;
                Init();
            }
        }

        /// <summary>
        /// Writes console rovers' last location and direction.
        /// </summary>
        private void GetLastLocations()
        {
            if (_rovers.Count > 0)
                foreach (var rover in _rovers)
                {
                    if(rover.CoordinateX<0 || rover.CoordinateY < 0)
                    {
                        Console.WriteLine($"{rover.CoordinateX} {rover.CoordinateY} {rover.Direction}  <-- Probably crashed.. Not enough information on documentation.");
                    }
                    else
                    {
                        Console.WriteLine($"{rover.CoordinateX} {rover.CoordinateY} {rover.Direction}");
                    }

                    
                }
        }

        /// <summary>
        /// Uses NASA directives to move the rover
        /// </summary>
        /// <param name="rover">current rover to move</param>
        /// <param name="directive">tells rover where to go</param>
        private void ChangeRoverValues(Rover rover, char directive)
        {
            if (directive == 'L')
            {
                if (rover.Direction == "N")
                {
                    rover.Direction = "W";
                }
                else if (rover.Direction == "W")
                {
                    rover.Direction = "S";
                }
                else if (rover.Direction == "E")
                {
                    rover.Direction = "N";
                }
                else if (rover.Direction == "S")
                {
                    rover.Direction = "E";
                }
            }
            else if (directive == 'R')
            {
                if (rover.Direction == "N")
                {
                    rover.Direction = "E";
                }
                else if (rover.Direction == "W")
                {
                    rover.Direction = "N";
                }
                else if (rover.Direction == "E")
                {
                    rover.Direction = "S";
                }
                else if (rover.Direction == "S")
                {
                    rover.Direction = "W";
                }
            }
            else if (directive == 'M')
            {
                if (rover.Direction == "N")
                {
                    rover.CoordinateY++;
                }
                else if (rover.Direction == "W")
                {
                    rover.CoordinateX--;
                }
                else if (rover.Direction == "E")
                {
                    rover.CoordinateX++;
                }
                else if (rover.Direction == "S")
                {
                    rover.CoordinateY--;
                }
            }
        }

        /// <summary>
        /// Lets user exit the app by typing 'exit'
        /// </summary>
        private void ExitApplication()
        {
            Console.WriteLine("You are not exiting from application. Press any key..");
            Console.ReadKey();
            Environment.Exit(0);
        }


    }
}
