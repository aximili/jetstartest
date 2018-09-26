using System;
using ToyRobot.Field;
using Xunit;

namespace XUnitTest
{
    public class TableTopTest
    {
        [Fact]
        public void TestPositionGood1()
        {
            TableTop table = new TableTop(2, 3);
            Assert.True(table.IsValidPosition(new Position_2D(0, 0)));
            Assert.True(table.IsValidPosition(new Position_2D(1, 2)));
        }

        [Fact]
        public void TestPositionBad1()
        {
            TableTop table = new TableTop(2, 3);
            Assert.False(table.IsValidPosition(new Position_2D(-1, 0)));
            Assert.False(table.IsValidPosition(new Position_2D(2, 2)));
        }

        [Fact]
        public void TestPositionGood2()
        {
            TableTop table = new TableTop(5, 5);
            Assert.True(table.IsValidPosition(new Position_2D(4, 4)));
            Assert.True(table.IsValidPosition(new Position_2D(4, 0)));
        }

        [Fact]
        public void TestPositionBad2()
        {
            TableTop table = new TableTop(5, 5);
            Assert.False(table.IsValidPosition(new Position_2D(4, -1)));
            Assert.False(table.IsValidPosition(new Position_2D(5, 0)));
        }


    }
}
