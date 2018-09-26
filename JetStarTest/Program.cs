using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using ToyRobot.Exceptions;
using ToyRobot.Field;
using ToyRobot.Robot;
using ToyRobot.Robot.Enum;

namespace JetStarTest
{
    class Program
    {
        // TODO These hardcoded const can come from a config file
        static IField_2D _field = new TableTop(5, 5);
        const bool _verbose = true;

        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("You can also feed commands from a file, eg.\n"+
                                  $"  dotnet.exe JetStarTest.dll \"C:\\input.txt\"\n\n");

                StartInteractiveMode();
            }
            else
            {
                ReadCommandsFromFile(args[0]);
            }
        }

        static void ReadCommandsFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found: " + filename);
                // TODO may be log an error here
                return;
            }

            IRobot_2D robot = new BasicRobot();

            using (StreamReader file = new StreamReader(filename))
            {
                string input;
                while ((input = file.ReadLine()) != null)
                {
                    try
                    {
                        if (_verbose)
                            Console.WriteLine("\n" + input);

                        ProcessUserInput(robot, input);

                        if (_verbose && input.ToUpper() != "REPORT")
                            Console.WriteLine("Robot status: " + GetRobotStatus(robot));
                    }
                    catch (SafeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        if (ex is RobotException || ex is FieldException)
                            Console.WriteLine(ex.Message);
                        else
                        {
                            Console.WriteLine("Unexpected error - " + ex.Message);
                            // TODO log error
                        }
                    }
                }
            }
            

        }

        static void StartInteractiveMode()
        {
            Console.WriteLine("Starting interactive mode.\n");

            IRobot_2D robot = new BasicRobot();

            while (true)
            {
                Console.Write("\nEnter command: ");
                string input = Console.ReadLine();

                if (input.ToUpper() == "EXIT")
                    break;

                try
                {
                    ProcessUserInput(robot, input);

                    if (_verbose && input.ToUpper() != "REPORT")
                        Console.WriteLine("Robot status: " + GetRobotStatus(robot));
                }
                catch (SafeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    if (ex is RobotException || ex is FieldException)
                        Console.WriteLine(ex.Message);
                    else
                    {
                        Console.WriteLine("Unexpected error - " + ex.Message);
                        // TODO log error
                    }
                }
            }

            Console.WriteLine("\nThank you for playing.");
        }

        private static void ProcessUserInput(IRobot_2D robot, string input)
        {
            if (input.ToUpper().StartsWith("PLACE"))
            {
                var match = Regex.Match(input, @"PLACE (\d), *(\d), *([a-z]+)", RegexOptions.IgnoreCase);
                if (!match.Success)
                    throw new SafeException("Invalid command. Try something like: PLACE 0, 0, NORTH");

                var position = new Position_2D(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                var direction = Enum.Parse<Direction_2D>(match.Groups[3].Value, true);
                robot.Place(_field, position, direction);
            }
            else
            {
                switch (input.ToUpper())
                {
                    case "MOVE":
                        robot.MoveForward();
                        break;
                    case "LEFT":
                        robot.TurnLeft();
                        break;
                    case "RIGHT":
                        robot.TurnRight();
                        break;
                    case "REPORT":
                        Console.WriteLine(GetRobotStatus(robot));
                        break;
                    default:
                        throw new SafeException("Only the following commands are supported: PLACE, MOVE, LEFT, RIGHT, REPORT");
                }
            }
        }

        private static string GetRobotStatus(IRobot_2D robot)
        {
            if (robot.Status.Field == null)
                throw new SafeException("Please place the robot first using the PLACE command.");

            return $"{robot.Status.Position.X}, {robot.Status.Position.Y}, {robot.Status.Facing.ToString().ToUpper()}";
        }
    }
}
