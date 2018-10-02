using System;
using System.Collections.Generic;
using System.Text;
using ToyRobot.Field;
using ToyRobot.Robot.Enum;

namespace ToyRobot.Robot
{
    public class RobotStatus
    {
        /// <summary>The field the robot is on</summary>
        public IField Field { get; set; }
        /// <summary>The position of the robot on the field</summary>
        public Position Position { get; set; }
        /// <summary>Where the robot is facing</summary>
        public Direction Facing { get; set; }
    }
}
