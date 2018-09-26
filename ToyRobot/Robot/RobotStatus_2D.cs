using System;
using System.Collections.Generic;
using System.Text;
using ToyRobot.Field;
using ToyRobot.Robot.Enum;

namespace ToyRobot.Robot
{
    public class RobotStatus_2D
    {
        public IField_2D Field { get; set; }
        public Position_2D Position { get; set; }
        public Direction_2D Facing { get; set; }
    }
}
