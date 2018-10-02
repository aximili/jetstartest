using System;
using ToyRobot.Exceptions;

namespace ToyRobot.Field
{
    /// <summary>
    /// A simple rectangular flat 2D field with no obstacles.
    /// (Obstacle or non-flat-surface support can be added later without changing the existing interface)
    /// </summary>
    public class TableTop: IField
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public TableTop(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
        }

        /// <summary>Returns true if the position is a valid position on this field (for a robot to stand)</summary>
        public bool IsValidPosition(Position position)
        {
            return (position.X >= 0 && position.X < SizeX) && (position.Y >= 0 && position.Y < SizeY);
        }

        /// <summary>Throws an InvalidPositionException if the position is invalid in this field (for a robot to stand)</summary>
        public void ValidatePosition(Position position)
        {
            if (!IsValidPosition(position))
                throw new InvalidPositionException($"Invalid position ({position.X}, {position.Y}) on TableTop of size {SizeX} x {SizeY}");
        }
    }
}
