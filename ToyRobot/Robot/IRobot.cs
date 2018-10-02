using System;
using System.Collections.Generic;
using System.Text;
using ToyRobot.Field;
using ToyRobot.Robot.Enum;

namespace ToyRobot.Robot
{
    /// <summary>
    /// An interface for a robot that can rotate and move forward on a field.
    /// </summary>
    public interface IRobot
    {
        RobotStatus Status { get; }

        /// <summary>Place the robot on a field. Throws an InvalidPositionException if the position on the field is invalid.</summary>
        /// <param name="field">The field to place the robot, eg. a TableTop</param>
        /// <param name="position">Where to put the robot in the field</param>
        /// <param name="facing">Where the robot is facing initially</param>
        void Place(IField field, Position position, Direction facing);

        /// <summary>Move forward (It would depend on the robot how far it moves and whether or not it can climb on a hilly field)</summary>
        void MoveForward();

        /// <summary>Rotates the robot to the right (or to the left if negative) by a certain degree</summary>
        void Rotate(int degreeRight);

        /// <summary>Rotates the robot to the right</summary>
        void TurnRight();
        /// <summary>Rotates the robot to the left</summary>
        void TurnLeft();
    }
}
