using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Models
{
    /// <summary>
    /// Rover with location , direction and NASA directives.
    /// </summary>
    public class Rover
    {
        public int CoordinateX { get; set; } = 0;
        public int CoordinateY { get; set; } = 0;

        //where the Rover's face is looking
        public string Direction { get; set; }

        //which moves the Rover's has to do.
        public string Directives { get; set; }

    }
}
