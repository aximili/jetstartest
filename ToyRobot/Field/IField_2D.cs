using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot.Field
{
    /// <summary>A 2-dimensional field</summary>
    public interface IField_2D
    {
        /// <summary>Returns true if the position is a valid position on this field</summary>
        bool IsValidPosition(Position_2D position);
        /// <summary>Throws an InvalidPositionException if the position is invalid in this field</summary>
        void ValidatePosition(Position_2D position);
    }
}
