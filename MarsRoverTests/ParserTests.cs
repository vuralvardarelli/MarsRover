using MarsRover.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MarsRoverTests
{
    public class ParserTests
    {
        [Fact]
        public void ParseLocationForRover_InputIsStringWithBlanks_ReturnListString()
        {
            Assert.Equal(typeof(List<string>), Parsers.ParseLocationForRover("1 2 3 4 M N").GetType());
        }

        [Fact]
        public void ParseDirectivesForRover_InputIsString_ReturnCharArray()
        {
            Assert.Equal(typeof(char[]), Parsers.ParseDirectivesForRover("12SXJKH").GetType());
        }
    }
}
