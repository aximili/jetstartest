using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Exceptions
{
    public class RobotException : CustomException
    {
        public RobotException() : base() { }
        public RobotException(string message) : base(message) { }
    }

    public class RobotUninitialisedException : RobotException
    {
        public RobotUninitialisedException() : base("Robot has not been placed anywhere") { }
        public RobotUninitialisedException(string message) : base(message) { }
    }

    public class InvalidMoveException : RobotException
    {
        public InvalidMoveException() : base("Invalid move or invalid destination") { }
        public InvalidMoveException(string message) : base(message) { }
    }

}
