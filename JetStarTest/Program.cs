using Microsoft.Extensions.Configuration;
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
        static ConsoleAppConfig _config;
        static IField _field;

        static void Main(string[] args)
        {
            ReadConfig();
            InitialiseField();

            if (args.Length == 0)
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

        /// <summary>Reads appsettings.json into _config</summary>
        private static void ReadConfig()
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .Build();

            _config = configuration.GetSection("consoleAppConfig").Get<ConsoleAppConfig>();
        }

        /// <summary>Creates an instance of the field and assigns it to _field based on _config</summary>
        private static void InitialiseField()
        {
            switch (_config.Field.Type)
            {
                case "TableTop":
                    _field = new TableTop(_config.Field.SizeX, _config.Field.SizeY);
                    break;
                default:
                    throw new NotImplementedException($"Unhandled field type: {_config.Field.Type}");
            }
        }

        /// <summary>
        /// Starts the application using the first mode: Interactive.
        /// It waits for user input then performs an action based on the input.
        /// </summary>
        static void StartInteractiveMode()
        {
            Console.WriteLine("Starting interactive mode.\n");

            IRobot robot = new BasicRobot();

            while (true)
            {
                Console.Write("\nEnter command: ");
                string input = Console.ReadLine();

                if (input.ToUpper() == "EXIT")
                    break;

                try
                {
                    ProcessCommand(robot, input);

                    if (_config.Verbose && input.ToUpper() != "REPORT")
                        Console.WriteLine("Robot status: " + GetRobotStatus(robot));
                }
                catch (Exception ex)
                {
                    if (ex is RobotException || ex is FieldException)
                        Console.WriteLine(ex.Message);
                    else
                    {
                        Console.WriteLine("Unexpected error - " + ex.Message);
                        // TODO handle/log error
                    }
                }
            }

            Console.WriteLine("\nThank you for playing.");
        }

        /// <summary>
        /// Starts the application using the second mode: Read commands from a file.
        /// It reads the file line by line and executes them.
        /// </summary>
        static void ReadCommandsFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found: " + filename);
                // TODO may be log an error here
                return;
            }

            IRobot robot = new BasicRobot();

            using (StreamReader file = new StreamReader(filename))
            {
                string input;
                while ((input = file.ReadLine()) != null)
                {
                    try
                    {
                        if (_config.Verbose)
                            Console.WriteLine("\n" + input);

                        ProcessCommand(robot, input);

                        if (_config.Verbose && input.ToUpper() != "REPORT")
                            Console.WriteLine("Robot status: " + GetRobotStatus(robot));
                    }
                    catch (Exception ex)
                    {
                        if (ex is RobotException || ex is FieldException)
                            Console.WriteLine(ex.Message);
                        else
                        {
                            Console.WriteLine("Unexpected error - " + ex.Message);
                            // TODO handle/log error
                        }
                    }
                }
            }


        }

        /// <summary>Process a command (either from user input or from a line in a file)</summary>
        /// <param name="robot">The robot to be commanded</param>
        /// <param name="command">The command, eg. MOVE</param>
        private static void ProcessCommand(IRobot robot, string command)
        {
            if (command.ToUpper().StartsWith("PLACE"))
            {
                var match = Regex.Match(command, @"PLACE (\d), *(\d), *([a-z]+)", RegexOptions.IgnoreCase);
                if (!match.Success)
                    throw new SafeException("Invalid command. Try something like: PLACE 0, 0, NORTH");

                var position = new Position(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                var direction = Enum.Parse<Direction>(match.Groups[3].Value, true);
                robot.Place(_field, position, direction);
            }
            else
            {
                switch (command.ToUpper())
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

        private static string GetRobotStatus(IRobot robot)
        {
            if (robot.Status.Field == null)
                throw new SafeException("Please place the robot first using the PLACE command.");

            return $"{robot.Status.Position.X}, {robot.Status.Position.Y}, {robot.Status.Facing.ToString().ToUpper()}";
        }
    }
}
