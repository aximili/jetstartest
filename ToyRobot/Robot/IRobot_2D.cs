using System;
using System.Collections.Generic;
using System.Text;
using ToyRobot.Field;
using ToyRobot.Robot.Enum;

namespace ToyRobot.Robot
{
    public interface IRobot_2D
    {
        RobotStatus_2D Status { get; }

        /// <summary>Place the robot on a field. Throws an InvalidPositionException if the position on the field is invalid.</summary>
        /// <param name="field">The field to place the robot, eg. a TableTop</param>
        /// <param name="position">Where to put the robot in the field</param>
        /// <param name="facing">Where the robot is facing initially</param>
        void Place(IField_2D field, Position_2D position, Direction_2D facing);

        /// <summary>Move forward by 1 step</summary>
        void MoveForward();

        /// <summary>Rotates the robot to the right (or to the left if negative) by a certain degree</summary>
        void Rotate(int degreeRight);

        /// <summary>Rotates the robot 90 degrees to the right</summary>
        void TurnRight();
        /// <summary>Rotates the robot 90 degrees to the left</summary>
        void TurnLeft();
    }
}
