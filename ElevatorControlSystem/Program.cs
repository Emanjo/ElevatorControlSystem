using ElevatorControlSystem.Models;
using System;
using System.Collections.Generic;

namespace ElevatorControlSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var elevatorControl = new ElevatorControlService(new ElevatorStatusService());

            while(true)
            {
                while(true)
                {
                    Console.WriteLine("Enter floor or press enter to continue:");

                    var choice = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(choice)) break;

                    if (!int.TryParse(choice, out int choiseAsInt)) 
                    {
                        Console.WriteLine("Floor can only be a number! Please try again:");
                        continue;
                    }

                    elevatorControl.EnqueueFloor(choiseAsInt);
                }

                if (!elevatorControl.GoToNextFloor())
                {
                    Console.WriteLine("Arrived at last stop");
                    break;
                }
                    
            }
        }
    }
}
