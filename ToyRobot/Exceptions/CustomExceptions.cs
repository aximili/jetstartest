using System;
using System.Collections.Generic;
using System.Text;

// TODO This file should be moved to a shared place outside this Class Library project

namespace ToyRobot.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException() : base() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>A safe or expected exception (eg. user error) that does not need to be logged beyond the DEBUG level</summary>
    public class SafeException : CustomException
    {
        public SafeException() : base() { }
        public SafeException(string message) : base(message) { }
        public SafeException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>For rethrowing an Exception that was already logged/handled. Do not log this Exception.</summary>
    public class PassedException : CustomException
    {
        public PassedException() : base() { }
        public PassedException(string message) : base(message) { }
        public PassedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
