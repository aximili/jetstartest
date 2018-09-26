using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Exceptions
{
    public class FieldException: CustomException
    {
        public FieldException(string message) : base(message) { }
    }

    public class InvalidPositionException : FieldException
    {
        public InvalidPositionException() : base("Invalid position on field") { }
        public InvalidPositionException(string message) : base(message) { }
    }

}
