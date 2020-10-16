using MarsRover.Log;
using MarsRover.Models;
using MarsRover.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MarsRover
{
    /// <summary>
    /// Main processes to run.
    /// </summary>
    public class Processes
    {
        private IConsole _console;

        private List<Rover> _rovers;

        //if user wants to start moving rovers
        private bool _startReceived = false;

        //if user wants to exit application
        private bool _exitReceived = false;

        public Processes(IConsole console)
        {
            _rovers = new List<Rover>();
            _console = console;

        }

        public void Init()
        {
            GetPlateau();

            AddRovers();

            MoveRovers();

            GetLastLocations();

        }

        private void GetPlateau()
        {

        }

        /// <summary>
        /// First, tries until if user input is valid for current location of rover.
        /// Then, tries until if user input is valid for NASA directives.
        /// When both are valid , adds rover to list.
        /// Until user types 'start' or 'exit' , repeats the process and can add countless rovers if it is desired.
        /// </summary>
        private void AddRovers()
        {
            _console.WritelineToConsole("\n");
            _console.WritelineToConsole("** If you want to move Rovers , please type 'start' as a new input value and hit 'Enter'.");
            _console.WritelineToConsole("** If you want to close application , please type 'exit' as a new input value and hit 'Enter'.\n");
            

            while (!_startReceived)
            {
                string location = "";

                _console.WritelineToConsole($"{_rovers.Count+1}.Rover's Starting Location Coordinates :\n");

                //try until get first input is valid and location for the rover
                while (!Checkers.CheckInput(location, InputType.LOCATION))
                {
                    location = _console.ReadLine();

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
                //List<string> locationValues = location.ParseLocation();

                string directives = "";

                _console.WritelineToConsole($"\n" +
                    $"{_rovers.Count+1}.Rover's Directives :\n");

                //try until get second input is valid and directives for the rover
                while (!Checkers.CheckInput(directives, InputType.DIRECTIVE))
                {
                    //second input value to add rover's properties
                    directives = _console.ReadLine();

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

                _console.WritelineToConsole($"\n** {_rovers.Count}.Rover added at '{Convert.ToInt32(locationValues[0])} {Convert.ToInt32(locationValues[1])} {locationValues[2]}' with directives : '{directives}'\n");
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
                    //char[] directives = rover.Directives.ParseDirectives();

                    foreach (var directive in directives)
                    {
                        ChangeRoverValues(rover, directive);
                    }

                }
            }
            else //if list has no valid rover.
            {

                _console.WritelineToConsole("You have no rovers with valid values. Please hit 'Enter' to start again.");

                //while (Console.ReadKey().Key != ConsoleKey.Enter)
                //{ }
                while (_console.ReadKey().Key != ConsoleKey.Enter)
                { }


                _console.ClearConsole();
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
            {
                _console.WritelineToConsole("\nOutput:\n");

                int i = 1;

                foreach (var rover in _rovers)
                {
                    if (rover.CoordinateX < 0 || rover.CoordinateY < 0)
                    {
                        _console.WritelineToConsole($"{i}.Rover : {rover.CoordinateX} {rover.CoordinateY} {rover.Direction}  <-- Probably crashed.. Not enough information on documentation.");
                    }
                    else
                    {
                        _console.WritelineToConsole($"{i}.Rover : {rover.CoordinateX} {rover.CoordinateY} {rover.Direction}");
                    }

                    i++;
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
            _console.WritelineToConsole("You are not exiting from application. Press any key..");
            _console.ReadKey();
            _console.ExitConsole();
        }


    }
}
