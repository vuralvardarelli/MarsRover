﻿using MarsRover.Log;
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

        private Plateau _plateau;

        private List<Rover> _crushedRovers;

        //if user wants to start moving rovers
        private bool _startReceived = false;

        public Processes(IConsole console)
        {
            _rovers = new List<Rover>();
            _plateau = new Plateau();
            _console = console;
            _crushedRovers = new List<Rover>();

        }

        public string Init()
        {
            GetPlateau();

            AddRovers();

            string message = MoveRovers();

            GetLastLocations();

            return message;

        }

        private void GetPlateau()
        {
            _console.WritelineToConsole("\n");
            _console.WritelineToConsole($"Plateau Max Distances :\n");

            string distanceStr = "";

            while (!Checkers.CheckInput(distanceStr, InputType.MAX_DISTANCE))
            {
                distanceStr = _console.ReadLine();

                if (!Checkers.CheckInput(distanceStr, InputType.MAX_DISTANCE))
                    _console.WritelineToConsole("\n**Wrong distance input,please try again.\n");

            }

            List<int> platVals = Parsers.ParseDistancesForPlateau(distanceStr);

            _plateau.DistanceX = platVals[0];
            _plateau.DistanceY = platVals[1];

            _console.WritelineToConsole("\nPlateau max distance values are set!");

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

            //if user wants to exit application
            bool exitReceived = false;


            while (!_startReceived)
            {
                string location = "";

                _console.WritelineToConsole($"{_rovers.Count + 1}.Rover's Starting Location Coordinates :\n");

                //try until get first input is valid and location for the rover
                while (!Checkers.CheckInput(location, InputType.LOCATION, _plateau))
                {
                    location = _console.ReadLine();

                    _startReceived = Checkers.CheckIfUserWantsToStart(location);
                    exitReceived = Checkers.CheckIfUserWantsToExit(location);

                    if (_startReceived || exitReceived)
                        break;

                    if (!Checkers.CheckInput(location, InputType.LOCATION, _plateau))
                        _console.WritelineToConsole("\n**Wrong/OutOfBoundries location input,please try again.\n");
                }

                if (_startReceived)
                    continue;

                if (exitReceived)
                    ExitApplication();

                //first input values to add rover's properties
                List<string> locationValues = Parsers.ParseLocationForRover(location);
                //List<string> locationValues = location.ParseLocation();

                string directives = "";

                _console.WritelineToConsole($"\n" +
                    $"{_rovers.Count + 1}.Rover's Directives :\n");

                //try until get second input is valid and directives for the rover
                while (!Checkers.CheckInput(directives, InputType.DIRECTIVE))
                {
                    //second input value to add rover's properties
                    directives = _console.ReadLine();

                    _startReceived = Checkers.CheckIfUserWantsToStart(directives);
                    exitReceived = Checkers.CheckIfUserWantsToExit(directives);

                    if (_startReceived || exitReceived)
                        break;

                    if (!Checkers.CheckInput(directives, InputType.DIRECTIVE))
                        _console.WritelineToConsole("\n**Wrong directive input,please try again.\n");
                }

                if (_startReceived)
                    continue;

                if (exitReceived)
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
        private string MoveRovers()
        {
            if (_rovers.Count > 0) //if list has valid rover/rovers
            {
                bool outOfBorder = true;
                //move rovers sequentially
                foreach (var rover in _rovers)
                {
                    char[] directives = Parsers.ParseDirectivesForRover(rover.Directives);
                    //char[] directives = rover.Directives.ParseDirectives();

                    foreach (var directive in directives)
                    {
                        outOfBorder = ChangeRoverValues(rover, directive);
                        if (!outOfBorder)
                            break;
                    }

                    if (!outOfBorder)
                        continue;


                }

                return "";
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
                return "";

            }
        }

        /// <summary>
        /// Writes console rovers' last location and direction.
        /// </summary>
        private void GetLastLocations()
        {
            if (_rovers.Count > 0)
            {
                _console.WritelineToConsole("\nOUTPUT:\n");

                int i = 1;

                foreach (var rover in _rovers)
                {
                    if (rover.CoordinateX < 0 || rover.CoordinateY < 0 || rover.CoordinateX > _plateau.DistanceX || rover.CoordinateY > _plateau.DistanceY)
                    {
                        _console.WritelineToConsole($"{i}.Rover : {rover.CoordinateX} {rover.CoordinateY} {rover.Direction} **OUT OF BOUNDRIES (Max Values : {_plateau.DistanceX} {_plateau.DistanceY})**");
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
        private bool ChangeRoverValues(Rover rover, char directive)
        {
            bool outOfBorder = true;

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
                foreach (var crushed in _crushedRovers)
                {
                    if (rover.CoordinateX == crushed.CoordinateX && rover.CoordinateY == crushed.CoordinateY && rover.Direction == crushed.Direction)
                        return false;
                }

                if (rover.Direction == "N")
                {
                    rover.CoordinateY++;

                    outOfBorder = CheckLastYLocation(rover, true);
                }
                else if (rover.Direction == "W")
                {
                    rover.CoordinateX--;

                    outOfBorder = CheckLastXLocation(rover, false);
                }
                else if (rover.Direction == "E")
                {
                    rover.CoordinateX++;

                    outOfBorder = CheckLastXLocation(rover, true);
                }
                else if (rover.Direction == "S")
                {
                    rover.CoordinateY--;

                    outOfBorder = CheckLastYLocation(rover, false);
                }

                if (!outOfBorder)
                    return false;

            }

            return true;
        }

        private bool CheckLastYLocation(Rover rover, bool isPlus)
        {
            if (rover.CoordinateY > _plateau.DistanceY)
            {
                if (isPlus)
                {
                    _console.WritelineToConsole($"Rover got out of border after Y coordinate : {rover.CoordinateY - 1}");
                    _crushedRovers.Add(new Rover()
                    {
                        Direction = rover.Direction,
                        CoordinateX = rover.CoordinateX,
                        CoordinateY = rover.CoordinateY - 1
                    });
                }
                else
                {
                    _console.WritelineToConsole($"Rover got out of border after Y coordinate : {rover.CoordinateY + 1}");
                    _crushedRovers.Add(new Rover()
                    {
                        Direction = rover.Direction,
                        CoordinateX = rover.CoordinateX,
                        CoordinateY = rover.CoordinateY + 1
                    });
                }

                return false;

            }

            return true;
        }

        private bool CheckLastXLocation(Rover rover, bool isPlus)
        {
            if (rover.CoordinateX > _plateau.DistanceX)
            {
                if (isPlus)
                {
                    _console.WritelineToConsole($"Rover got out of border after X coordinate : {rover.CoordinateX - 1}");
                    _crushedRovers.Add(new Rover()
                    {
                        Direction = rover.Direction,
                        CoordinateX = rover.CoordinateX - 1,
                        CoordinateY = rover.CoordinateY
                    });

                }
                else
                {
                    _console.WritelineToConsole($"Rover got out of border after X coordinate : {rover.CoordinateX + 1}");
                    _crushedRovers.Add(new Rover()
                    {
                        Direction = rover.Direction,
                        CoordinateX = rover.CoordinateX + 1,
                        CoordinateY = rover.CoordinateY
                    });
                }
                return false;

            }

            return true;
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
