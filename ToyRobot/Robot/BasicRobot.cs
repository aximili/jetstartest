using System;
using System.Collections.Generic;
using System.Text;
using ToyRobot.Exceptions;
using ToyRobot.Field;
using ToyRobot.Robot.Enum;
using System.Linq;

namespace ToyRobot.Robot
{
    /// <summary>
    /// This robot can only face N/E/S/W and can only move forward 1 tile at a time.
    /// </summary>
    public class BasicRobot: IRobot
    {
        private static readonly Direction[] VALID_INITIAL_DIRECTIONS = new[] { Direction.North, Direction.East, Direction.South, Direction.West };
        private const int ROTATE_STEP = 90;

        private IField _field;
        private Position _position;
        private Direction _facing;

        public BasicRobot()
        {
            _field = null;
        }

        /// <summary>Gets the current status of the robots (field, position and facing direction)</summary>
        public RobotStatus Status
        {
            get
            {
                if (_field == null)
                {
                    return new RobotStatus { Field = null };
                }
                else
                {
                    return new RobotStatus {
                        Field = _field,
                        Position = _position,
                        Facing = _facing
                    };
                }
            }
        }

        /// <summary>Place the robot on a field. Throws an InvalidPositionException if the position on the field is invalid.</summary>
        /// <param name="field">The field to place the robot, eg. a TableTop</param>
        /// <param name="position">Where to put the robot in the field</param>
        public void Place(IField field, Position position, Direction facing)
        {
            field.ValidatePosition(position);
            ValidateInitialDirection(facing);

            _field = field;
            _position = position;
            _facing = facing;
        }

        /// <summary>Move forward by 1 step</summary>
        public void MoveForward()
        {
            if (_field == null) // Robot has not been placed anywhere
                throw new RobotUninitialisedException();

            Position futurePosition = new Position(_position.X, _position.Y);

            switch (_facing)
            {
                case Direction.North:
                    futurePosition.Y = _position.Y + 1;
                    break;
                case Direction.East:
                    futurePosition.X = _position.X + 1;
                    break;
                case Direction.South:
                    futurePosition.Y = _position.Y - 1;
                    break;
                case Direction.West:
                    futurePosition.X = _position.X - 1;
                    break;
                default:
                    throw new RobotException($"Robot is facing an unsupported Direction: {_facing}");
            }

            if (!_field.IsValidPosition(futurePosition))
                throw new InvalidMoveException("Cannot move forward to an invalid position");

            _position.X = futurePosition.X;
            _position.Y = futurePosition.Y;
        }

        /// <summary>Rotates the robot to the right (or to the left if negative) by a certain degree</summary>
        public void Rotate(int degreeRight)
        {
            if (_field == null) // Robot has not been placed anywhere
                throw new RobotUninitialisedException();

            if (degreeRight % ROTATE_STEP != 0)
                throw new RobotException($"This Robot can only turn by a multiplication of {ROTATE_STEP} degree.");

            _facing = (Direction)(((int)_facing + degreeRight + 360) % 360);
        }

        /// <summary>Rotates the robot 90 degrees to the right</summary>
        public void TurnRight()
        {
            Rotate(90);
        }
        /// <summary>Rotates the robot 90 degrees to the left</summary>
        public void TurnLeft()
        {
            Rotate(-90);
        }


        /// <summary>Throws a RobotException if initial direction is invalid</summary>
        private void ValidateInitialDirection(Direction facing)
        {
            if (!VALID_INITIAL_DIRECTIONS.Contains(facing))
                throw new RobotException("The initial facing direction is not valid for this robot.");
        }
    }
}
