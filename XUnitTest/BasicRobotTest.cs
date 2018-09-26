using System;
using System.Collections.Generic;
using System.Text;
using ToyRobot.Exceptions;
using ToyRobot.Field;
using ToyRobot.Robot;
using ToyRobot.Robot.Enum;
using Xunit;

namespace XUnitTest
{
    public class BasicRobotTest
    {
        [Fact]
        public void NotPlaced1()
        {
            IRobot_2D robot = new BasicRobot();
            Assert.Throws<RobotUninitialisedException>(() => robot.MoveForward());
        }

        [Fact]
        public void NotPlaced2()
        {
            IRobot_2D robot = new BasicRobot();
            Assert.Null(robot.Status.Position);
        }

        [Fact]
        public void Placed()
        {
            IRobot_2D robot = new BasicRobot();
            robot.Place(new TableTop(5, 5), new Position_2D(4, 0), Direction_2D.East);
            Assert.Equal(4, robot.Status.Position.X);
            Assert.Equal(0, robot.Status.Position.Y);
            Assert.Equal(Direction_2D.East, robot.Status.Facing);
        }

        [Fact]
        public void InvalidMove()
        {
            IRobot_2D robot = new BasicRobot();
            robot.Place(new TableTop(5, 5), new Position_2D(4, 0), Direction_2D.East);
            Assert.Throws<InvalidMoveException>(() => robot.MoveForward());
        }

        [Fact]
        public void TurnLeft()
        {
            IRobot_2D robot = new BasicRobot();
            robot.Place(new TableTop(5, 5), new Position_2D(4, 0), Direction_2D.East);
            robot.TurnLeft();
            Assert.Equal(Direction_2D.North, robot.Status.Facing);
        }

        [Fact]
        public void TurnLeftAndMove()
        {
            IRobot_2D robot = new BasicRobot();
            robot.Place(new TableTop(5, 5), new Position_2D(4, 0), Direction_2D.East);
            robot.TurnLeft();
            robot.MoveForward();
            Assert.Equal(4, robot.Status.Position.X);
            Assert.Equal(1, robot.Status.Position.Y);
            Assert.Equal(Direction_2D.North, robot.Status.Facing);
        }
    }
}
