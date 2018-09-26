using System;
using System.Collections.Generic;
using System.Text;
using ToyRobot.Exceptions;
using ToyRobot.Field;
using ToyRobot.Robot.Enum;

namespace ToyRobot.Robot
{
    public class BasicRobot: IRobot_2D
    {
        private IField_2D _field;
        private Position_2D _position;
        private Direction_2D _facing;

        public BasicRobot()
        {
            _field = null;
        }

        public RobotStatus_2D Status
        {
            get
            {
                if (_field == null)
                {
                    return new RobotStatus_2D { Field = null };
                }
                else
                {
                    return new RobotStatus_2D {
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
        public void Place(IField_2D field, Position_2D position, Direction_2D facing)
        {
            field.ValidatePosition(position);

            _field = field;
            _position = position;
            _facing = facing;
        }

        /// <summary>Move forward by 1 step</summary>
        public void MoveForward()
        {
            if (_field == null) // Robot has not been placed anywhere
                throw new RobotUninitialisedException();

            Position_2D futurePosition = new Position_2D(_position.X, _position.Y);

            switch (_facing)
            {
                case Direction_2D.North:
                    futurePosition.Y = _position.Y + 1;
                    break;
                case Direction_2D.East:
                    futurePosition.X = _position.X + 1;
                    break;
                case Direction_2D.South:
                    futurePosition.Y = _position.Y - 1;
                    break;
                case Direction_2D.West:
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

            if (degreeRight % 90 != 0)
                throw new RobotException($"Robot can only turn by a multiplication of 90 degree.");

            _facing = (Direction_2D)(((int)_facing + degreeRight) % 360);
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

    }
}
