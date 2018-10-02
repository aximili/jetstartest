using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Field
{
    /// <summary>
    /// A position (or coordinate) on a field
    /// </summary>
    public class Position
    { 
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
