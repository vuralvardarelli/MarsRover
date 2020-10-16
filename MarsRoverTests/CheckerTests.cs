using MarsRover.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MarsRoverTests
{
    public class CheckerTests
    {
        [Fact]
        public void CheckInput_InputIsNotDistance_ReturnFalse()
        {
            Assert.False(Checkers.CheckInput("-3", MarsRover.Models.InputType.MAX_DISTANCE));
        }

        [Fact]
        public void CheckInput_InputIsDistance_ReturnTrue()
        {
            Assert.True(Checkers.CheckInput("2", MarsRover.Models.InputType.MAX_DISTANCE));
        }

        [Fact]
        public void CheckInput_InputIsNotDirective_ReturnFalse()
        {
            Assert.False(Checkers.CheckInput("kjhdsakck", MarsRover.Models.InputType.DIRECTIVE));
        }

        [Fact]
        public void CheckInput_InputIsDirective_ReturnTrue()
        {
            Assert.True(Checkers.CheckInput("LMR", MarsRover.Models.InputType.DIRECTIVE));
        }

        [Fact]
        public void CheckInput_InputIsNotLocation_ReturnFalse()
        {
            Assert.False(Checkers.CheckInput("kjhdsakck", MarsRover.Models.InputType.LOCATION));
        }

        [Fact]
        public void CheckInput_InputIsLocation_ReturnTrue()
        {
            Assert.True(Checkers.CheckInput("1 2 N", MarsRover.Models.InputType.LOCATION));
        }

        [Fact]
        public void CheckIfUserWantsToStart_InputIsNotStart_ReturnFalse()
        {
            Assert.False(Checkers.CheckIfUserWantsToStart("asdxzcsad"));
        }
        [Fact]
        public void CheckIfUserWantsToStart_InputIsStart_ReturnTrue()
        {
            Assert.True(Checkers.CheckIfUserWantsToStart("start"));
        }

        [Fact]
        public void CheckIfUserWantsToExit_InputIsNotExit_ReturnFalse()
        {
            Assert.False(Checkers.CheckIfUserWantsToExit("aasxz20"));
        }

        [Fact]
        public void CheckIfUserWantsToExit_InputIsExit_ReturnTrue()
        {
            Assert.True(Checkers.CheckIfUserWantsToExit("exit"));
        }
    }
}
