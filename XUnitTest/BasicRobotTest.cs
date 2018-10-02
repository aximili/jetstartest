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
            IRobot robot = new BasicRobot();
            Assert.Throws<RobotUninitialisedException>(() => robot.MoveForward());
        }

        [Fact]
        public void NotPlaced2()
        {
            IRobot robot = new BasicRobot();
            Assert.Null(robot.Status.Position);
        }

        [Fact]
        public void Placed()
        {
            IRobot robot = new BasicRobot();
            robot.Place(new TableTop(5, 5), new Position(4, 0), Direction.East);
            Assert.Equal(4, robot.Status.Position.X);
            Assert.Equal(0, robot.Status.Position.Y);
            Assert.Equal(Direction.East, robot.Status.Facing);
        }

        [Fact]
        public void InvalidMove()
        {
            IRobot robot = new BasicRobot();
            robot.Place(new TableTop(5, 5), new Position(4, 0), Direction.East);
            Assert.Throws<InvalidMoveException>(() => robot.MoveForward());
        }

        [Fact]
        public void TurnLeft()
        {
            IRobot robot = new BasicRobot();
            robot.Place(new TableTop(5, 5), new Position(4, 0), Direction.North);
            robot.TurnLeft();
            Assert.Equal(Direction.West, robot.Status.Facing);
        }

        [Fact]
        public void TurnLeftAndMove()
        {
            IRobot robot = new BasicRobot();
            robot.Place(new TableTop(5, 5), new Position(4, 0), Direction.East);
            robot.TurnLeft();
            robot.MoveForward();
            Assert.Equal(4, robot.Status.Position.X);
            Assert.Equal(1, robot.Status.Position.Y);
            Assert.Equal(Direction.North, robot.Status.Facing);
        }

        [Fact]
        public void Rotate360()
        {
            IRobot robot = new BasicRobot();
            robot.Place(new TableTop(5, 5), new Position(4, 0), Direction.North);
            robot.TurnRight();
            robot.TurnRight();
            robot.TurnRight();
            robot.TurnRight();
            Assert.Equal(Direction.North, robot.Status.Facing);

            robot.TurnLeft();
            robot.TurnLeft();
            robot.TurnLeft();
            robot.TurnLeft();
            Assert.Equal(Direction.North, robot.Status.Facing);
        }

    }
}
