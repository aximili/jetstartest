using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Field
{
    /// <summary>A 2-dimensional field</summary>
    public interface IField
    {
        /// <summary>Returns true if the position is a valid position on this field (for a robot to stand)</summary>
        bool IsValidPosition(Position position);
        /// <summary>Throws an InvalidPositionException if the position is invalid in this field (for a robot to stand)</summary>
        void ValidatePosition(Position position);
    }
}
