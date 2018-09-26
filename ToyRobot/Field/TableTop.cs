using System;
using ToyRobot.Exceptions;

namespace ToyRobot.Field
{
    /// <summary>A simple rectangular 2D field</summary>
    public class TableTop: IField_2D
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public TableTop(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
        }

        public bool IsValidPosition(Position_2D position)
        {
            return (position.X >= 0 && position.X < SizeX) && (position.Y >= 0 && position.Y < SizeY);
        }

        public void ValidatePosition(Position_2D position)
        {
            if (!IsValidPosition(position))
                throw new InvalidPositionException($"Invalid position ({position.X}, {position.Y}) on TableTop of size {SizeX} x {SizeY}");
        }
    }
}
