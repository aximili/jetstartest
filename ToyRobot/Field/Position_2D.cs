using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Field
{
    public class Position_2D
    { 
        public int X { get; set; }
        public int Y { get; set; }

        public Position_2D(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
